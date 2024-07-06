using System;
using GameCreator.Runtime.Common;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class PropertyGetFaction : TPropertyGet<PropertyTypeGetFaction, Faction>
    {
        public PropertyGetFaction() : base(new GetFactionInstance())
        { }

        public PropertyGetFaction(PropertyTypeGetFaction defaultType) : base(defaultType)
        { }
    }
}