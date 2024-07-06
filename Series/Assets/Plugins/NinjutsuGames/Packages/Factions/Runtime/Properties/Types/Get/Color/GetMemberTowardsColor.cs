using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Member Status Towards Faction")]
    [Category("Factions/Member Status Towards Faction")]
    
    [Image(typeof(IconFactionMember), ColorTheme.Type.Blue, typeof(OverlayArrowRight))]
    [Description("A reference to a Color value of a Member Status towards another Member")]
    
    [Keywords("Member","Faction", "Color", "Icon")]

    [Serializable]
    public class GetMemberTowardsColor : PropertyTypeGetColor
    {
        [SerializeField] protected PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();
        [SerializeField] protected PropertyGetFaction m_Towards = GetFactionInstance.Create();

        public override Color Get(Args args)
        {
            var member = m_Member.Get<Member>(args);
            var towards = m_Towards.Get(args);
            if(!member || !towards) return Color.white;
            var stance = member.HighestStatusToFaction(towards);
            return stance.GetColor(args);
        }

        public static PropertyGetColor Create() => new(
            new GetMemberTowardsColor()
        );

        public override string String => $"{m_Member} towards Faction[{m_Towards}] Color";
    }
}