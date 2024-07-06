using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomPropertyDrawer(typeof(ReputationThreshold))]
    public class ReputationThresholdDrawer : PropertyDrawer
    {
        public const string PROPERTY_STANCE = "m_Stance";
        public const string PROPERTY_POINTS = "m_PointsRequired";

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return MakePropertyGUI(property);
        }

        public static VisualElement MakePropertyGUI(SerializedProperty property)
        {
            var points = property.FindPropertyRelative(PROPERTY_POINTS);

            var root = new VisualElement();
            root.Add(new PropertyField(points));

            return root;
        }
    }
}