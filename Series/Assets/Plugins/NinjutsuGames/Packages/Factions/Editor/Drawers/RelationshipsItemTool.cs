using System.Collections.Generic;
using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEngine;

namespace NinjutsuGames.Editor.Factions
{
    public class RelationshipsItemTool : TPolymorphicItemTool
    {
        private static readonly IIcon DEFAULT_ICON = new IconTag(ColorTheme.Type.Yellow);

        
        // PROPERTIES: ----------------------------------------------------------------------------
        
        protected override List<string> CustomStyleSheetPaths => new List<string>
        {
            EditorPaths.VISUAL_SCRIPTING + "Instructions/StyleSheets/Instruction-Head",
            EditorPaths.VISUAL_SCRIPTING + "Instructions/StyleSheets/Instruction-Body"
        };

        protected override object Value => m_Property.GetValue<FactionRelationshipData>();

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public RelationshipsItemTool(IPolymorphicListTool parentTool, int index)
            : base(parentTool, index)
        { }
        
        protected override Texture2D GetIcon() => DEFAULT_ICON.Texture;

        public override string Title => Value.ToString();
    }
}