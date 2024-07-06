using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("None")]
    [Category("None")]
    [Description("Don't save on anything")]
    
    [Image(typeof(IconNull), ColorTheme.Type.TextLight)]

    [Serializable]
    public class SetFactionNone : PropertyTypeSetFaction
    {
        public override void Set(Faction value, Args args)
        { }
        
        public override void Set(Faction value, GameObject gameObject)
        { }

        public static PropertySetFaction Create => new(
            new SetFactionNone()
        );

        public override string String => "(none)";
    }
}