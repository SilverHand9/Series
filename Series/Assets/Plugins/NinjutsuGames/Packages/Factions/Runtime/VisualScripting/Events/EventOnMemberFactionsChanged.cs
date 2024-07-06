using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using Event = GameCreator.Runtime.VisualScripting.Event;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("On Member Factions Change")]
    [Category("Factions/On Member Factions Change")]
    [Description("Triggers when there is a change in the factions of a member.")]
    
    [Keywords("Faction", "Member")]
    [Image(typeof(IconFaction), ColorTheme.Type.Yellow)]
    
    [Serializable]
    public class EventOnMemberFactionsChanged : Event
    {
        [SerializeField] private PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();
        
        private Member member;
        
        // INITIALIZERS: --------------------------------------------------------------------------

        protected override void OnEnable(Trigger trigger)
        {
            base.OnEnable(trigger);
            
            member = m_Member.Get<Member>(trigger);
            if (member == null) return;
            member.EventChange += OnChange;
        }

        protected override void OnDisable(Trigger trigger)
        {
            base.OnDisable(trigger);
            
            member = m_Member.Get<Member>(trigger);
            if (member == null) return;
            member.EventChange -= OnChange;
        }

        private void OnChange()
        {
            _ = m_Trigger.Execute(new Args(m_Trigger.gameObject, member.gameObject));
        }
    }
}