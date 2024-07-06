using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NinjutsuGames.Runtime.Factions.UnityUI
{
    [AddComponentMenu("Game Creator/UI/Factions/Faction List UI Tab")]
    [Icon(RuntimePaths.GIZMOS + "GizmoFactionListUI.png")]
    
    [DefaultExecutionOrder(ApplicationManager.EXECUTION_ORDER_LAST_LATER)]
    
    public class FactionListUITab : MonoBehaviour, IPointerClickHandler, ISubmitHandler
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private FactionListUI m_FactionListUI;

        [SerializeField] private StateFlags m_FilterBy = StateFlags.Active;
        [SerializeField] private GameObject m_ActiveFilter;
        
        // INITIALIZERS: --------------------------------------------------------------------------

        private void OnEnable()
        {
            if (!m_FactionListUI) return;
            m_FactionListUI.EventRefreshUI -= RefreshUI;
            m_FactionListUI.EventRefreshUI += RefreshUI;

            RefreshUI();
        }

        private void OnDisable()
        {
            if (!m_FactionListUI) return;
            m_FactionListUI.EventRefreshUI -= RefreshUI;
        }

        // CALLBACKS: -----------------------------------------------------------------------------
        
        public void OnPointerClick(PointerEventData data) => Filter();
        public void OnSubmit(BaseEventData data) => Filter();

        private void RefreshUI()
        {
            if (!m_FactionListUI) return;
            if (!m_ActiveFilter) return;
            
            var currentFilter = m_FactionListUI.Show;
            m_ActiveFilter.SetActive(m_FilterBy == currentFilter);
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Filter()
        {
            if (!m_FactionListUI) return;
            if (m_FactionListUI.Show == m_FilterBy) return;
            
            FactionItemUI.DeselectFactionUI();
            
            m_FactionListUI.Show = m_FilterBy;
        }
    }
}