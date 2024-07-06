using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Member Status")]
    [Category("Factions/Member Status")]
    
    [Image(typeof(IconFactionMember), ColorTheme.Type.Green, typeof(OverlayArrowRight))]
    [Description("A reference to a Color value of a Member Status towards another Faction")]
    
    [Keywords("Member","Faction", "Color", "Icon")]

    [Serializable]
    public class GetMemberTowardsMemberColor : PropertyTypeGetColor
    {
        [SerializeField] protected PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();
        [SerializeField] protected PropertyGetGameObject m_Towards = GetGameObjectInstance.Create();

        public override Color Get(Args args)
        {
            var member = m_Member.Get<Member>(args);
            var towards = m_Towards.Get<Member>(args);
            if(!member || !towards) return Color.white;
            var stance = member.HighestStatusToMember(towards);
            return stance.GetColor(args);
        }

        public static PropertyGetColor Create() => new(
            new GetMemberTowardsColor()
        );

        public override string String => $"{m_Member} status towards {m_Towards} Color";
    }
}