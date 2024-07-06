using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using Event = GameCreator.Runtime.VisualScripting.Event;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("On Member Join Faction")]
    [Category("Factions/On Member Join Faction")]
    [Description("Triggers when a Member joins a Faction.")]
    
    [Keywords("Faction", "Member", "Join")]
    [Image(typeof(IconFaction), ColorTheme.Type.Green, typeof(OverlayArrowLeft))]
    
    [Serializable]
    public class EventOnMemberJoinFaction : Event
    {
        [SerializeField] private PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();
        
        private Member member;
        
        // INITIALIZERS: --------------------------------------------------------------------------

        protected override void OnEnable(Trigger trigger)
        {
            base.OnEnable(trigger);
            
            member = m_Member.Get<Member>(trigger);
            if (!member) return;
            member.EventOnFactionJoined += OnJoinedFaction;
        }

        protected override void OnDisable(Trigger trigger)
        {
            base.OnDisable(trigger);
            
            member = m_Member.Get<Member>(trigger);
            if (!member) return;
            member.EventOnFactionJoined -= OnJoinedFaction;
        }

        private void OnJoinedFaction(Faction faction)
        {
            _ = m_Trigger.Execute(new Args(m_Trigger.gameObject, member.gameObject));
        }
    }
}