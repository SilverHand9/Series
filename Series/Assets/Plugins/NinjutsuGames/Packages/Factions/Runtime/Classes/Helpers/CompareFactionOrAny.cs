using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class CompareFactionOrAny
    {
        private enum Option
        {
            Any = 0,
            Specific = 1
        }
        
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private Option m_Option = Option.Any;
        [SerializeField] private PropertyGetFaction m_Faction = GetFactionInstance.Create();
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool Any => m_Option == Option.Any;

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public CompareFactionOrAny()
        { }

        public CompareFactionOrAny(PropertyGetFaction faction) : this()
        {
            m_Option = Option.Specific;
            m_Faction = faction;
        }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public bool Match(Faction compareTo, Args args)
        {
            if (Any) return true;
            if (!compareTo) return false;
            
            var faction = Get(args);
            return faction && compareTo.Equals(faction);
        }
        
        public bool Match(Faction compareTo, GameObject args)
        {
            if (Any) return true;
            if (!compareTo) return false;

            var faction = Get(args);
            return faction && compareTo.Equals(faction);
        }
        
        public Faction Get(Args args)
        {
            return m_Faction.Get(args);
        }

        public Faction Get(GameObject target)
        {
            return m_Faction.Get(target);
        }
    }
}