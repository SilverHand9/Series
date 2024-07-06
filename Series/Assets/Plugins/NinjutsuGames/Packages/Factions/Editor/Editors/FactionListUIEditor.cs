using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions.UnityUI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomEditor(typeof(FactionListUI))]
    public class FactionListUIEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            var journal = serializedObject.FindProperty("m_Member");
            var filter = serializedObject.FindProperty("m_Filter");
            
            root.Add(new PropertyField(journal));
            root.Add(new SpaceSmaller());
            root.Add(new PropertyField(filter));
            
            var content = serializedObject.FindProperty("m_Content");
            var prefab = serializedObject.FindProperty("m_Prefab");
            
            root.Add(new SpaceSmaller());
            root.Add(new PropertyField(content));
            root.Add(new PropertyField(prefab));

            return root;
        }
    }
}