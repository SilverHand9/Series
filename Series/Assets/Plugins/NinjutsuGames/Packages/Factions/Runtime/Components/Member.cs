using System;
using System.Collections.Generic;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    /// <summary>
    /// The Member component allows any game object to join, leave, and manage affiliations with multiple factions.
    /// This component is versatile and can be attached to any entity in the game, enabling complex interactions and relationships within a faction-based system.
    /// It provides methods for dynamically altering Memberships and retrieving faction-related information.
    /// </summary>
    
    [HelpURL("https://docs.ninjutsugames.com/game-creator-2/factions/member")]
    [AddComponentMenu("Game Creator/Factions/Member")]
    [Icon(RuntimePaths.GIZMOS + "GizmoMember.png")]
    public class Member : MonoBehaviour, ISpatialHash
    {
        public static List<Member> AllMembers { get; } = new();
        
        [SerializeField] private bool m_IgnoreReputationPoints = false;
        [SerializeField] private FactionList m_StartFactions = new();
        [SerializeField] private List<Faction> m_Factions = new();

        public List<Faction> Factions => m_Factions;

        // Events
        public event Action<Faction> EventOnFactionJoined;
        public event Action<Faction> EventOnFactionLeft;
        public event Action EventReputationStatusChange;
        public event Action EventChange;
        public event Action EventPointsChanged;
        public static event Action EventMembersChanged;
        public Faction LastFactionStatusChange { get; private set; }
        public Faction LastJoinedFaction { get; private set; }
        public Faction LastLeftFaction { get; private set; }
        public Faction LastFactionPointsChange { get; private set; }
        public int LastStatusIndexChange { get; private set; }
        public int LastPointsChange { get; private set; }
        public string LastStatusChange { get; set; }

        public FactionReputation Reputation { get; } = new();

        private FactionStances _stances;
        
        private void Awake()
        {
            _stances = Settings.From<FactionsRepository>().Stances;
            JoinStartFactions();
        }

        private void OnEnable()
        {
            AllMembers.Add(this);
            SpatialHashFactionMembers.Insert(this);
            EventMembersChanged?.Invoke();
        }
        
        private void OnDisable()
        {
            if(!Application.isPlaying) return;
            
            AllMembers.Remove(this);
            SpatialHashFactionMembers.Remove(this);
            
            // Remove from all factions
            foreach (var faction in m_Factions)
            {
                faction.RemoveMember(this);
            }
            
            EventMembersChanged?.Invoke();
        }

        private void JoinStartFactions()
        {
            for(var i = 0; i < m_StartFactions.Length; i++)
            {
                var factionItem = m_StartFactions.Get(i);
                if (factionItem == null) continue;
                
                JoinFaction(factionItem.Faction, factionItem.InitialPoints);
            }
        }
        
        private void InitializePointsForFaction(Faction faction, int points)
        {
            var threshold = faction.Reputation.GetByPoints(points);
            var factionPoints = new FactionPoints(threshold.Stance, points);
            Reputation.Add(faction.Name, factionPoints);
            
            LastFactionStatusChange = faction;
            LastStatusChange = threshold.Stance;
            LastStatusIndexChange = _stances.IndexOf(LastStatusChange);

            if (points <= 0) return;
            LastPointsChange = points;
            EventPointsChanged?.Invoke();
        }
        
        private void RemovePointsForFaction(Faction faction)
        {
            Reputation.Remove(faction.Name);
        }
        
        public void SetReputationPoints(Faction faction, int points)
        {
            var reputation = GetFactionPoints(faction);
            if(reputation == null) return;
            
            // Check if the points are the same
            if (reputation.points == points) return;
            
            // Prevent points to go above the maximum
            points = Mathf.Min(points, faction.Reputation.MaxPoints);

            LastPointsChange = reputation.points = points;
            LastFactionPointsChange = faction;
            
            EventPointsChanged?.Invoke();

            UpdateFactionStance(faction);
        }

        #if UNITY_EDITOR
        public void TriggerChangeEvent()
        {
            EventChange?.Invoke();
        }
        #endif
        
        public int GetReputationPoints(Faction faction)
        {
            return GetFactionPoints(faction)?.points ?? 0;
        }

        private void UpdateFactionStance(Faction faction)
        {
            var reputation = GetFactionPoints(faction);
            if(reputation == null) return;
            
            var thresholds = faction.Reputation.Descending();
            var currentStance = reputation.stance;
            var newStance = currentStance;
            
            foreach (var threshold in thresholds)
            {
                if (reputation.points < threshold.PointsRequired) continue;
                
                newStance = threshold.Stance;
                if (newStance != currentStance)
                {
                    reputation.stance = newStance;
                    
                    LastFactionStatusChange = faction;
                    LastStatusChange = newStance;
                    LastStatusIndexChange = _stances.IndexOf(LastStatusChange);

                    CallReputationStatusChange();
                }

                break;
            }
        }

        public void CallReputationStatusChange()
        {
            EventChange?.Invoke();
            EventReputationStatusChange?.Invoke();
        }

        private FactionPoints GetFactionPoints(Faction faction)
        {
            return Reputation.TryGetValue(faction.Name, out var points) ? points : null;
        }

        /// <summary>
        /// Makes the Member join the specified Faction.
        /// </summary>
        /// <param name="faction">The Faction to join.</param>
        /// <param name="points">The initial reputation points for the Member (default is 0).</param>
        /// <param name="triggerEvents">Determines whether to trigger change events (default is true).</param>
        public void JoinFaction(Faction faction, int points = 0, bool triggerEvents = true)
        {
            if (IsInFaction(faction)) return;

            faction.AddMember(this);
            m_Factions.Add(faction);
            InitializePointsForFaction(faction, points);
            
            LastPointsChange = points;
            LastJoinedFaction = faction;

            if(!triggerEvents) return;
            
            EventChange?.Invoke();
            EventOnFactionJoined?.Invoke(faction);
        }

        /// <summary>
        /// Removes the Faction from the Member's list of joined Factions.
        /// </summary>
        /// <param name="faction">The Faction to leave.</param>
        public void LeaveFaction(Faction faction)
        {
            if (!IsInFaction(faction)) return;
            
            faction.RemoveMember(this);
            m_Factions.Remove(faction);
            RemovePointsForFaction(faction);
            LastLeftFaction = faction;

            EventChange?.Invoke();
            EventOnFactionLeft?.Invoke(faction);
        }
        
        /// <summary>
        /// Returns the "highest" stance of this Character towards the target via the Factions that both belong to.
        /// Stance goes up from Friendly at the lowest level, then Neutral, and then Hostile being the highest.
        /// The check is not two-way, meaning the stance of the target Member's Factions are not tested against
        /// the Factions of this Character. This Member's Faction stances are only checked against the Factions of the
        /// target Member. So a Faction can be friendly towards another while the other Faction might be 
        /// Hostile to it. Returns the default stance if this Member is in no Factions.
        /// </summary>
        public FactionStance HighestStatusToMember(Member target)
        {
            if (m_Factions.Count == 0 || target.m_Factions.Count == 0)
            {
                return _stances.Lowest;
            }

            var highestStanceIndex = -1;
            var highestStance = _stances.Lowest;
            Faction reputationFaction = default;
            FactionPoints reputation = default;
            FactionPoints reputationTarget = default;

            foreach (var faction in m_Factions)
            {
                // Consider reputation points if the system is enabled
                if (faction.ReputationEnabled && Reputation.TryGetValue(faction.Name, out reputation) && !m_IgnoreReputationPoints && !target.m_IgnoreReputationPoints)
                {
                    var stanceIndex = _stances.IndexOf(reputation.stance);
                    if(stanceIndex > highestStanceIndex)
                    {
                        reputationFaction = faction;
                        highestStanceIndex = stanceIndex;
                        highestStance = _stances.GetStance(reputation.stance);
                    }
                    
                    reputationTarget = target.GetFactionPoints(faction);
                    if (reputationTarget is { points: > 0 } && !target.m_IgnoreReputationPoints)
                    {
                        var targetIndex = _stances.IndexOf(reputationTarget.stance);
                        if (targetIndex > highestStanceIndex)
                        {
                            reputationFaction = faction;
                            highestStanceIndex = targetIndex;
                            highestStance = _stances.GetStance(reputationTarget.stance);
                        }
                    }
                }
                
                foreach (var targetFaction in target.m_Factions)
                {
                    if(reputationFaction && reputationFaction.Equals(targetFaction)) continue;

                    var stance = faction.GetStatusTowards(targetFaction);
                    var stanceIndex = _stances.IndexOf(stance);
                    if (stanceIndex > highestStanceIndex)
                    {
                        highestStanceIndex = stanceIndex;
                        highestStance = _stances.Get[highestStanceIndex];
                    }
                }
            }
            return highestStance;
        }
        
        /// <summary>
        /// Returns the current status of this Member towards the a Faction by check all factions and picking the lowest status.
        /// </summary>
        /// <param name="targetFaction">The target Faction to compare stances against</param>
        public FactionStance HighestStatusToFaction(Faction targetFaction)
        {
            var highestStanceIndex = -1;
            var highestStance = _stances.Lowest;

            foreach (var faction in m_Factions)
            {
                // Check this faction's stance towards the target faction
                if (faction.Equals(targetFaction))
                {
                    // Consider reputation points if the system is enabled
                    if (faction.ReputationEnabled && Reputation.TryGetValue(faction.Name, out var reputation) && !m_IgnoreReputationPoints)
                    {
                        var reputationStanceIndex = _stances.IndexOf(reputation.stance);
                        if (reputationStanceIndex > highestStanceIndex)
                        {
                            highestStanceIndex = reputationStanceIndex;
                            highestStance = _stances.Get[highestStanceIndex];
                            continue;
                        }
                    }
                    
                    var stance = faction.GetStatusTowards(targetFaction);
                    var stanceIndex = _stances.IndexOf(stance);
                    if (stanceIndex > highestStanceIndex)
                    {
                        highestStanceIndex = stanceIndex;
                        highestStance = _stances.Get[highestStanceIndex];
                    }
                }
                
                // Check target faction's stance towards this faction
                var reverseStance = targetFaction.GetStatusTowards(faction);
                var reverseStanceIndex = _stances.IndexOf(reverseStance);
                if (reverseStanceIndex > highestStanceIndex)
                {
                    highestStanceIndex = reverseStanceIndex;
                    highestStance = _stances.Get[highestStanceIndex];
                }
            }
            return highestStance;
        }

        /// <summary>
        /// Checks if the Member is a member of the given Faction.
        /// </summary>
        /// <param name="faction">The Faction to check against.</param>
        /// <returns>True if the Member is a member of the given Faction, otherwise false.</returns>
        public bool IsInFaction(Faction faction)
        {
            return m_Factions.Contains(faction);
        }

        /// <summary>
        /// Returns the Faction at the specified index from the list of factions that the Member belongs to.
        /// The index parameter represents the position of the Faction in the list.
        /// If the index is greater than the total number of factions, the first faction in the list is returned.
        /// </summary>
        /// <param name="index">The index of the Faction to retrieve.</param>
        /// <returns>The Faction at the specified index.</returns>
        public Faction GetFaction(int index)
        {
            return m_Factions.Count > 0 ? m_Factions[Mathf.Clamp(index, 0, m_Factions.Count - 1)] : null;
        }

        /// <summary>
        /// Returns the current progress of the Member's reputation towards the specified Faction.
        /// </summary>
        /// <param name="faction"></param>
        /// <returns></returns>
        public float ReputationProgressToFaction(Faction faction)
        {
            if (!Reputation.TryGetValue(faction.Name, out var reputation)) return 0f;
            return (float) reputation.points / faction.Reputation.MaxPoints;
        }
        
        /// <summary>
        /// Returns the current progress of current status of the Member's reputation towards the specified Faction.
        /// </summary>
        /// <param name="faction"></param>
        /// <returns></returns>
        public float ReputationProgressToNextThreshold(Faction faction)
        {
            if (!Reputation.TryGetValue(faction.Name, out var reputation)) return 0f;
            var thresholdPoints = faction.Reputation.GetNextThreshold(reputation.points);
            return (float) reputation.points / thresholdPoints;
        }

        /// <summary>
        /// Sets the Member to ignore reputation points.
        /// </summary>
        /// <param name="ignore">Determines whether to ignore reputation points (default is true).</param>
        public void SetIgnoreReputation(bool ignore = true)
        {
            m_IgnoreReputationPoints = ignore;
        }
        
        private void OnDrawGizmosSelected()
        {
            if(!Application.isPlaying) return;

#if UNITY_EDITOR
            if (UnityEditor.PrefabUtility.IsPartOfPrefabAsset(gameObject)) return;
#endif
            if(!ShortcutPlayer.Instance) return;
            if(gameObject == ShortcutPlayer.Instance.gameObject) return;
            
            var color = HighestStatusToMember(ShortcutPlayer.Instance.GetComponent<Member>()).GetColor();
            Gizmos.color = color;
            Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0.8f, 0), 0.05f);
        }

        private void OnDrawGizmos()
        {
            if(!Application.isPlaying) return;
#if UNITY_EDITOR
            if (UnityEditor.PrefabUtility.IsPartOfPrefabAsset(gameObject)) return;
#endif
            if(!ShortcutPlayer.Instance) return;
            if(gameObject == ShortcutPlayer.Instance.gameObject) return;
            
            var color = HighestStatusToMember(ShortcutPlayer.Instance.GetComponent<Member>()).GetColor();
            color.a /= 2;
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position + new Vector3(0, 0.8f, 0), 0.05f);
        }

        public void OnRemember()
        {
            EventPointsChanged?.Invoke();
            CallReputationStatusChange();
        }
    }
}