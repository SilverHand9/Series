using System;
using GameCreator.Runtime.Common;

namespace NinjutsuGames.Runtime.Factions
{
    public static class FactionUtilities
    {
        /// <summary>
        /// Returns the next faction stance based on the current faction stance.
        /// </summary>
        /// <param name="currentStance"></param>
        /// <returns></returns>
        public static FactionStance GetNextStatus(this FactionStance currentStance)
        {
            var stancesInOrder = Settings.From<FactionsRepository>().Stances.Get;
            
            // Find the index of the current stance in the order
            var currentIndex = Array.IndexOf(stancesInOrder, currentStance);
    
            if (currentIndex == -1)
            {
                throw new Exception("Unexpected FactionStance: " + currentStance);
            }
    
            // Get the next index, wrapping back to zero if we're at the end
            var nextIndex = (currentIndex + 1) % stancesInOrder.Length;
    
            // Return the stance at the next index
            return stancesInOrder[nextIndex];
        }
    }
}