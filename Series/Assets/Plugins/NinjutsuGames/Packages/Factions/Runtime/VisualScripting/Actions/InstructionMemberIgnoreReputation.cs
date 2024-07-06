using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Set Member Ignore Reputation")]
    [Description("Set Member to ignore reputation points.")]

    [Category("Factions/Set Member Ignore Reputation")]
    
    [Parameter("Target", "The Member to ignore reputation.")]

    [Image(typeof(IconFactionMember), ColorTheme.Type.Green, typeof(OverlayCross))]
    
    [Keywords("Faction", "Ignore", "Reputation")]
    [Serializable]
    public class InstructionMemberIgnoreReputation : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectPlayer.Create();
        [SerializeField] private bool m_IgnoreReputation = true;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Set {m_Target} ignore reputation: {m_IgnoreReputation}";

        // RUN METHOD: ----------------------------------------------------------------------------

        protected override Task Run(Args args)
        {
            var member = m_Target.Get<Member>(args);
            if (!member) return Task.CompletedTask;

            member.SetIgnoreReputation(m_IgnoreReputation);
            
            return Task.CompletedTask;
        }
    }
}