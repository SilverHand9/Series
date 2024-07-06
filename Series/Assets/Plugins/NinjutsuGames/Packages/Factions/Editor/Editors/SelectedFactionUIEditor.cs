using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions.UnityUI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomEditor(typeof(SelectedFactionUI))]
    public class SelectedFactionUIEditor : TFactionUIEditor
    {
        protected override string Message => 
            "This component is automatically configured by the current selected Faction";

        protected override void CreateAdditionalProperties(VisualElement root)
        {
            base.CreateAdditionalProperties(root);

            var activeIfSelected = this.serializedObject.FindProperty("m_ActiveIfSelected");
            
            root.Add(new SpaceSmall());
            root.Add(new PropertyField(activeIfSelected));
        }
    }
}