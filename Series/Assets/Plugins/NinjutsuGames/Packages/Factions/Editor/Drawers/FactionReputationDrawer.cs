using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{

    [CustomPropertyDrawer(typeof(FactionReputationList))]
    public class FactionReputationDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            
            // Use Reputation field
            var useReputationProperty = property.FindPropertyRelative("m_Enabled");
            var useReputationField = new PropertyField(useReputationProperty)
            {
                label = "Use Reputation"
            };
            root.Add(useReputationField);
            root.Add(new SpaceSmaller());

            var tool = new FactionReputationTool(property);

            tool.SetEnabled(useReputationProperty.boolValue);
            useReputationField.RegisterValueChangeCallback(changeEvent =>
            {
                tool.SetEnabled(changeEvent.changedProperty.boolValue);
            });
            root.Add(tool);
            return root;
        }
    }
}