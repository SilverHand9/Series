using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Member Count")]
    [Category("Factions/Member Count")]

    [Image(typeof(IconCharacter), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Get the number of members in a faction.")]
    
    [Serializable] [HideLabelsInEditor]
    public class GetDecimalFactionMembers : PropertyTypeGetDecimal
    {
        [SerializeField] protected PropertyGetFaction m_Faction = GetFactionInstance.Create();

        public override double Get(Args args)
        {
            var faction = m_Faction.Get(args);
            return !faction ? 0 : faction.GetMembers().Count;
        }

        public override string String => $"Members of {m_Faction}";
    }
}