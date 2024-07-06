using System;
using GameCreator.Runtime.Common;
using UnityEditor;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions.UnityUI
{
    [DisallowMultipleComponent]
    
    [AddComponentMenu("Game Creator/UI/Factions/Faction Item UI")]
    [Icon(RuntimePaths.GIZMOS + "GizmoFactionUI.png")]
    
    [DefaultExecutionOrder(ApplicationManager.EXECUTION_ORDER_LAST_LATER)]
    
    [Serializable]
    public class FactionItemUI : TFactionUI
    {
        public static event Action<Member, Faction> EventSelect;

        public static Faction UI_LastFactionSelected;
        
#if UNITY_EDITOR

        [InitializeOnEnterPlayMode]
        private static void InitializeOnEnterPlayMode()
        {
            EventSelect = null;
            UI_LastFactionSelected = null;
        }
        
#endif
        
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public void RefreshUI(Member member, Faction faction)
        {
            Refresh(member, faction);
        }

        // PUBLIC STATIC METHODS: -----------------------------------------------------------------
        
        public static void SelectFactionUI(Member member, Faction faction)
        {
            UI_LastFactionSelected = faction;
            EventSelect?.Invoke(member, faction);
        }
        
        public static void DeselectFactionUI()
        {
            UI_LastFactionSelected = null;
            EventSelect?.Invoke(null, null);
        }
    }
}