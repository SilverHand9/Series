using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("On Member Count")]
    [Category("Factions/On Member Count")]
    [Description("Triggers when a Member count changes.")]
    
    [Keywords("Faction", "Team")]
    [Image(typeof(IconFaction), ColorTheme.Type.Blue, typeof(OverlayDot))]
    
    [Serializable]
    public class EventOnFactionMemberCountChanged : GameCreator.Runtime.VisualScripting.Event
    {
        [SerializeField] private CompareFactionOrAny m_Faction = new();
        
        // INITIALIZERS: --------------------------------------------------------------------------

        protected override void OnEnable(Trigger trigger)
        {
            base.OnEnable(trigger);
            
            Faction.EventMembersCountChange += OnChange;
        }

        protected override void OnDisable(Trigger trigger)
        {
            base.OnDisable(trigger);
            
            Faction.EventMembersCountChange -= OnChange;
        }

        private void OnChange(Faction from)
        {
            var args = new Args(m_Trigger.gameObject);
            if (!m_Faction.Match(from, args)) return;
            
            _ = m_Trigger.Execute(args);
        }
    }
}