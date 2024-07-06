using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Status Towards")]
    [Category("Factions/Faction Status Color")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Blue, typeof(OverlayArrowRight))]
    [Description("A reference to a Color value of a Faction Status towards another Faction")]
    
    [Keywords("Faction", "Color", "Icon")]

    [Serializable]
    public class GetFactionStatusTowardsColor : PropertyTypeGetColor
    {
        [SerializeField] protected PropertyGetFaction m_Faction = GetFactionInstance.Create();
        [SerializeField] protected PropertyGetFaction m_Towards = GetFactionInstance.Create();

        public override Color Get(Args args)
        {
            var faction = m_Faction.Get(args);
            if(!faction) return Color.white;
            var towards = m_Towards.Get(args);
            if(!towards) return Color.white;
            var stance = Settings.From<FactionsRepository>().Stances.GetStance(faction.GetStatusTowards(towards));
            return stance.GetColor(args);
        }

        public static PropertyGetColor Create() => new(
            new GetFactionStatusTowardsColor()
        );

        public override string String => $"{m_Faction} towards {m_Towards} status Color";
    }
}