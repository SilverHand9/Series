using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Status Color")]
    [Category("Factions/Status Color")]
    
    [Image(typeof(IconBookmarkSolid), ColorTheme.Type.Yellow)]
    [Description("A reference to a Color value of a Faction Status")]
    
    [Keywords("Status", "Faction", "Icon")]

    [Serializable]
    public class GetFactionStatusColor : PropertyTypeGetColor
    {
        [SerializeField, FactionStance] private string m_Status;

        public override Color Get(Args args)
        {
            var stance = Settings.From<FactionsRepository>().Stances.GetStance(m_Status);
            return stance?.GetColor(args) ?? Color.white;
        }

        public static PropertyGetColor Create() => new(
            new GetFactionStatusColor()
        );

        public override string String => $"{m_Status} Color";
    }
}