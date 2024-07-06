using System;
using GameCreator.Runtime.Common;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class FactionPoints
    {
        public int points;
        public string stance;
        
        public FactionPoints(string stance, int points)
        {
            this.points = points;
            this.stance = stance;
        }

        public override string ToString() => $"Status: {stance} Points: {points}";
    }
}