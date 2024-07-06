using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomPropertyDrawer(typeof(RelationshipsList))]
    public class RelationshipsListDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            var instructionListTool = new RelationshipsListTool(
                property
            );
            root.Add(instructionListTool);
            return root;
        }
    }
}