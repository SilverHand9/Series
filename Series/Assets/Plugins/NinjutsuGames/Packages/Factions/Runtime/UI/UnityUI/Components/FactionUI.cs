using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace NinjutsuGames.Runtime.Factions.UnityUI
{
    [DisallowMultipleComponent]
    
    [AddComponentMenu("Game Creator/UI/Factions/Faction UI")]
    [Icon(RuntimePaths.GIZMOS + "GizmoFactionUI.png")]
    
    [DefaultExecutionOrder(ApplicationManager.EXECUTION_ORDER_LAST_LATER)]
    
    [Serializable]
    public class FactionUI : TFactionUI
    {
        [SerializeField] private PropertyGetFaction m_Faction = GetFactionInstance.Create();
        [SerializeField] private PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();

        private void Awake()
        {
            Member = m_Member.Get<Member>(gameObject);
            Faction = m_Faction.Get(gameObject);
            Refresh(Member, Faction);
        }

        private void Start()
        {
            Awake();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            Member = m_Member.Get<Member>(gameObject);
            Faction = m_Faction.Get(gameObject);
            Faction.EventStatusChange += FactionChange;
            
            Refresh(Member, Faction);

            if(!Member) return;
            Member.EventPointsChanged += Refresh;
            Member.EventChange += Refresh;
            Member.EventReputationStatusChange += Refresh;
        }

        private void FactionChange(Faction arg1, Faction arg2, string arg3)
        {
            Refresh();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Faction.EventStatusChange -= FactionChange;

            if(!Member) return;
            Member.EventPointsChanged -= Refresh;
            Member.EventChange -= Refresh;
            Member.EventReputationStatusChange -= Refresh;
        }
    }
}