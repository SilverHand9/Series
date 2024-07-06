using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Leave Faction")]
    [Description("Makes target member leave a Faction.")]

    [Category("Factions/Leave Faction")]
    
    [Parameter("Target", "The Member that will leave the Faction")]
    [Parameter("Faction", "The Faction to leave")]

    [Image(typeof(IconFaction), ColorTheme.Type.Red)] 
    
    [Keywords("Faction", "Join")]
    [Serializable]
    public class InstructionLeaveFaction : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectPlayer.Create();

        [SerializeField] private PropertyGetFaction m_Faction = new();
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"{m_Target} Leave Faction {m_Faction}";

        // RUN METHOD: ----------------------------------------------------------------------------

        protected override Task Run(Args args)
        {
            var member = m_Target.Get<Member>(args);
            if (member == null) return Task.CompletedTask;

            var faction = m_Faction.Get(args);
            if (faction == null) return Task.CompletedTask;
            
            member.LeaveFaction(faction);
            return Task.CompletedTask;
        }
    }
}