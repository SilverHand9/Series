using System;
using System.Collections.Generic;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace NinjutsuGames.Runtime.Factions
{
    
    [CreateAssetMenu(
        fileName = "My Faction",
        menuName = "Game Creator/Factions/Faction"
    )]
    [Icon(RuntimePaths.GIZMOS + "GizmoFaction.png")]
    public class Faction : TGlobalVariables, INameVariable, IEquatable<Faction>
    {
#if UNITY_EDITOR

        [InitializeOnEnterPlayMode]
        private static void InitializeOnEnterPlayMode()
        {
            LastStatusChanged = null;
            LastStatusChangedTarget = null;
        }
        
#endif
        // MEMBERS: -------------------------------------------------------------------------------
    
        [SerializeReference] protected NameList m_NameList = new();
        [SerializeField] private PropertyGetString title = GetStringString.Create;
        [SerializeField] private PropertyGetString description = GetStringTextArea.Create();

        [SerializeField] private PropertyGetColor color = GetColorColorsWhite.Create;
        [SerializeField] private PropertyGetSprite sprite = GetSpriteNone.Create;
        
        [SerializeField] private FactionType m_Type = FactionType.Normal;
        [SerializeField] private int m_SortOrder;
        
        [SerializeField] private FactionReputationList reputation = new (); 
        [SerializeField] private List<FactionRelationshipData> relationships = new();
        [field:NonSerialized] private List<FactionRelationshipData> m_RuntimeRelationShips = new();
        
        // PROPERTIES: ----------------------------------------------------------------------------
        public string Name => TextUtils.Humanize(name);
        public FactionType Type => m_Type;
        public int SortOrder => m_SortOrder;
        public NameList NameList => m_NameList;
        public string[] Names => m_NameList.Names;
        public string GetTitle(Args args) => title.Get(args);
        public string GetDescription(Args args) => description.Get(args);
        public Color GetColor(Args args) => color.Get(args);
        public Sprite GetSprite(Args args) => sprite.Get(args);
        
        public FactionReputationList Reputation => reputation;
        
        public bool ReputationEnabled => reputation.Enabled;
        
        public List<FactionRelationshipData> Relationships
        {
            get
            {
                if (Application.isPlaying) return m_RuntimeRelationShips;
                if(m_RuntimeRelationShips.Count > 0) m_RuntimeRelationShips.Clear();
                return relationships;
            }
        }
        
        public static Faction LastStatusChanged;
        public static Faction LastStatusChangedTarget;
        public static event Action<Faction, Faction, string> EventStatusChange;
        public static event Action<Faction> EventMembersCountChange;

        [field: NonSerialized] private List<Member> Members { get; set; } = new();
        
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        /// <summary>
        /// Returns the index of this faction in the repository.
        /// </summary>
        public int Index => Settings.From<FactionsRepository>().Factions.GetIndex(this);

        public bool Exists(string name)
        {
            return FactionsManager.Instance.Exists(this, name);
        }
        
        public object Get(string name)
        {
            return FactionsManager.Instance.Get(this, name);
        }

        public void Set(string name, object value)
        {
            FactionsManager.Instance.Set(this, name, value);
        }

        public void Register(Action<string> callback)
        {
            if (ApplicationManager.IsExiting) return;
            FactionsManager.Instance.Register(this, callback);
        }
        
        public void Unregister(Action<string> callback)
        {
            if (ApplicationManager.IsExiting) return;
            FactionsManager.Instance.Unregister(this, callback);
        }

        /// <summary>
        /// Gets the relationship status between this faction and the target faction.
        /// </summary>
        /// <param name="faction">The target faction to get the relationship status with.</param>
        /// <returns>The relationship status between this faction and the target faction.</returns>
        public string GetStatusTowards(Faction faction)
        {
            return Relationships.Find(r => r.faction.Equals(faction))?.stance ?? Settings.From<FactionsRepository>().Stances.Lowest.Key;
        }

        /// <summary>
        /// Sets the relationship status between this faction and the target faction.
        /// </summary>
        /// <param name="faction">The target faction to set the relationship status with.</param>
        /// <param name="stance">The new relationship status.</param>
        /// <param name="saveInRuntime">Optional parameter to save the relationship status in runtime.</param>
        public void SetRelationshipStatus(Faction faction, string stance, bool saveInRuntime = false)
        {
            var relationship = Relationships.Find(r => r.faction.Equals(faction));
            if (relationship == null)
            {
                relationship = new FactionRelationshipData(faction, stance);
                Relationships.Add(relationship);
            }
            else
            {
                relationship.stance = stance;
            }
            
#if UNITY_EDITOR
            if (saveInRuntime && Application.isPlaying)
            {
                relationship = relationships.Find(r => r.faction.Equals(faction));
                if (relationship == null)
                {
                    relationship = new FactionRelationshipData(faction, stance);
                    relationships.Add(relationship);
                }
                else
                {
                    relationship.stance = stance;
                }
            } 
#endif
            
            if(!Application.isPlaying) return;
            
            LastStatusChanged = this;
            LastStatusChangedTarget = faction;
            EventStatusChange?.Invoke(this, faction, stance);
        }
        
        /// <summary>
        /// Returns a list of all the members of this faction.
        /// </summary>
        /// <returns></returns>
        public List<Member> GetMembers()
        {
            // Remove all null members
            Members.RemoveAll(m => m == null);
            return Members;
        }
        
        /// <summary>
        /// Adds a new member to this faction.
        /// </summary>
        /// <param name="member"></param>
        internal void AddMember(Member member)
        {
            if (Members.Contains(member)) return;
            Members.Add(member);
            EventMembersCountChange?.Invoke(this);
        }
        
        
        /// <summary>
        /// Removes a member from this faction.
        /// </summary>
        /// <param name="member"></param>
        internal void RemoveMember(Member member)
        {
            if (!Members.Contains(member)) return;
            Members.Remove(member);
            EventMembersCountChange?.Invoke(this);
        }
        
        // EDITOR METHODS: ------------------------------------------------------------------------
        
        public string VariableTitle(string name)
        {
            return FactionsManager.Instance.Title(this, name);
        }

        public Texture VariableIcon(string name)
        {
            return FactionsManager.Instance.Icon(this, name);
        }

        // EQUALITY METHODS: ----------------------------------------------------------------------

        public bool Equals(Faction other)
        {
            return other && UniqueID.Hash == other.UniqueID.Hash;
        }

        public override bool Equals(object other)
        {
            return other is Faction otherFaction && Equals(otherFaction);
        }

        public override int GetHashCode()
        {
            return UniqueID.Hash;
        }

        internal void RuntimeInit()
        {
            Members.Clear();
            m_RuntimeRelationShips = new List<FactionRelationshipData>();
            foreach (var relationship in relationships)
            {
                var copy = new FactionRelationshipData(relationship.faction, relationship.stance);
                m_RuntimeRelationShips.Add(copy);
            }
        }

        public void OnLoadRelationships(SaveFactionRelationships saveFactionRelationships)
        {
            // Replace stance of relationships with the saved ones

            foreach (var relationship in m_RuntimeRelationShips)
            {
                var savedRelationship = saveFactionRelationships[relationship.faction.name];
                if (savedRelationship == null) continue;
                
                relationship.stance = savedRelationship;
                EventStatusChange?.Invoke(this, relationship.faction, savedRelationship);

            }
        }
    }
}