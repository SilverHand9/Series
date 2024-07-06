using System;
using UnityEngine;
using UnityEngine.UI;

namespace NinjutsuGames.Runtime.Factions.UnityUI
{
    [Serializable]
    public class InteractionFactionUI
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private Button m_ButtonJoin;
        [SerializeField] private Button m_ButtonLeave;
        [SerializeField] private Selectable m_SelectFaction;

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private TFactionUI m_FactionUI;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Setup(TFactionUI factionUI)
        {
            m_FactionUI = factionUI;

            if (m_SelectFaction)
            {
                SelectableHelper.Register(m_SelectFaction, OnSelect, null);
            }
            
            if (m_ButtonJoin)
            {
                m_ButtonJoin.onClick.RemoveListener(OnJoin);
                m_ButtonJoin.onClick.AddListener(OnJoin);
            }
            
            if (m_ButtonLeave)
            {
                m_ButtonLeave.onClick.RemoveListener(OnLeave);
                m_ButtonLeave.onClick.AddListener(OnLeave);
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private void OnLeave()
        {
            if(m_FactionUI.Member) m_FactionUI.Member.LeaveFaction(m_FactionUI.Faction);
        }
        
        private void OnJoin()
        {
            if(m_FactionUI.Member) m_FactionUI.Member.JoinFaction(m_FactionUI.Faction);
        }
        
        private void OnSelect()
        {
            m_FactionUI.Select();
        }
    }
}