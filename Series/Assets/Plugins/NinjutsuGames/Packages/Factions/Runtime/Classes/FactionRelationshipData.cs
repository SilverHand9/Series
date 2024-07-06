using System;
using GameCreator.Runtime.Common;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class FactionRelationshipData
    {
        public Faction faction;
        public string stance;
        
        public FactionRelationshipData(Faction faction, string stance)
        {
            this.faction = faction;
            this.stance = stance;
        }

        public override string ToString() => faction == null ? "None" : faction.name;
    }
}