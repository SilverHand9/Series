using System;
using System.Collections.Generic;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class FactionReputationList : TPolymorphicList<ReputationThreshold>
    {
        [SerializeField] private bool m_Enabled = true;
        [SerializeReference] private ReputationThreshold[] m_Thresholds = Array.Empty<ReputationThreshold>();
        
        public override int Length => m_Thresholds.Length;
        public ReputationThreshold[] Thresholds => m_Thresholds;
        public bool Enabled => m_Enabled;
        
        /// <summary>
        /// Returns the maximum number of points that can be achieved.
        /// </summary>
        /// <returns></returns>
        public int MaxPoints => m_Thresholds.Length > 0 ? m_Thresholds[^1].PointsRequired : 0;

        /// <summary>
        /// Returns the reputation threshold with the specified key in the list.
        /// </summary>
        /// <param name="key">The key of the reputation threshold to search for.</param>
        /// <returns>The reputation threshold with the specified key, if found; otherwise, null.</returns>
        public ReputationThreshold Get(string key)
        {
            foreach (var t in m_Thresholds)
            {
                if (t.Stance.ToLowerInvariant().Equals(key.ToLowerInvariant())) return t;
            }

            return null;
        }

        /// <summary>
        /// Returns the index of the first occurrence of a reputation threshold with the specified key in the list.
        /// </summary>
        /// <param name="key">The key of the reputation threshold to search for.</param>
        /// <returns>The zero-based index of the first occurrence of the reputation threshold with the specified key, if found; otherwise, -1.</returns>
        public int IndexOf(string key)
        {
            for (var i = 0; i < m_Thresholds.Length; ++i)
            {
                if (m_Thresholds[i].Stance.ToLowerInvariant().Equals(key.ToLowerInvariant())) return i;
            }

            return -1;
        }
        
        public ReputationThreshold Get(int index)
        {
            return m_Thresholds[index];
        }
        
        public void Set(int index, ReputationThreshold threshold)
        {
            m_Thresholds[index] = threshold;
        }

        /// <summary>
        /// Adds a reputation threshold to the list.
        /// </summary>
        /// <param name="threshold">The reputation threshold to add.</param>
        public void Add(ReputationThreshold threshold)
        {
            Array.Resize(ref m_Thresholds, m_Thresholds.Length + 1);
            m_Thresholds[^1] = threshold;
        }

        /// <summary>
        /// Removes the reputation threshold at the specified index.
        /// </summary>
        /// <param name="index">The index of the reputation threshold to remove.</param>
        public void Remove(int index)
        {
            var list = new List<ReputationThreshold>(m_Thresholds);
            list.RemoveAt(index);
            m_Thresholds = list.ToArray();
        }

        /// <summary>
        /// Sorts the reputation thresholds in descending order based on the points required.
        /// </summary>
        /// <returns>An array of reputation thresholds sorted in descending order.</returns>
        public ReputationThreshold[] Descending()
        {
            var list = new List<ReputationThreshold>(m_Thresholds);
            list.Sort((a, b) => b.PointsRequired.CompareTo(a.PointsRequired));
            return list.ToArray();
        }
        
        /// <summary>
        /// Sorts the reputation thresholds in ascending order based on the points required.
        /// </summary>
        /// <returns>An array of reputation thresholds sorted in ascending order.</returns>
        public ReputationThreshold[] Ascending()
        {
            var list = new List<ReputationThreshold>(m_Thresholds);
            list.Sort((a, b) => a.PointsRequired.CompareTo(b.PointsRequired));
            return list.ToArray();
        }

        /// <summary>
        /// Retrieves the reputation threshold based on the given points.
        /// Returns the threshold that matches the given points, or the highest threshold if no match is found.
        /// </summary>
        /// <param name="points">The points to check against the reputation thresholds.</param>
        /// <returns>The reputation threshold that matches the given points, or null if no threshold is found.</returns>
        public ReputationThreshold GetByPoints(int points)
        {
            var array = Descending();
            foreach (var threshold in array)
            {
                if (points >= threshold.PointsRequired) return threshold;
            }
            
            return m_Thresholds.Length > 0 ? m_Thresholds[^1] : null;
        }
        
        /// <summary>
        /// Returns the next threshold value based on the current points.
        /// </summary>
        /// <param name="points">The current points to compare against the reputation thresholds.</param>
        /// <returns>The next threshold value if found, otherwise the maximum threshold value.</returns>
        public int GetNextThreshold(int points)
        {
            var array = Ascending();
            foreach (var threshold in array)
            {
                if (points < threshold.PointsRequired) return threshold.PointsRequired;
            }
            
            return m_Thresholds.Length > 0 ? m_Thresholds[^1].PointsRequired : 0;
        }
    }
}