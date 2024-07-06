using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions.UnityUI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomEditor(typeof(FactionUI))]
    public class FactionUIEditor : TFactionUIEditor
    {
        protected override string Message => string.Empty;

        protected override void CreateAdditionalProperties(VisualElement root)
        {
            var member = serializedObject.FindProperty("m_Member");
            var faction = serializedObject.FindProperty("m_Faction");

            root.Add(new SpaceSmallest());
            root.Add(new PropertyField(member));
            root.Add(new PropertyField(faction));
        }
    }
}