using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class FactionList : TPolymorphicList<FactionItem>
    {
        [SerializeReference] private FactionItem[] m_Factions = Array.Empty<FactionItem>();
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override int Length => m_Factions.Length;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public FactionItem Get(int index) => m_Factions[index];
    }
}