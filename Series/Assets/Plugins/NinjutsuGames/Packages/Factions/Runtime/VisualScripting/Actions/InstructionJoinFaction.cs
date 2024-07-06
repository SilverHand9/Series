using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Join Faction")]
    [Description("Makes target member join a Faction.")]

    [Category("Factions/Join Faction")]
    
    [Parameter("Target", "The Member that will join the Faction")]
    [Parameter("Faction", "The Faction to join")]

    [Image(typeof(IconFaction), ColorTheme.Type.Green)]
    
    [Keywords("Faction", "Join")]
    [Serializable]
    public class InstructionJoinFaction : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectPlayer.Create();

        [SerializeField] private PropertyGetFaction m_Faction = new();
        [SerializeField] private PropertyGetInteger m_InitialPoints = new(new GetDecimalConstantZero());
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"{m_Target} Join Faction {m_Faction}";

        // RUN METHOD: ----------------------------------------------------------------------------

        protected override async Task Run(Args args)
        {
            var member = m_Target.Get<Member>(args);
            if (!member) return;

            var faction = m_Faction.Get(args);
            if (!faction)
            {
                Debug.LogWarning($"Couldn't find faction in {m_Faction}.");
                return;
            }
            
            member.JoinFaction(faction, (int)m_InitialPoints.Get(args));
            await NextFrame();
        }
    }
}