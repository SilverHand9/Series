using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Color")]
    [Category("Factions/Faction Color")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Blue)]
    [Description("A reference to a Color value of a Faction")]
    
    [Keywords("Faction", "Color", "Icon")]

    [Serializable]
    public class GetFactionColor : PropertyTypeGetColor
    {
        [SerializeField] protected PropertyGetFaction m_Faction = GetFactionInstance.Create();

        public override Color Get(Args args)
        {
            var faction = m_Faction.Get(args);
            return faction ? faction.GetColor(args) : Color.white;
        }

        public static PropertyGetColor Create() => new(
            new GetFactionColor()
        );

        public override string String => $"{m_Faction} Color";
    }
}