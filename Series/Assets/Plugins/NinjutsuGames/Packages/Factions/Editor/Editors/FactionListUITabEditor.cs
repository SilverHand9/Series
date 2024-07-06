using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions.UnityUI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{

    [CustomEditor(typeof(FactionListUITab))]
    public class FactionListUITabEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            var factionList = serializedObject.FindProperty("m_FactionListUI");
            var filterBy = serializedObject.FindProperty("m_FilterBy");

            root.Add(new PropertyField(factionList));
            root.Add(new SpaceSmaller());
            root.Add(new PropertyField(filterBy));
            
            var activeFilter = serializedObject.FindProperty("m_ActiveFilter");
            
            root.Add(new SpaceSmall());
            root.Add(new PropertyField(activeFilter));

            return root;
        }
    }
}