using System.Collections.Generic;
using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEditor.UIElements;
using UnityEngine;

namespace NinjutsuGames.Editor.Factions
{
    public class FactionStanceTool : TPolymorphicItemTool
    {
        private static readonly IIcon ICON_NODE = new IconBookmarkSolid(ColorTheme.Type.White);

        private const string TITLE = "{0}";
        
        // PROPERTIES: ----------------------------------------------------------------------------

        protected override object Value => null;

        public override string Title => string.Format(
            TITLE, 
            m_Property
                .FindPropertyRelative(FactionStanceDrawer.PROPERTY_KEY)
                .FindPropertyRelative(IdStringDrawer.NAME_STRING).stringValue
        );

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public FactionStanceTool(IPolymorphicListTool parentTool, int index)
            : base(parentTool, index)
        { }
        
        // IMPLEMENTATIONS: -----------------------------------------------------------------------

        protected override Texture2D GetIcon()
        {
            return ICON_NODE.Texture;
        }

        protected override void SetupBody()
        {
            var fieldBody = FactionStanceDrawer.MakePropertyGUI(m_Property);

            fieldBody.Bind(m_Property.serializedObject);
            fieldBody.RegisterCallback<SerializedPropertyChangeEvent>(_ =>
            {
                m_Property.serializedObject.Update();
                UpdateHead();
            });
            
            m_Body.Add(fieldBody);
            UpdateBody(false);
        }

        protected override void UpdateHead()
        {
            base.UpdateHead();
            UpdateIconColor();
        }

        protected override void SetupHead()
        {
            base.SetupHead();
            UpdateIconColor();
        }

        private void UpdateIconColor()
        {
            var color = m_Property.GetValue<FactionStance>().GetColor();
            m_HeadImage.tintColor = color;
        }
    }
}