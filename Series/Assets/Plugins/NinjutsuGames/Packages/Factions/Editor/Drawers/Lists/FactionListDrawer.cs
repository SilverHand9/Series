using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomPropertyDrawer(typeof(FactionList))]
    public class FactionListDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new FactionListTool(property);
        }
    }
}