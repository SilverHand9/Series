using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class FactionStances : TPolymorphicList<FactionStance>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeReference] private FactionStance[] m_Stances =
        {
            new(new IdString("Hostile"), GetColorColorsRed.Create, new PropertyGetInteger(new GetDecimalConstantZero())),
            new(new IdString("Neutral"), GetColorColorsYellow.Create, new PropertyGetInteger(50)),
            new(new IdString("Friendly"), GetColorColorsGreen.Create, new PropertyGetInteger(100)),
        };

        // PROPERTIES: ----------------------------------------------------------------------------

        public override int Length => m_Stances.Length;

        public FactionStance[] Get => m_Stances;

        /// <summary>
        /// Returns the lowest Faction Stance based on index.
        /// </summary>
        public FactionStance Lowest
        {
            get
            {
                if (m_Stances.Length == 0) m_Stances = new FactionStance[] { new(new IdString("Hostile"), GetColorColorsRed.Create, new PropertyGetInteger(new GetDecimalConstantZero())) };
                return m_Stances[0];
            }
        }

        /// <summary>
        /// Returns the highest Faction Stance based on index.
        /// </summary>
        public FactionStance Highest => m_Stances.Length == 0 ? null : m_Stances[^1];

        /// <summary>
        /// Returns the Faction Stance at the specified index.
        /// </summary>
        /// <param name="stance"></param>
        /// <returns></returns>
        public int IndexOf(string stance)
        {
            for (var i = 0; i < m_Stances.Length; ++i)
            {
                if (m_Stances[i].Key.ToLowerInvariant().Equals(stance.ToLowerInvariant())) return i;
            }

            return -1;
        }

        /// <summary>
        /// Returns the Faction Stance based on the specified key name.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public FactionStance GetStance(string key)
        {
            foreach (var stance in m_Stances)
            {
                if (stance.Key == key) return stance;
            }

            return Lowest;
        }
    }
}