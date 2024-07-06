using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.UI;

namespace NinjutsuGames.Runtime.Factions.UnityUI
{
    [DisallowMultipleComponent]
    
    [AddComponentMenu("Game Creator/UI/Factions/Scoreboard Item UI")]
    [Icon(RuntimePaths.GIZMOS + "GizmoFactionUI.png")]
    
    [DefaultExecutionOrder(ApplicationManager.EXECUTION_ORDER_LAST_LATER)]
    public class ScoreboardItemUI : MonoBehaviour
    {
        [SerializeField] private Graphic m_AlternateBackground;
        [SerializeField] private FieldList fieldList;
        
        public FieldItem GetField(int index) => fieldList.Get(index);
        
        public void RefreshUI(Member member)
        {
            for(var i = 0; i < fieldList.Length; i++)
            {
                var field = fieldList.Get(i);
                field.Refresh(member.gameObject);
            }
        }

        public void SetAlternateBackground(bool b)
        {
            if(m_AlternateBackground) m_AlternateBackground.enabled = b;
        }
    }
}