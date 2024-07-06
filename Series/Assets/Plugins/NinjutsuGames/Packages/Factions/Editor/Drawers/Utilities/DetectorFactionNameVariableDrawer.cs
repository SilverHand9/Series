using System;
using GameCreator.Editor.Variables;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomPropertyDrawer(typeof(DetectorFactionNameVariable))]
    public class DetectorFactionNameVariableDrawer : TDetectorNameVariableDrawer
    {
        protected override Type AssetType => typeof(Faction);
        protected override bool AllowSceneReferences => false;
        
        protected override TNamePickTool Tool(ObjectField field, SerializedProperty property)
        {
            FactionNamePickTool namePickTool = new (property);
            field.RegisterValueChangedCallback(_ => namePickTool.OnChangeAsset());
            
            return namePickTool;
        }
    }
}