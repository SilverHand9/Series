using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Is Member In Faction")]
    [Description("Returns true if a Member is in a Faction.")]

    [Category("Factions/Is Member In Faction")]

    [Keywords("Member", "Faction", "Status")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Teal)]
    [Serializable]
    public class ConditionFactionMemberOf : Condition
    {
        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectPlayer.Create();
        [SerializeField] private PropertyGetFaction m_Faction = new();
        
        // PROPERTIES: ----------------------------------------------------------------------------
        
        protected override string Summary => $"{m_Target} in <b>{m_Faction}</b> faction";
        
        // RUN METHOD: ----------------------------------------------------------------------------

        protected override bool Run(Args args)
        {
            var member = m_Target.Get<Member>(args);
            return member && member.IsInFaction(m_Faction.Get(args));
        }
    }
}
