using System;
using System.Collections.Generic;
using GameCreator.Runtime.Variables;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    internal class SaveGroupFactions
    {
        [Serializable]
        private class Group
        {
            [SerializeField] private string m_ID;
            [SerializeField] private SaveSingleFaction m_Data;

            public string ID => m_ID;
            public SaveSingleFaction Data => m_Data;
            
            public Group(string id, NameVariableRuntime runtime, List<FactionRelationshipData> relationships)
            {
                m_ID = id;
                m_Data = new SaveSingleFaction(runtime, relationships);
            }
        }
        
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private List<Group> m_Groups;

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public SaveGroupFactions(Dictionary<string, NameVariableRuntime> runtime, Dictionary<string, List<FactionRelationshipData>> relationships)
        {
            m_Groups = new List<Group>();
            
            foreach (var entry in runtime)
            {
                m_Groups.Add(new Group(entry.Key, entry.Value, relationships[entry.Key]));
            }
        }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public int Count()
        {
            return m_Groups?.Count ?? 0;
        }

        public string GetID(int index)
        {
            return m_Groups?[index].ID ?? string.Empty;
        }
        
        public SaveSingleFaction GetData(int index)
        {
            return m_Groups?[index].Data;
        }
    }
}