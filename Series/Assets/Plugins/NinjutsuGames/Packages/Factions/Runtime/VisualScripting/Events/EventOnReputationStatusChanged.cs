using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using Event = GameCreator.Runtime.VisualScripting.Event;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("On Reputation Status Change")]
    [Category("Factions/On Reputation Status Change")]
    [Description("Triggers when the reputation status of Member is modified.")]
    
    [Keywords("Faction", "Member", "Status", "Change")]
    [Image(typeof(IconFaction), ColorTheme.Type.Teal, typeof(OverlayBolt))]
    
    [Serializable]
    public class EventOnReputationStatusChanged : Event
    {
        private enum DetectionType
        {
            OnChange,
            OnIncrease,
            OnDecrease
        }
        
        [SerializeField] private PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();
        [SerializeField] private CompareFactionOrAny m_Faction = new();
        [SerializeField] private DetectionType m_When = DetectionType.OnChange;
        
        private Member member;
        private int _lastValue;

        // INITIALIZERS: --------------------------------------------------------------------------

        protected override void OnEnable(Trigger trigger)
        {
            base.OnEnable(trigger);
            
            member = m_Member.Get<Member>(trigger);
            if (!member) return;
            member.EventReputationStatusChange += ReputationChanged;
            _lastValue = member.LastStatusIndexChange;
        }

        protected override void OnDisable(Trigger trigger)
        {
            base.OnDisable(trigger);
            
            member = m_Member.Get<Member>(trigger);
            if (!member) return;
            member.EventReputationStatusChange -= ReputationChanged;
        }

        private void ReputationChanged()
        {
            var args = new Args(m_Trigger.gameObject);
            if (!m_Faction.Match(member.LastFactionStatusChange, args)) return;
            
            var nextValue = member.LastStatusIndexChange;
            var prevValue = _lastValue;
            
            _lastValue = nextValue;
            
            if (Math.Abs(nextValue - prevValue) < float.Epsilon) return;
            if (m_When == DetectionType.OnIncrease && nextValue <= prevValue) return;
            if (m_When == DetectionType.OnDecrease && nextValue >= prevValue) return;
            
            _ = m_Trigger.Execute(new Args(m_Trigger.gameObject, member.gameObject));
        }
    }
}