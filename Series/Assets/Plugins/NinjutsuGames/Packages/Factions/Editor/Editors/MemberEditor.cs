using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomEditor(typeof(Member))]
    public class MemberEditor : UnityEditor.Editor
    {
        private const string TOOLTIP_IGNORE_REPUTATION = "If enabled, the member will not gain or lose reputation points, status will be based on the factions the member is part of";

        private const string USS_PATH =
            EditorPaths.COMMON + "Polymorphism/Properties/Styles/PropertyElement";
        
        private VisualElement root;
        
        // PAINT METHODS: -------------------------------------------------------------------------
        
        public override VisualElement CreateInspectorGUI()
        {
            root = new VisualElement();

            var sheets = StyleSheetUtils.Load(USS_PATH);
            foreach (var sheet in sheets) root.styleSheets.Add(sheet);
            
            root.Add(new SpaceSmaller());
            var ignoreReputation = serializedObject.FindProperty("m_IgnoreReputationPoints");
            var field = new PropertyField(ignoreReputation)
            {
                tooltip = TOOLTIP_IGNORE_REPUTATION
            };
            field.RegisterValueChangeCallback(evt =>
            {
                var m = target as Member;
                m.TriggerChangeEvent();
            });
            root.Add(field);
            root.Add(new SpaceSmaller());
            
            var playMode = EditorApplication.isPlayingOrWillChangePlaymode && !PrefabUtility.IsPartOfPrefabAsset(target);
            
            switch (playMode)
            {
                case true: 
                    root.Add(new FactionsView(target as Member));
                    root.Add(new FactionsView(target as Member, false, true));
                    break;
                case false: 
                    var startFactions = serializedObject.FindProperty("m_StartFactions");
                    root.Add(new PropertyField(startFactions));
                    break;
            }

            return root;
        }
    }
}