using System.Collections.Generic;
using GameCreator.Editor.Common;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    public class RelationshipsListTool : TPolymorphicListTool
    {
        private const string NAME_BUTTON_ADD = "GC-Instruction-List-Foot-Add";
        
        private static readonly IIcon ICON_ADD = new IconCharacter(ColorTheme.Type.TextLight);

        // MEMBERS: -------------------------------------------------------------------------------

        
        // PROPERTIES: ----------------------------------------------------------------------------

        protected override string ElementNameHead => "GC-Instruction-List-Head";
        protected override string ElementNameBody => "GC-Instruction-List-Body";
        protected override string ElementNameFoot => "GC-Instruction-List-Foot";

        protected override List<string> CustomStyleSheetPaths => new()
        {
            EditorPaths.VISUAL_SCRIPTING + "Instructions/StyleSheets/Instructions-List"
        };

        public override bool AllowReordering => false;
        public override bool AllowDuplicating => false;
        public override bool AllowDeleting  => false;
        public override bool AllowContextMenu => true;
        public override bool AllowCopyPaste => false;
        public override bool AllowInsertion => false;
        public override bool AllowBreakpoint => false;
        public override bool AllowDisable => false;
        public override bool AllowDocumentation => true;
        
        private BaseActions Actions => SerializedObject?.targetObject as BaseActions;

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public RelationshipsListTool(SerializedProperty property) : base(property, "m_RelationshipDatas")
        {
            SerializedObject.Update();
        }

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected override VisualElement MakeItemTool(int index)
        {
            return new RelationshipsItemTool(this, index);
        }

        protected override void SetupHead()
        { }

        protected override void SetupFoot()
        {
        }
    }
}