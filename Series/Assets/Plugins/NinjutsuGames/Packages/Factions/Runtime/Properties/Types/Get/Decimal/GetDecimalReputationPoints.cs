using System;
using System.Globalization;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Reputation Points")]
    [Category("Factions/Reputation Points")]

    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the reputation points from a Faction of a Member.")]
    
    [Serializable] [HideLabelsInEditor]
    public class GetDecimalReputationPoints : PropertyTypeGetDecimal
    {
        [SerializeField] private PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();
        [SerializeField] protected PropertyGetFaction m_Faction = GetFactionInstance.Create();

        public override double Get(Args args)
        {
            var faction = m_Faction.Get(args);
            if (!faction) return 0;
            
            var member = m_Member.Get<Member>(args);
            return !member ? 0 : member.GetReputationPoints(faction);
        }

        public override string String => $"{m_Member} Reputation Points from {m_Faction}";
    }
}