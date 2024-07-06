using System;
using GameCreator.Runtime.Variables;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions.UnityUI.Classes
{
    [Serializable]
    public class FilterFactions
    {
        private enum Filter
        {
            None = 0,
            InLocalList = 1,
            InGlobalList = 2
        }
        
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private StateFlags m_Show = StateFlags.Active;
        [SerializeField] private bool m_ShowHidden;
        
        [SerializeField] private Filter m_Filter = Filter.None;
        [SerializeField] private LocalListVariables m_LocalList;
        [SerializeField] private GlobalListVariables m_GlobalList;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public StateFlags Show
        {
            get => m_Show;
            set => m_Show = value;
        }
   
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public bool Check(Faction faction, Member member)
        {
            if (!faction) return false;
            
            if(member)
            {
                var state = member.IsInFaction(faction) ? StateFlags.Active : StateFlags.Inactive;
                if ((m_Show & state) == 0) return false;
            }
            
            if (faction.Type != FactionType.Normal && !m_ShowHidden) return false;

            return m_Filter switch
            {
                Filter.None => true,
                Filter.InLocalList => m_LocalList &&
                                      m_LocalList.Contains(faction),
                Filter.InGlobalList => m_GlobalList && 
                                       m_GlobalList.Contains(faction),
                _ => false
            };
        }
    }
}