using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NinjutsuGames.Runtime.Factions.UnityUI
{
    [AddComponentMenu("Game Creator/UI/Factions/Scoreboard UI Tab")]
    [Icon(RuntimePaths.GIZMOS + "GizmoFactionListUI.png")]
    
    [DefaultExecutionOrder(ApplicationManager.EXECUTION_ORDER_LAST_LATER)]
    
    public class ScoreboardUITab : MonoBehaviour, IPointerClickHandler, ISubmitHandler
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private ScoreboardUI m_ScoreboardUI;

        [SerializeField] private SortDirection m_SortDirection = SortDirection.Descending;
        [SerializeField] private int m_SortIndex = 0;
        [SerializeField] private GameObject m_ActiveIndex;
        [SerializeField] private GameObject m_DirectionArrow;
        
        // INITIALIZERS: --------------------------------------------------------------------------

        private void OnEnable()
        {
            if (!m_ScoreboardUI) return;
            m_ScoreboardUI.EventRefreshUI -= RefreshUI;
            m_ScoreboardUI.EventRefreshUI += RefreshUI;

            RefreshUI();
        }

        private void OnDisable()
        {
            if (!m_ScoreboardUI) return;
            m_ScoreboardUI.EventRefreshUI -= RefreshUI;
        }

        // CALLBACKS: -----------------------------------------------------------------------------
        
        public void OnPointerClick(PointerEventData data) => Filter();
        public void OnSubmit(BaseEventData data) => Filter();

        private void RefreshUI()
        {
            if (!m_ScoreboardUI) return;
            
            var currentFilter = m_ScoreboardUI.SortIndex;
            if(m_ActiveIndex) m_ActiveIndex.SetActive(m_SortIndex == currentFilter);
            if(m_DirectionArrow) m_DirectionArrow.SetActive(m_SortIndex == currentFilter);
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Filter()
        {
            if (!m_ScoreboardUI) return;
            if (m_ScoreboardUI.SortIndex == m_SortIndex)
            {
                // Toggle sort direction
                m_SortDirection = m_SortDirection == SortDirection.Ascending
                    ? SortDirection.Descending
                    : SortDirection.Ascending;
                
                // Flip the y scale of active direction based on the current direction
                if(m_DirectionArrow)
                {
                    m_DirectionArrow.transform.localScale = new Vector3(1,
                        m_SortDirection == SortDirection.Ascending ? -1 : 1, 1);
                }
            }
            
            m_ScoreboardUI.SetSortDirection(m_SortDirection);
            m_ScoreboardUI.SetSortIndex(m_SortIndex);
        }
    }
}