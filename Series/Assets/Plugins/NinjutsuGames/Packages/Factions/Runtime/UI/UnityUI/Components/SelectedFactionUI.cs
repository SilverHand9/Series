using System;
using System.Collections;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions.UnityUI
{
    [DisallowMultipleComponent]
    
    [AddComponentMenu("Game Creator/UI/Factions/Selected Faction UI")]
    [Icon(RuntimePaths.GIZMOS + "GizmoFactionUI.png")]
    
    [DefaultExecutionOrder(ApplicationManager.EXECUTION_ORDER_LAST_LATER)]
    
    public class SelectedFactionUI : TFactionUI
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private GameObject m_ActiveIfSelected;
        
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private Member m_PreviousJournal;
        
        // INITIALIZERS: --------------------------------------------------------------------------

        private IEnumerator Start()
        {
            yield return null;
            OnSelectFaction(Member, Faction);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            FactionItemUI.EventSelect -= OnSelectFaction;
            FactionItemUI.EventSelect += OnSelectFaction;
        }

        protected override  void OnDisable()
        {
            base.OnDisable();
            FactionItemUI.EventSelect -= OnSelectFaction;
        }
        
        // CALLBACK METHODS: ----------------------------------------------------------------------
        
        private void OnSelectFaction(Member member, Faction faction)
        {
            if (m_ActiveIfSelected)
            {
                bool isSelected = faction;
                m_ActiveIfSelected.SetActive(isSelected);
            }
            
            if (!faction) return;
            Refresh(member, faction);
        }
    }
}