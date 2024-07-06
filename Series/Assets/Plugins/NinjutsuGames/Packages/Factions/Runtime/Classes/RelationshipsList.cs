using System;
using System.Collections.Generic;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class RelationshipsList
    {
        [SerializeReference]
        private FactionRelationshipData[] m_RelationshipDatas = Array.Empty<FactionRelationshipData>();

        public FactionRelationshipData[] List => m_RelationshipDatas;

        public void RemoveRelationships(List<FactionRelationshipData> remove)
        {
            var temp = new List<FactionRelationshipData>();
            foreach (var t in m_RelationshipDatas)
            {
                if (!remove.Contains(t))
                {
                    temp.Add(t);
                }
            }
            m_RelationshipDatas = temp.ToArray();
        }

        public void Add(FactionRelationshipData factionRelationshipData)
        {
            var temp = new List<FactionRelationshipData>(m_RelationshipDatas) { factionRelationshipData };
            m_RelationshipDatas = temp.ToArray();
        }


        public bool Exists(Faction otherFaction)
        {
            foreach (var data in m_RelationshipDatas)
            {
                if (data.faction == otherFaction)
                {
                    return true;
                }
            }

            return false;
        }
    }
}