using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions.UnityUI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomEditor(typeof(ScoreboardItemUI))]
    public class ScoreboardItemUIEditor : UnityEditor.Editor
    {
        private static readonly StyleLength DefaultMarginTop = new(5);

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement
            {
                style =
                {
                    marginTop = DefaultMarginTop
                }
            };
            
            var alternateBackground = serializedObject.FindProperty("m_AlternateBackground");
            root.Add(new PropertyField(alternateBackground));
            
            root.Add(new SpaceSmall());
            
            var fields = serializedObject.FindProperty("fieldList");
            root.Add(new PropertyField(fields));

            return root;
        }
    }
}