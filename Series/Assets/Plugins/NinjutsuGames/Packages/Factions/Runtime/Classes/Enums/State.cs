using System;

namespace NinjutsuGames.Runtime.Factions
{
    public enum State
    {
        Inactive  = 0,
        Active    = 1,
    }
    
    [Flags]
    public enum StateFlags
    {
        Inactive  = 1, // 0b00001
        Active    = 2, // 0b00010
    }
}