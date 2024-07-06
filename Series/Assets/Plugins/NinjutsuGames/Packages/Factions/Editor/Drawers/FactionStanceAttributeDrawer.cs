using System.Collections.Generic;
using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomPropertyDrawer(typeof(FactionStanceAttribute))]
    public class FactionStanceAttributeDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();

            // Fetch all FactionStances from the FactionsRepository
            var stances = Settings.From<FactionsRepository>().Stances;
            var options = new List<string>();
            for (var i = 0; i < stances.Length; i++)
            {
                options.Add(stances.Get[i].Key);
            }

            // Create the dropdown
            var dropdown = new PopupField<string>(property.displayName, options, 0);
            AlignLabel.On(dropdown);
            
            // Set the default value if property is empty
            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = options[0]; // Default to the first option
                property.serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }
            dropdown.value = property.stringValue;

            // Update property value when dropdown value changes
            dropdown.RegisterValueChangedCallback(evt =>
            {
                property.stringValue = evt.newValue;
                property.serializedObject.ApplyModifiedProperties();
            });

            container.Add(dropdown);
            
            return container;
        }
    }
}