using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Common.UnityUI;
using UnityEngine;
using UnityEngine.UI;

namespace NinjutsuGames.Runtime.Factions.UnityUI
{

    [Serializable]
    public class ReputationFactionUI
    {
        public enum ProgressType
        {
            NextThreshold,
            MaxPoints
        }
        public enum PointsFormatType
        {
            Points,
            PointsOfNextThreshold,
            PointsOfMaxPoints,
        }
        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        
        [SerializeField] private GameObject m_ActiveIfReputationEnabled;
        
        [SerializeField] private TextReference m_Status = new();
        [SerializeField] private Graphic m_StatusColor;
        
        [SerializeField] private TextReference m_Points = new();
        [SerializeField] private string m_PointsFormat = "({0} of {1})";
        [SerializeField] private PointsFormatType m_PointsFormatType = PointsFormatType.PointsOfMaxPoints;
        
        [SerializeField] private TextReference m_PointsRequired = new();
        [SerializeField] private TextReference m_MaxPoints = new();
        [SerializeField] private Image m_ReputationProgress;
        [SerializeField] private ProgressType m_ReputationProgressType = ProgressType.MaxPoints;

        
        // MEMBERS: -------------------------------------------------------------------------------
        
        [NonSerialized] private Member m_Member;
        [NonSerialized] private Faction m_Faction;
        
        ~ReputationFactionUI()
        {
            if (!m_Member) return;
            m_Member.EventPointsChanged -= OnPointsChanged;
            m_Member.EventReputationStatusChange -= OnReputationStatusChanged;
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public void Refresh(Member member, Faction faction)
        {
            if(!faction)
            {
                ToggleObjects(false);
                return;
            }
            
            m_Faction = faction;
            m_Member = member;
            
            UpdateReputationProgress();
            UpdatePoints();
            UpdateStatus();
            
            ToggleObjects(faction.ReputationEnabled);
            
            if (!m_Member) return;
            m_Member.EventPointsChanged -= OnPointsChanged;
            m_Member.EventPointsChanged += OnPointsChanged;
            
            m_Member.EventReputationStatusChange -= OnReputationStatusChanged;
            m_Member.EventReputationStatusChange += OnReputationStatusChanged;
        }
        
        private void UpdateReputationProgress()
        {
            if (!m_ReputationProgress) return;
            if (!m_Faction) return;
            if (!m_Member) return;
            if(!m_Faction.ReputationEnabled) return;
            
            var progress = m_ReputationProgressType == ProgressType.MaxPoints ? m_Member.ReputationProgressToFaction(m_Faction) : m_Member.ReputationProgressToNextThreshold(m_Faction);
            m_ReputationProgress.fillAmount = progress;
        }

        private void UpdatePoints()
        {
            if (!m_Faction) return;
            if (!m_Member) return;
            if(!m_Faction.ReputationEnabled) return;

            var points = m_Member.GetReputationPoints(m_Faction);
            m_Points.Text = GetPointsText(points);
            m_PointsRequired.Text = m_Faction.Reputation.GetNextThreshold(points).ToString();
            m_MaxPoints.Text = m_Faction.Reputation.MaxPoints.ToString();
        }

        private string GetPointsText(int points)
        {
            return m_PointsFormatType switch
            {
                PointsFormatType.Points => points.ToString(),
                PointsFormatType.PointsOfNextThreshold => string.Format(m_PointsFormat, points,
                    m_Faction.Reputation.GetNextThreshold(points)),
                PointsFormatType.PointsOfMaxPoints => string.Format(m_PointsFormat, points,
                    m_Faction.Reputation.MaxPoints),
                _ => points.ToString()
            };
        }

        private void UpdateStatus()
        {
            if (!m_Member) return;
            var stance = m_Member.HighestStatusToFaction(m_Faction);
            m_Status.Text = stance.Key;
            if(m_StatusColor) m_StatusColor.color = stance.GetColor();
        }

        private void OnReputationStatusChanged()
        {
            UpdateReputationProgress();
            UpdatePoints();
            UpdateStatus();
        }

        private void OnPointsChanged()
        {
            UpdateReputationProgress();
            UpdatePoints();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void ToggleObjects(bool factionReputationEnabled)
        {
            if (m_ActiveIfReputationEnabled) m_ActiveIfReputationEnabled.SetActive(factionReputationEnabled);
        }
    }
}