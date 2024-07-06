using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    public class FactionItemTool : TPolymorphicItemTool
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        protected override object Value => this.m_Property.GetValue<FactionItem>();
        
        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public FactionItemTool(IPolymorphicListTool parentTool, int index)
            : base(parentTool, index)
        { }
    }
}