using System.Collections.Generic;
using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    public class FactionStancesTool : TPolymorphicListTool
    {
        private const string NAME_BUTTON_ADD = "GC-Instruction-List-Foot-Add";
        
        private static readonly IIcon ICON_ADD = new IconBookmarkSolid(ColorTheme.Type.TextLight);
        
        // MEMBERS: -------------------------------------------------------------------------------

        private Button m_ButtonAdd;

        // PROPERTIES: ----------------------------------------------------------------------------

        protected override string ElementNameHead => "GC-Instruction-List-Head";
        protected override string ElementNameBody => "GC-Instruction-List-Body";
        protected override string ElementNameFoot => "GC-Instruction-List-Foot";

        protected override List<string> CustomStyleSheetPaths => new List<string>
        {
            EditorPaths.VISUAL_SCRIPTING + "Instructions/StyleSheets/Instructions-List"
        };

        public override bool AllowReordering => true;
        public override bool AllowDuplicating => true;
        public override bool AllowDeleting  => true;
        public override bool AllowContextMenu => false;
        public override bool AllowCopyPaste => false;
        public override bool AllowInsertion => false;
        public override bool AllowBreakpoint => false;
        public override bool AllowDisable => false;
        public override bool AllowDocumentation => false;

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public FactionStancesTool(SerializedProperty property) : base(property, "m_Stances")
        {
            SerializedObject.Update();
        }

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected override VisualElement MakeItemTool(int index)
        {
            return new FactionStanceTool(this, index);
        }

        protected override void SetupHead()
        { }

        protected override void SetupFoot()
        {
            base.SetupFoot();
        
            m_ButtonAdd = new Button { name = NAME_BUTTON_ADD };
        
            m_ButtonAdd.Add(new Image { image = ICON_ADD.Texture });
            m_ButtonAdd.Add(new Label { text = "Add Faction Status..." });
        
            m_ButtonAdd.clicked += () =>
            {
                SerializedObject.Update();
            
                int insertIndex = PropertyList.arraySize;
                PropertyList.InsertArrayElementAtIndex(insertIndex);
                PropertyList
                    .GetArrayElementAtIndex(insertIndex)
                    .SetValue(new FactionStance(new IdString("NewStatus"), GetColorColorsMagenta.Create, new PropertyGetInteger(new GetDecimalConstantZero())));
        
                SerializationUtils.ApplyUnregisteredSerialization(SerializedObject);
        
                int size = PropertyList.arraySize;
                ExecuteEventChangeSize(size);
            
                using ChangeEvent<int> changeEvent = ChangeEvent<int>.GetPooled(size, size);
                changeEvent.target = this;
                SendEvent(changeEvent);
            
                Refresh();
            };
        
            m_Foot.Add(m_ButtonAdd);
        }
    }
}