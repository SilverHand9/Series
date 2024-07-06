using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions.UnityUI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{

    [CustomPropertyDrawer(typeof(ReputationFactionUI), true)]
    public class ReputationFactionUIDrawer : TBoxDrawer
    {
        protected override string Name(SerializedProperty property) => "Reputation Elements";

        protected override void CreatePropertyContent(VisualElement container, SerializedProperty property)
        {
            var activeIfReputationEnabled = property.FindPropertyRelative("m_ActiveIfReputationEnabled");
            var status = property.FindPropertyRelative("m_Status");
            var statusColor = property.FindPropertyRelative("m_StatusColor");
            var points = property.FindPropertyRelative("m_Points");
            var pointsFormat = property.FindPropertyRelative("m_PointsFormat");
            var pointsFormatType = property.FindPropertyRelative("m_PointsFormatType");
            var pointsRequired = property.FindPropertyRelative("m_PointsRequired");
            var maxPoints = property.FindPropertyRelative("m_MaxPoints");
            var reputationProgress = property.FindPropertyRelative("m_ReputationProgress");
            var reputationProgressType = property.FindPropertyRelative("m_ReputationProgressType");

            container.Add(new PropertyField(activeIfReputationEnabled));
            container.Add(new SpaceSmall());
            container.Add(new PropertyField(status));
            container.Add(new PropertyField(statusColor));

            container.Add(new SpaceSmall());
            container.Add(new PropertyField(points));
            container.Add(new PropertyField(pointsFormat));
            container.Add(new PropertyField(pointsFormatType));

            container.Add(new SpaceSmall());
            container.Add(new PropertyField(pointsRequired));
            container.Add(new PropertyField(maxPoints));

            container.Add(new SpaceSmall());
            container.Add(new PropertyField(reputationProgress));
            container.Add(new PropertyField(reputationProgressType));
        }
    }
}