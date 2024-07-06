using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Last Faction Left")]
    [Category("Member/Last Faction Left")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Red, typeof(OverlayArrowRight))]
    [Description("A reference to the last Faction a member left")]

    [Serializable] [HideLabelsInEditor]
    public class GetFactionLastLeft : PropertyTypeGetFaction
    {
        [SerializeField] protected PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();

        public override Faction Get(Args args)
        {
            var member = m_Member.Get<Member>(args);
            return member ? member.LastLeftFaction : null;
        }

        public static PropertyGetFaction Create()
        {
            var instance = new GetFactionLastLeft();
            return new PropertyGetFaction(instance);
        }

        public override string String => "Last Faction Left";
    }
}