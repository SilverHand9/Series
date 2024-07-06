using System;
using System.Collections.Generic;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions.UnityUI
{
    public enum SortDirection
    {
        Ascending,
        Descending
    }
    
    [AddComponentMenu("Game Creator/UI/Factions/Scoreboard UI")]
    [Icon(RuntimePaths.GIZMOS + "GizmoFactionListUI.png")]
    
    [DefaultExecutionOrder(ApplicationManager.EXECUTION_ORDER_LAST_LATER)]
    
    [Serializable]
    public class ScoreboardUI : BaseFactionUI
    {
        
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private CompareFactionOrAny m_Faction = new();
        [SerializeField] private RectTransform m_Content;
        [SerializeField] private GameObject m_Prefab;
        [SerializeField] private SortDirection m_SortDirection = SortDirection.Descending;
        [SerializeField] private int m_SortFieldIndex = 0;
        
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private Faction[] _factions;
        private Member[] _members;
        private double _lastUpdate;

        // PROPERTIES: ----------------------------------------------------------------------------

        public int SortIndex => m_SortFieldIndex;
        public SortDirection SortDirection => m_SortDirection;
        
        // EVENTS: --------------------------------------------------------------------------------

        public event Action EventRefreshUI;
        
        // INITIALIZERS: --------------------------------------------------------------------------
        
        private void Awake()
        {
            Faction = m_Faction.Get(gameObject);
            RefreshUI();
        }

        private void Start()
        {
            RefreshUI();
        }
        
        private void OnEnable()
        {
            Faction.EventMembersCountChange -= OnMembersCountChange;
            Faction.EventMembersCountChange += OnMembersCountChange;
            
            RefreshUI();
        }

        private void OnDisable()
        {
            Faction.EventMembersCountChange -= OnMembersCountChange;
        }

        private void LateUpdate()
        {
            if (!(Time.time > _lastUpdate)) return;
            Refresh(_members);
            _lastUpdate = Time.time + 0.5f;
        }

        // CALLBACKS: -----------------------------------------------------------------------------
        
        private void OnMembersCountChange(Faction faction)
        {
            if(!m_Faction.Match(faction, gameObject)) return;
            
            RefreshUI();
        }
        private void OnFactionChange()
        {
            RefreshUI();
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        [ContextMenu("Refresh UI")]
        private void RefreshUI()
        {
            if (!m_Content) return;
            if (!m_Prefab) return;
            
            _members = CollectMembers();
            
            RectTransformUtils.RebuildChildren(m_Content, m_Prefab, _members.Length);
            Refresh(_members);
            
            EventRefreshUI?.Invoke();
        }

        private void Refresh(Member[] members)
        {
            for (var i = 0; i < members.Length; ++i)
            {
                var child = m_Content.GetChild(i);
                var itemUI = child.Get<ScoreboardItemUI>();
                if (itemUI) itemUI.RefreshUI(members[i]);
            }
            
            // Sort the children by the SortField value from the ScoreboardItemUI
            var children = new List<Transform>();
            for (var i = 0; i < m_Content.childCount; ++i)
            {
                children.Add(m_Content.GetChild(i));
            }
            
            children.Sort((a, b) =>
            {
                var itemA = a.Get<ScoreboardItemUI>();
                var itemB = b.Get<ScoreboardItemUI>();
                if (!itemA || !itemB) return 0;
                
                if (m_SortDirection == SortDirection.Descending)
                {
                    (itemA, itemB) = (itemB, itemA);
                }
                
                return itemA.GetField(m_SortFieldIndex).CompareTo(itemB.GetField(m_SortFieldIndex));
            });
            
            for (var i = 0; i < children.Count; ++i)
            {
                var child = children[i];
                child.SetSiblingIndex(i);
                
                var itemUI = child.Get<ScoreboardItemUI>();
                if (itemUI) itemUI.SetAlternateBackground(i % 2 == 0);
            }
        }

        private Member[] CollectMembers()
        {
            _factions = Settings.From<FactionsRepository>().Factions.Factions;

            var members = new List<Member>();
            foreach (var faction in _factions)
            {
                if(!m_Faction.Match(faction, gameObject)) continue;
                members.AddRange(faction.GetMembers());
            }

            return members.ToArray();
        }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public void SetSortIndex(int index)
        {
            m_SortFieldIndex = index;
            RefreshUI();
        }   
        
        public void SetSortDirection(SortDirection direction)
        {
            m_SortDirection = direction;
            RefreshUI();
        }   
    }
}