using System;
using GameCreator.Runtime.Common;
using UnityEditor;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class FactionsRepository : TRepository<FactionsRepository>
    {
        // REPOSITORY PROPERTIES: -----------------------------------------------------------------
        
        public override string RepositoryID => "factions.general";

        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private FactionStances m_Stances = new();
        [SerializeField] private GlobalFactions m_Factions = new();

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public GlobalFactions Factions => m_Factions;
        public FactionStances Stances => m_Stances;
        
        // EDITOR ENTER PLAYMODE: -----------------------------------------------------------------

#if UNITY_EDITOR
        
        [InitializeOnEnterPlayMode]
        public static void InitializeOnEnterPlayMode() => Instance = null;

#endif
    }
}