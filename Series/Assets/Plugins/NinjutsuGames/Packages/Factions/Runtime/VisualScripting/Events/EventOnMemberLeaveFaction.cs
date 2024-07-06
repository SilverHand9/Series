using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using Event = GameCreator.Runtime.VisualScripting.Event;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("On Member Leave Faction")]
    [Category("Factions/On Member Leave Faction")]
    [Description("Triggers when a Member joins a Faction.")]
    
    [Keywords("Faction", "Member", "Leave")]
    [Image(typeof(IconFaction), ColorTheme.Type.Red, typeof(OverlayArrowRight))]
    
    [Serializable]
    public class EventOnMemberLeaveFaction : Event
    {
        [SerializeField] private PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();
        
        private Member member;
        
        // INITIALIZERS: --------------------------------------------------------------------------

        protected override void OnEnable(Trigger trigger)
        {
            base.OnEnable(trigger);
            
            member = m_Member.Get<Member>(trigger);
            if (member == null) return;
            member.EventOnFactionLeft += OnLeftFaction;
        }

        protected override void OnDisable(Trigger trigger)
        {
            base.OnDisable(trigger);
            
            member = m_Member.Get<Member>(trigger);
            if (member == null) return;
            member.EventOnFactionLeft -= OnLeftFaction;
        }

        private void OnLeftFaction(Faction faction)
        {
            _ = m_Trigger.Execute(new Args(m_Trigger.gameObject, member.gameObject));
        }
    }
}