using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Change Faction Points")]
    [Description("Change Faction Points to a Member.")]

    [Category("Factions/Change Faction Points")]
    
    [Parameter("Target", "The Member to add the points.")]
    [Parameter("Faction", "The Faction to join")]
    [Parameter("Change", "The value changed.")]

    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]
    
    [Keywords("Faction", "Join")]
    [Serializable]
    public class InstructionChangeFactionPoints : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectPlayer.Create();

        [SerializeField] private PropertyGetFaction m_Faction = new();
        [SerializeField] private ChangeInteger m_Change = new(1);
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"{m_Target} [{m_Faction} Faction Points] {m_Change}";

        // RUN METHOD: ----------------------------------------------------------------------------

        protected override Task Run(Args args)
        {
            var member = m_Target.Get<Member>(args);
            if (!member) return Task.CompletedTask;

            var faction = m_Faction.Get(args);
            if (!faction) return Task.CompletedTask;

            var points = m_Change.Get(member.GetReputationPoints(faction), args);
            member.SetReputationPoints(faction, points);
            
            return Task.CompletedTask;
        }
    }
}