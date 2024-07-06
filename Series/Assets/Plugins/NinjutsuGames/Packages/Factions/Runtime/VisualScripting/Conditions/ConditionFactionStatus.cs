using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Is Faction Status")]
    [Description("Returns true if a Faction has a specific status towards another Faction.")]

    [Category("Factions/Is Faction Status")]

    [Keywords("Faction", "Status")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Yellow)]
    [Serializable]
    public class ConditionFactionStatus : Condition
    {
        [SerializeField] private PropertyGetFaction m_Faction = GetFactionInstance.Create();
        [SerializeField] private PropertyGetFaction m_Towards = GetFactionInstance.Create();
        [SerializeField] private CompareFactionStatus m_Status = new();

        
        // PROPERTIES: ----------------------------------------------------------------------------
        
        protected override string Summary => $"<b>{m_Towards}</b> are <b>{m_Status}</b> to <b>{m_Faction}</b>";
        
        // RUN METHOD: ----------------------------------------------------------------------------

        protected override bool Run(Args args)
        {
            var faction = m_Faction.Get(args);
            var towards = m_Towards.Get(args);
            
            return m_Status.Match(faction.GetStatusTowards(towards));
        }
    }
}
