using System;
using GameCreator.Runtime.Common;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class PropertySetFaction : TPropertySet<PropertyTypeSetFaction, Faction>
    {
        public PropertySetFaction() : base(new SetFactionNone())
        { }

        public PropertySetFaction(PropertyTypeSetFaction defaultType) : base(defaultType)
        { }
    }
}