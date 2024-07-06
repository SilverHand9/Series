using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Set Faction")]
    [Description("Sets a Faction value equal to another one")]

    [Category("Factions/Set Faction")]

    [Parameter("Set", "Where the value is set")]
    [Parameter("From", "The value that is set")]

    [Keywords("Change", "Faction", "Variable", "Asset")]
    [Image(typeof(IconFaction), ColorTheme.Type.Yellow)]
    
    [Serializable]
    public class InstructionSetFaction : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] 
        private PropertySetFaction m_Set = SetFactionNone.Create;
        
        [SerializeField]
        private PropertyGetFaction m_From = new();

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public override string Title => $"Set {m_Set} = {m_From}";

        // RUN METHOD: ----------------------------------------------------------------------------
        
        protected override Task Run(Args args)
        {
            var value = m_From.Get(args);
            m_Set.Set(value, args);

            return DefaultResult;
        }
    }
}