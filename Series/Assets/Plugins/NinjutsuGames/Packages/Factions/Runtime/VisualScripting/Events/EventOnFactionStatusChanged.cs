using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("On Faction Status")]
    [Category("Factions/On Faction Status")]
    [Description("Triggers when a faction status changes.")]
    
    [Keywords("Faction", "Team")]
    [Image(typeof(IconFaction), ColorTheme.Type.Blue)]
    
    [Serializable]
    public class EventOnFactionStatusChanged : GameCreator.Runtime.VisualScripting.Event
    {
        [SerializeField] private CompareFactionOrAny m_Faction = new();
        
        // INITIALIZERS: --------------------------------------------------------------------------

        protected override void OnEnable(Trigger trigger)
        {
            base.OnEnable(trigger);
            
            Faction.EventStatusChange += OnChange;
        }

        protected override void OnDisable(Trigger trigger)
        {
            base.OnDisable(trigger);
            
            Faction.EventStatusChange -= OnChange;
        }

        private void OnChange(Faction from, Faction towards, string stance)
        {
            var args = new Args(m_Trigger.gameObject);
            if (!m_Faction.Match(from, args)) return;
            
            _ = m_Trigger.Execute(args);
        }
    }
}