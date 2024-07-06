using System;
using System.Collections.Generic;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Collect Members")]
    [Description("Collects all Members from a Faction within a certain radius of a position")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Teal, typeof(OverlayListVariable))]
    
    [Category("Factions/Collect Members")]
    
    [Serializable]
    public class InstructionVariablesCollectFactionMembersFaction : TInstructionVariablesCollect
    {
        [NonSerialized] private List<ISpatialHash> m_Results = new();
        [SerializeField, Space(5)] private PropertyGetFaction m_Faction = GetFactionInstance.Create();

        protected override string TitleTarget => $"{m_Faction} members";
        
        protected override List<GameObject> Collect(Vector3 origin, float maxRadius, float minDistance)
        {
            var from = m_Faction.Get(Args.EMPTY);
            var result = new List<GameObject>();
            SpatialHashFactionMembers.Find(origin, maxRadius, m_Results);

            foreach (var element in m_Results)
            {
                if (Vector3.Distance(element.Position, origin) <= minDistance) continue;

                var member = element as Member;
                if (!member) continue;

                if (!member.IsInFaction(from)) continue;
                
                result.Add(member.gameObject);
            }

            return result;
        }
    }
}