using System.Collections.Generic;
using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    public class FactionReputationTool : TPolymorphicListTool
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        protected override string ElementNameHead => "GC-Instruction-List-Head";
        protected override string ElementNameBody => "GC-Instruction-List-Body";
        protected override string ElementNameFoot => "GC-Instruction-List-Foot";

        protected override List<string> CustomStyleSheetPaths => new List<string>
        {
            EditorPaths.VISUAL_SCRIPTING + "Instructions/StyleSheets/Instructions-List"
        };

        public override bool AllowReordering => false;
        public override bool AllowDuplicating => false;
        public override bool AllowDeleting  => false;
        public override bool AllowContextMenu => false;
        public override bool AllowCopyPaste => false;
        public override bool AllowInsertion => false;
        public override bool AllowBreakpoint => false;
        public override bool AllowDisable => false;
        public override bool AllowDocumentation => false;

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public FactionReputationTool(SerializedProperty property) : base(property, "m_Thresholds")
        {
            SerializedObject.Update();
        }

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected override VisualElement MakeItemTool(int index)
        {
            return new ReputationThresholdTool(this, index);
        }

        protected override void SetupHead()
        { }

        protected override void SetupFoot()
        { }
    }
}