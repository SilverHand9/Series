using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions.UnityUI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomEditor(typeof(ScoreboardUI))]
    public class ScoreboardUIEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            var faction = serializedObject.FindProperty("m_Faction");
            root.Add(new PropertyField(faction));
            root.Add(new SpaceSmaller());
            
            var content = serializedObject.FindProperty("m_Content");
            var prefab = serializedObject.FindProperty("m_Prefab");
            
            root.Add(new SpaceSmaller());
            root.Add(new PropertyField(content));
            root.Add(new PropertyField(prefab));
            root.Add(new SpaceSmall());
            
            
            var sortDirection = serializedObject.FindProperty("m_SortDirection");
            root.Add(new PropertyField(sortDirection));
            
            var sortFieldIndex = serializedObject.FindProperty("m_SortFieldIndex");
            root.Add(new PropertyField(sortFieldIndex));

            return root;
        }
    }
}