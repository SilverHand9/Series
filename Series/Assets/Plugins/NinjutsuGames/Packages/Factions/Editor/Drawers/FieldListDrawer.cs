using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomPropertyDrawer(typeof(FieldList))]
    public class FieldListDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new FieldListTool(property);
        }
    }
}