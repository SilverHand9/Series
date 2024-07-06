using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using Event = GameCreator.Runtime.VisualScripting.Event;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("On Faction Name Variable Change")]
    [Category("Variables/On Faction Name Variable Change")]
    [Description("Executed when the Faction Name Variable is modified")]

    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]

    [Serializable]
    public class EventOnVariableFactionNameChange : Event
    {
        [SerializeField]
        private DetectorFactionNameVariable m_Variable = new ();
        
        // INITIALIZERS: --------------------------------------------------------------------------
        
        protected override void OnEnable(Trigger trigger)
        {
            base.OnEnable(trigger);
            m_Variable.StartListening(OnChange);
        }

        protected override void OnDisable(Trigger trigger)
        {
            base.OnDisable(trigger);
            m_Variable.StopListening(OnChange);
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void OnChange(string name)
        {
            _ = m_Trigger.Execute(Self);
        }
    }
}