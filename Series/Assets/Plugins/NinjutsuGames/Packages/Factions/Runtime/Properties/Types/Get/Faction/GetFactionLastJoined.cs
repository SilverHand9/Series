using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Last Faction Joined")]
    [Category("Member/Last Faction Joined")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Green, typeof(OverlayArrowLeft))]
    [Description("A reference to the last Faction a member joined")]

    [Serializable] [HideLabelsInEditor]
    public class GetFactionLastJoined : PropertyTypeGetFaction
    {
        [SerializeField] protected PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();

        public override Faction Get(Args args)
        {
            var member = m_Member.Get<Member>(args);
            return member ? member.LastJoinedFaction : null;
        }

        public static PropertyGetFaction Create()
        {
            var instance = new GetFactionLastJoined();
            return new PropertyGetFaction(instance);
        }

        public override string String => "Last Faction Joined";
    }
}