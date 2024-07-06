using System.Collections.Generic;
using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    public class ReputationThresholdTool : TPolymorphicItemTool
    {
        private static readonly IIcon ICON_NODE = new IconBookmarkSolid(ColorTheme.Type.White);
        private Label pointsLabel;

        private const string TITLE = "{0}";
        
        // PROPERTIES: ----------------------------------------------------------------------------
        protected override object Value => null;

        public override string Title => string.Format(
            TITLE, 
            m_Property.FindPropertyRelative(ReputationThresholdDrawer.PROPERTY_STANCE).stringValue
        );

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public ReputationThresholdTool(IPolymorphicListTool parentTool, int index)
            : base(parentTool, index)
        { }
        
        // IMPLEMENTATIONS: -----------------------------------------------------------------------

        protected override Texture2D GetIcon()
        {
            return ICON_NODE.Texture;
        }

        protected override void SetupBody()
        {
            var fieldBody = ReputationThresholdDrawer.MakePropertyGUI(m_Property);

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
            pointsLabel = new Label
            {
                style =
                {
                    unityTextAlign = TextAnchor.MiddleRight,
                    color = ColorTheme.Get(ColorTheme.Type.TextLight),
                    paddingRight = 8,
                }
            };
            m_HeadButton.Add(pointsLabel);
            UpdateIconColor();
        }

        private void UpdateIconColor()
        {
            var threshold = m_Property.GetValue<ReputationThreshold>();

            if(pointsLabel != null) pointsLabel.text = $"({threshold.PointsRequired.ToString()}) ";
            var color = FactionStance.GetStance(threshold.Stance).GetColor();
            m_HeadImage.tintColor = color;
        }
    }
}