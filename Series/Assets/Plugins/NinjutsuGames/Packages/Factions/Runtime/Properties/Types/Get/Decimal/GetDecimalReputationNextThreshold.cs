using System;
using System.Globalization;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Reputation Next Threshold")]
    [Category("Factions/Reputation Next Threshold")]

    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayBar))]
    [Description("Returns the reputation points needed to reach the next threshold of a Member towards a Faction")]
    
    [Serializable] [HideLabelsInEditor]
    public class GetDecimalReputationNextThreshold : PropertyTypeGetDecimal
    {
        [SerializeField] private PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();
        [SerializeField] protected PropertyGetFaction m_Faction = GetFactionInstance.Create();

        public override double Get(Args args)
        {
            var faction = m_Faction.Get(args);
            if (!faction) return 0;
            
            var member = m_Member.Get<Member>(args);
            if (!member) return 0;
            
            var reputation = member.GetReputationPoints(faction);
            return faction.Reputation.GetNextThreshold(reputation);
        }

        public override string String => $"{m_Member} reputation next threshold towards {m_Faction}";
    }
}