using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Member Status Towards Faction")]
    [Category("Factions/Member Status Towards Faction")]
    
    [Image(typeof(IconFactionMember), ColorTheme.Type.Blue, typeof(OverlayArrowRight))]
    [Description("The status of a particular Member towards a Faction")]

    [Serializable] [HideLabelsInEditor]
    public class GetStringMemberStatusTowardsFaction : PropertyTypeGetString
    {
        [SerializeField] protected PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();
        [SerializeField] protected PropertyGetFaction m_Towards = GetFactionInstance.Create();

        public override string Get(Args args)
        {
            var member = m_Member.Get<Member>(args);
            var towards = m_Towards.Get(args);
            return member && towards ? member.HighestStatusToFaction(towards).Key : string.Empty;
        }

        public static PropertyGetString Create => new(
            new GetStringFactionStatusTowards()
        );

        public override string String => $"{m_Member} status towards {m_Towards}";
    }
}