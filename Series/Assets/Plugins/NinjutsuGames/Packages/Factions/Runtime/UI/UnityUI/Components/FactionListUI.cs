using System;
using System.Collections;
using System.Collections.Generic;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using NinjutsuGames.Runtime.Factions.UnityUI.Classes;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions.UnityUI
{
    [AddComponentMenu("Game Creator/UI/Factions/Faction List UI")]
    [Icon(RuntimePaths.GIZMOS + "GizmoFactionListUI.png")]
    
    [DefaultExecutionOrder(ApplicationManager.EXECUTION_ORDER_LAST_LATER)]
    
    [Serializable]
    public class FactionListUI : MonoBehaviour
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();
        [SerializeField] private FilterFactions m_Filter = new();
        
        [SerializeField] private RectTransform m_Content;
        [SerializeField] private GameObject m_Prefab;
        
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private Member _mMember;
        private Faction[] _factions;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public StateFlags Show
        {
            get => m_Filter.Show;
            set
            {
                m_Filter.Show = value;
                RefreshUI();
            }
        }
        
        // EVENTS: --------------------------------------------------------------------------------

        public event Action EventRefreshUI;
        
        // INITIALIZERS: --------------------------------------------------------------------------

        private IEnumerator Start()
        {
            yield return null;
            OnEnable();
        }
        
        private void OnEnable()
        {
            _mMember = m_Member.Get<Member>(gameObject);
            if (!_mMember) return;

            _mMember.EventChange -= OnChange;
            _mMember.EventChange += OnChange;
            
            RefreshUI();
        }

        private void OnDisable()
        {
            if (!_mMember) return;
            _mMember.EventChange -= OnChange;
        }
        
        // CALLBACKS: -----------------------------------------------------------------------------
        
        private void OnChange()
        {
            RefreshUI();
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        [ContextMenu("Refresh UI")]
        private void RefreshUI()
        {
            if (!m_Content) return;
            if (!m_Prefab) return;
            
            var factions = CollectFactions();
            Array.Sort(factions, SortFactions);
            
            RectTransformUtils.RebuildChildren(m_Content, m_Prefab, factions.Length);

            for (var i = 0; i < factions.Length; ++i)
            {
                var child = m_Content.GetChild(i);
            
                var factionUI = child.Get<FactionItemUI>();
                if (factionUI) factionUI.RefreshUI(_mMember, factions[i]);
            }
            
            EventRefreshUI?.Invoke();
        }

        private Faction[] CollectFactions()
        {
            if (!_mMember) return Array.Empty<Faction>();
            _factions ??= Settings.From<FactionsRepository>().Factions.Factions;

            var factions = new List<Faction>();
            foreach (var faction in _factions)
            {
                if (!m_Filter.Check(faction, _mMember)) continue;
                factions.Add(faction);
            }

            return factions.ToArray();
        }
        
        private int SortFactions(Faction a, Faction b)
        {
            return a.SortOrder.CompareTo(b.SortOrder);
        }
    }
}