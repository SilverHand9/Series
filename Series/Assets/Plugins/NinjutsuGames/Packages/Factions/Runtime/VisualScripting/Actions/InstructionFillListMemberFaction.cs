using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Fill List with Members")]
    [Description("Fill a list with all Members from a Faction")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Green, typeof(OverlayListVariable))]
    
    [Category("Factions/Fill List with Members")]
    
    [Serializable]
    public class InstructionFillListMemberFaction : Instruction
    {
        [SerializeField] private PropertyGetFaction m_Faction = GetFactionInstance.Create();

        [SerializeField]  private CollectorListVariable m_StoreIn = new();
        
        public override string Title => $"Fill {m_StoreIn} with {m_Faction} members";
        
        protected override Task Run(Args args)
        {
            var faction = m_Faction.Get(args);
            if (!faction) return DefaultResult;
            
            var members = faction.GetMembers();
            var elements = new List<GameObject>();
            foreach (var member in members)
            {
                elements.Add(member.gameObject);
            }
            m_StoreIn.Fill(elements.ToArray(), args);
            return DefaultResult;
        }
    }
}