using System;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class ReputationThreshold : TPolymorphicItem<ReputationThreshold>
    {
        [SerializeField] private string m_Stance;
        [SerializeField] private PropertyGetInteger m_PointsRequired = new(new GetDecimalConstantZero());

        public ReputationThreshold(string factionStance, int points)
        {
            m_Stance = factionStance;
            m_PointsRequired = points == 0 ? new PropertyGetInteger(new GetDecimalConstantZero()) : new PropertyGetInteger(points);
        }

        public string Stance => m_Stance;
        public int PointsRequired => (int)m_PointsRequired.Get(Args.EMPTY);

        public override string ToString() => $"{m_Stance} ({m_PointsRequired})";
    }
}