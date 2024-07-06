using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Is Member Status")]
    [Description("Returns true if a Member towards another Member has a specific status")]

    [Category("Factions/Is Member Status")]

    [Keywords("Member", "Faction", "Status")]
    
    [Image(typeof(IconFactionMember), ColorTheme.Type.Green, typeof(OverlayArrowRight))]
    [Serializable]
    public class ConditionFactionMemberStatus : Condition
    {
        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectPlayer.Create();
        [SerializeField] private PropertyGetGameObject m_Towards = GetGameObjectInstance.Create();
        [SerializeField] private CompareFactionStatus m_Status = new();
        
        // PROPERTIES: ----------------------------------------------------------------------------
        
        protected override string Summary => $"<b>{m_Towards}</b> is <b>{m_Status}</b> to <b>{m_Target}</b>";
        
        // RUN METHOD: ----------------------------------------------------------------------------

        protected override bool Run(Args args)
        {
            var member = m_Target.Get<Member>(args);
            if (member == null) return false;
            var towards = m_Towards.Get<Member>(args);
            
            return m_Status.Match(member.HighestStatusToMember(towards).Key);
        }
    }
}
