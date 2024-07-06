using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomPropertyDrawer(typeof(CompareFactionOrAny))]
    public class CompareFactionOrAnyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            var head = new VisualElement();
            var body = new VisualElement();

            var option = property.FindPropertyRelative("m_Option");
            var quest = property.FindPropertyRelative("m_Faction");
            
            var fieldOption = new PropertyField(option, property.displayName);
            
            var fieldQuest = new PropertyElement(
                quest.FindPropertyRelative(IPropertyDrawer.PROPERTY_NAME),
                string.Empty, true
            );
            
            head.Add(fieldOption);
            
            fieldOption.RegisterValueChangeCallback(changeEvent =>
            {
                body.Clear();
                if (changeEvent.changedProperty.intValue != 1) return;
                body.Add(fieldQuest);
                body.Bind(changeEvent.changedProperty.serializedObject);
            });

            if (option.intValue == 1)
            {
                body.Add(fieldQuest);
            }

            root.Add(head);
            root.Add(body);
            
            return root;
        }
    }
}