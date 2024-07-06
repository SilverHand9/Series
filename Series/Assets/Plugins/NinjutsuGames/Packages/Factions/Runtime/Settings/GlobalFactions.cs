using System;
using System.Collections.Generic;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class GlobalFactions
    {
         // MEMBERS: -------------------------------------------------------------------------------
        
        [NonSerialized] private Dictionary<IdString, Faction> m_MapFactions;
        
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private Faction[] m_Factions = Array.Empty<Faction>();
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public Faction[] Factions => m_Factions;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public Faction Get(IdString itemID)
        {
            RequireInitialize();
            return m_MapFactions.GetValueOrDefault(itemID);
        }

        public void RequireInitialize()
        {
            if (m_MapFactions != null) return;
            
            m_MapFactions = new Dictionary<IdString, Faction>();

            foreach (var faction in m_Factions)
            {
                m_MapFactions[faction.UniqueID] = faction;
            }
        }

        public int GetIndex(Faction faction)
        {
            RequireInitialize();
            for (var i = 0; i < m_Factions.Length; ++i)
            {
                if (m_Factions[i] == faction) return i;
            }

            return -1;
        }

        public Faction GetByString(string name)
        {
            var targetName = name.ToLowerInvariant();
            RequireInitialize();
            foreach (var faction in m_Factions)
            {
                if (faction.Name.ToLowerInvariant().Equals(targetName)) return faction;
                if (faction.GetTitle(Args.EMPTY).ToLowerInvariant().Equals(targetName)) return faction;
            }

            return null;
        }
    }
}