using System;
using System.Collections.Generic;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Collect Members Status")]
    [Description("Collects all Members with an specific status within a certain radius of a position")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayListVariable))]
    
    [Category("Factions/Collect Members Status")]
    
    [Serializable]
    public class InstructionVariablesCollectFactionMembers : TInstructionVariablesCollect
    {
        [NonSerialized] private List<ISpatialHash> m_Results = new();
        [SerializeField, Space(5)] private PropertyGetGameObject m_Towards = GetGameObjectPlayer.Create();
        [SerializeField, Space(5)] private CompareFactionStatus m_Status = new();

        protected override string TitleTarget => $"Members {m_Status}";
        
        protected override List<GameObject> Collect(Vector3 origin, float maxRadius, float minDistance)
        {
            var from = m_Towards.Get<Member>(Args.EMPTY);
            var result = new List<GameObject>();
            SpatialHashFactionMembers.Find(origin, maxRadius, m_Results);

            foreach (var element in m_Results)
            {
                if (Vector3.Distance(element.Position, origin) <= minDistance) continue;

                var member = element as Member;
                if (!member) continue;

                if (!m_Status.Match(member.HighestStatusToMember(from).Key)) continue;
                
                result.Add(member.gameObject);
            }

            return result;
        }
    }
}