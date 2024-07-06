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
    public class GetStringReputationPoints : PropertyTypeGetString
    {
        [SerializeField] private PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();
        [SerializeField] protected PropertyGetFaction m_Faction = GetFactionInstance.Create();

        public override string Get(Args args)
        {
            var faction = m_Faction.Get(args);
            if (!faction) return string.Empty;
            
            var member = m_Member.Get<Member>(args);
            return !member ? string.Empty : member.GetReputationPoints(faction).ToString("0", CultureInfo.InvariantCulture);
        }

        public override string String => $"{m_Member} Reputation Points from {m_Faction}";
    }
}