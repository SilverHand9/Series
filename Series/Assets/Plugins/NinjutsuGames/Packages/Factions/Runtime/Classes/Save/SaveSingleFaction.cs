using System;
using System.Collections.Generic;
using GameCreator.Runtime.Variables;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]

    internal class SaveSingleFaction
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeReference] private List<NameVariable> m_Variables;
        [SerializeField] private SaveFactionRelationships m_RelationShips;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public List<NameVariable> Variables => m_Variables;
        public SaveFactionRelationships RelationShips => m_RelationShips;

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public SaveSingleFaction(NameVariableRuntime runtime, List<FactionRelationshipData> relationships)
        {
            m_Variables = new List<NameVariable>();
            foreach (var entry in runtime.Variables)
            {
                m_Variables.Add(entry.Value.Copy as NameVariable);
            }

            m_RelationShips = new ();
            foreach (var data in relationships)
            {
                m_RelationShips.Add(data.faction.name, data.stance);
            }
        }
    }
}