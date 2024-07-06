using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomPropertyDrawer(typeof(FactionStance))]
    public class FactionStanceDrawer : PropertyDrawer
    {
        public const string PROPERTY_KEY = "m_Key";
        public const string PROPERTY_COLOR = "m_Color";
        public const string PROPERTY_POINTS = "m_PointsRequired";

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return MakePropertyGUI(property);
        }

        public static VisualElement MakePropertyGUI(SerializedProperty property)
        {
            var key = property.FindPropertyRelative(PROPERTY_KEY);
            var color = property.FindPropertyRelative(PROPERTY_COLOR);
            var pointsRequired = property.FindPropertyRelative(PROPERTY_POINTS);

            var root = new VisualElement();
            root.Add(new PropertyField(key));
            root.Add(new PropertyField(color));
            root.Add(new PropertyField(pointsRequired));

            return root;
        }
    }
}