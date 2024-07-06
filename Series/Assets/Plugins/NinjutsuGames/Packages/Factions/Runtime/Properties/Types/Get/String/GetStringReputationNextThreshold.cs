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
    public class GetStringReputationNextThreshold : PropertyTypeGetString
    {
        [SerializeField] private PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();
        [SerializeField] protected PropertyGetFaction m_Faction = GetFactionInstance.Create();

        public override string Get(Args args)
        {
            var faction = m_Faction.Get(args);
            if (!faction) return string.Empty;
            
            var member = m_Member.Get<Member>(args);
            if (!member) return string.Empty;
            
            var reputation = member.GetReputationPoints(faction);
            var threshold = faction.Reputation.GetNextThreshold(reputation);
            return threshold.ToString(CultureInfo.InvariantCulture);
        }

        public override string String => $"{m_Member} reputation next threshold towards {m_Faction}";
    }
}