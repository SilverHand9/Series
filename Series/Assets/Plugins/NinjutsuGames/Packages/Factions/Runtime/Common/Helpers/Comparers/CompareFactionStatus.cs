using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{

    [Serializable]
    public class CompareFactionStatus
    {
        public enum Comparison
        {
            Equals,
            Different,
            Less,
            Greater,
            LessOrEqual,
            GreaterOrEqual
        }
        
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] 
        private Comparison m_Comparison = Comparison.Equals;
        
        [SerializeField, FactionStance] private string m_CompareTo;
        
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public CompareFactionStatus()
        { }
        
        public CompareFactionStatus(Comparison comparison) : this()
       {
           m_Comparison = comparison;
       }

        public CompareFactionStatus(string factionStance) : this()
        {
            m_CompareTo = factionStance;
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public bool Match(string value)
        {
            var a = Settings.From<FactionsRepository>().Stances.IndexOf(value);
            var b = Settings.From<FactionsRepository>().Stances.IndexOf(m_CompareTo);
            
            return m_Comparison switch
            {
                Comparison.Equals => a == b,
                Comparison.Different => a != b,
                Comparison.Less => a < b,
                Comparison.Greater => a > b,
                Comparison.LessOrEqual => a <= b,
                Comparison.GreaterOrEqual => a >= b,
                _ => throw new ArgumentOutOfRangeException($"Enum '{m_Comparison}' not found")
            };
        }
        
        // STRING: --------------------------------------------------------------------------------

        public override string ToString()
        {
            var operation = m_Comparison switch
            {
                Comparison.Equals => "=",
                Comparison.Different => "≠",
                Comparison.Less => "<",
                Comparison.Greater => ">",
                Comparison.LessOrEqual => "≤",
                Comparison.GreaterOrEqual => "≥",
                _ => string.Empty
            };
            
            return $"{operation} {m_CompareTo}";
        }
    }
}
