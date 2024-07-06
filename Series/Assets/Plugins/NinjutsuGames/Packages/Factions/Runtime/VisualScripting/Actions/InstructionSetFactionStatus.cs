using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Set Faction Status")]
    [Description("Set the status of one Faction towards another Faction.")]

    [Category("Factions/Set Faction Status")]
    
    [Parameter("Faction", "The Faction to set status on")]
    [Parameter("Towards", "The Faction to change the status towards")]
    [Parameter("Change Both Ways", "Change the status for both factions?")]
    [Parameter("Status", "The status to set")]

    [Image(typeof(IconFaction), ColorTheme.Type.Blue, typeof(OverlayTick))]
    
    [Keywords("Faction", "Join")]
    [Serializable]
    public class InstructionSetFactionStatus : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------
        

        [SerializeField] private PropertyGetFaction m_Faction = new();
        [SerializeField] private PropertyGetFaction m_Towards = new();
        [FactionStance, SerializeField] private string m_Status;
        [SerializeField] private PropertyGetBool m_ChangeBothWays = GetBoolTrue.Create;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Set status of <b>{m_Faction}</b> towards <b>{m_Towards}</b> to {m_Status}";

        // RUN METHOD: ----------------------------------------------------------------------------

        protected override Task Run(Args args)
        {
            var faction = m_Faction.Get(args);
            if (!faction) return Task.CompletedTask;
            
            var towards = m_Towards.Get(args);
            if (!towards) return Task.CompletedTask;
            
            faction.SetRelationshipStatus(towards, m_Status);
            if(m_ChangeBothWays.Get(args)) towards.SetRelationshipStatus(faction, m_Status);
            return Task.CompletedTask;
        }
    }
}