using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions.UnityUI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{

    [CustomEditor(typeof(ScoreboardUITab))]
    public class ScoreboardUITabEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            var scoreboardUI = serializedObject.FindProperty("m_ScoreboardUI");
            var sortDirection = serializedObject.FindProperty("m_SortDirection");
            var sortIndex = serializedObject.FindProperty("m_SortIndex");

            root.Add(new PropertyField(scoreboardUI));
            root.Add(new SpaceSmaller());
            root.Add(new PropertyField(sortDirection));
            root.Add(new PropertyField(sortIndex));
            
            var activeIndex = serializedObject.FindProperty("m_ActiveIndex");
            var directionArrow = serializedObject.FindProperty("m_DirectionArrow");
            
            root.Add(new SpaceSmall());
            root.Add(new PropertyField(activeIndex));
            root.Add(new PropertyField(directionArrow));

            return root;
        }
    }
}