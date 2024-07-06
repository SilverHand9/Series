using GameCreator.Editor.Common;
using GameCreator.Editor.Variables;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
	[CustomPropertyDrawer(typeof(FieldGetFactionName))]
    public class FieldGetFactionNameDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            
            var variable = property.FindPropertyRelative("m_Variable");
            var typeID = property.FindPropertyRelative("m_TypeID");

            var fieldVariable = new ObjectField(variable.displayName)
            {
                allowSceneObjects = false,
                objectType = typeof(Faction),
                bindingPath = variable.propertyPath
            };
            
            fieldVariable.AddToClassList(AlignLabel.CLASS_UNITY_ALIGN_LABEL);

            var typeIDStr = typeID.FindPropertyRelative(IdStringDrawer.NAME_STRING);
            var typeIDValue = new IdString(typeIDStr.stringValue);
            
            var toolPickName = new FactionNamePickTool(
                property,
                typeIDValue,
                true
            );

            fieldVariable.RegisterValueChangedCallback(_ => toolPickName.OnChangeAsset());
            
            root.Add(fieldVariable);
            root.Add(toolPickName);

            return root;
        }
    }
}