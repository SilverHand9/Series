using System;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Status")]
    [Category("Factions/Faction Status")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Blue, typeof(OverlayArrowRight))]
    [Description("The status of a particular Faction Status towards another Faction")]

    [Serializable] [HideLabelsInEditor]
    public class GetStringFactionStatusTowards : PropertyTypeGetString
    {
        [SerializeField] protected PropertyGetFaction m_Faction = GetFactionInstance.Create();
        [SerializeField] protected PropertyGetFaction m_Towards = GetFactionInstance.Create();

        public override string Get(Args args)
        {
            var faction = m_Faction.Get(args);
            var towards = m_Towards.Get(args);
            return faction && towards ? faction.GetStatusTowards(towards) : string.Empty;
        }

        public static PropertyGetString Create => new(
            new GetStringFactionStatusTowards()
        );

        public override string String => $"{m_Faction} Name";
    }
}