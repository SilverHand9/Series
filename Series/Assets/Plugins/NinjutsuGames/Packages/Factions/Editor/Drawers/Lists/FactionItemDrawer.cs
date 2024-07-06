using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomPropertyDrawer(typeof(FactionItem))]
    public class FactionItemDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();

            var faction = property.FindPropertyRelative("m_Faction");
            var fieldFaction = new PropertyField(faction);
            
            var initialPoints = property.FindPropertyRelative("m_InitialPoints");
            var fieldInitialPoints = new PropertyField(initialPoints);
            
            root.Add(fieldFaction);
            root.Add(fieldInitialPoints);

            return root;
        }
    }
}