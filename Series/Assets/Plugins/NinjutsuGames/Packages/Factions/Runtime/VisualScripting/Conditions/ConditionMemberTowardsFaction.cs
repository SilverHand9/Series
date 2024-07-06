using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Is Member Towards Faction")]
    [Description("Returns true if a Member status towards a Faction is a specific status.")]

    [Category("Factions/Is Member Towards Faction")]

    [Keywords("Member", "Faction", "Status")]
    
    [Image(typeof(IconFactionMember), ColorTheme.Type.Teal, typeof(OverlayArrowRight))]
    [Serializable]
    public class ConditionMemberTowardsFaction : Condition
    {
        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectPlayer.Create();
        [SerializeField] private PropertyGetFaction m_Faction = new();
        [SerializeField] private CompareFactionStatus m_Status = new();

        
        // PROPERTIES: ----------------------------------------------------------------------------
        
        protected override string Summary => $"<b>{m_Faction}</b> is <b>{m_Status}</b> to <b>{m_Target}</b>";
        
        // RUN METHOD: ----------------------------------------------------------------------------

        protected override bool Run(Args args)
        {
            var member = m_Target.Get<Member>(args);
            if (!member) return false;
            var faction = m_Faction.Get(args);
            var status = member.HighestStatusToFaction(faction);
            return m_Status.Match(status.Key);
        }
    }
}
