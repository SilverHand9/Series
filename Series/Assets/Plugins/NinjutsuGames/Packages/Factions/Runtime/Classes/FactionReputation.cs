using System;
using GameCreator.Runtime.Common;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class FactionReputation : TSerializableDictionary<string, FactionPoints>
    {
    }
}