using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Member Status")]
    [Category("Factions/Member Status")]
    
    [Image(typeof(IconFactionMember), ColorTheme.Type.Green, typeof(OverlayArrowRight))]
    [Description("The status of a particular Member towards another Member")]

    [Serializable] [HideLabelsInEditor]
    public class GetStringMemberStatusTowards : PropertyTypeGetString
    {
        [SerializeField] protected PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();
        [SerializeField] protected PropertyGetGameObject m_Towards = GetGameObjectInstance.Create();

        public override string Get(Args args)
        {
            var member = m_Member.Get<Member>(args);
            var towards = m_Towards.Get<Member>(args);
            return member && towards ? member.HighestStatusToMember(towards).Key : string.Empty;
        }

        public static PropertyGetString Create => new(
            new GetStringFactionStatusTowards()
        );

        public override string String => $"{m_Member} status towards {m_Towards}";
    }
}