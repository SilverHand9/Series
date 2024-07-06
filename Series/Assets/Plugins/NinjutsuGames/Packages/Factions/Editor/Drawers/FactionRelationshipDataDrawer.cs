using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    // [CustomPropertyDrawer(typeof(FactionRelationshipData))]
    public class FactionRelationshipDataDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            var faction = property.FindPropertyRelative("faction");
            var relationshipStatus = property.FindPropertyRelative("relationshipStatus");
            
            var relationshipElement = new VisualElement();
            relationshipElement.style.flexDirection = FlexDirection.Row;
            relationshipElement.style.alignItems = Align.Center;

            var label = new Label($"Status towards {faction.FindPropertyRelative("name")}")
            {
                style =
                {
                    unityTextAlign = TextAnchor.MiddleLeft,
                    flexGrow = 1
                }
            };
            relationshipElement.Add(label);
            relationshipElement.Add(new PropertyField(relationshipStatus, ""));

            var thisFaction = faction.objectReferenceValue as Faction;

            // var button = GlobalFactionsDrawer.CreateRelationshipButton(thisFaction.GetRelationshipStatus(faction), thisFaction, faction);
            // relationshipElement.Add(button);
            
            root.Add(relationshipElement);
            return root;
        }
    }
}