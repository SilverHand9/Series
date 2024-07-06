using System;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions.UnityUI
{

    [Serializable]
    public class ActiveFactionUI : TActiveUI
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        
        [SerializeField] private GameObject m_ActiveIfSelected;
        
        // MEMBERS: -------------------------------------------------------------------------------
        
        [NonSerialized] private Faction m_Faction;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void OnEnable()
        {
            FactionItemUI.EventSelect -= OnSelect;
            FactionItemUI.EventSelect += OnSelect;

            OnSelect(null, FactionItemUI.UI_LastFactionSelected);
        }
        
        public void OnDisable()
        {
            FactionItemUI.EventSelect -= OnSelect;
        }
        
        public void Refresh(Faction faction)
        {
            m_Faction = faction;
            OnSelect(null, FactionItemUI.UI_LastFactionSelected);
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void OnSelect(Member member, Faction faction)
        {
            if (!m_ActiveIfSelected) return;
            
            m_ActiveIfSelected.SetActive(
                faction && 
                m_Faction &&
                m_Faction.Equals(faction)
            );
        }
    }
}