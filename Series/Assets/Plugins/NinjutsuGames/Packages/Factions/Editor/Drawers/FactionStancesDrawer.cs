using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomPropertyDrawer(typeof(FactionStances))]
    public class FactionStancesDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new FactionStancesTool(property);
        }
    }
}