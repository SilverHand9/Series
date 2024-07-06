using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Last Status Changed")]
    [Category("Factions/Last Status Changed")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Blue, typeof(OverlayTick))]
    [Description("A reference to the last Faction status changed (if any)")]

    [Serializable] [HideLabelsInEditor]
    public class GetFactionLastStatusChanged : PropertyTypeGetFaction
    {
        public override Faction Get(Args args) => Faction.LastStatusChanged;
        public override Faction Get(GameObject gameObject) => Faction.LastStatusChanged;

        public static PropertyGetFaction Create()
        {
            var instance = new GetFactionLastStatusChanged();
            return new PropertyGetFaction(instance);
        }

        public override string String => "Last Status Changed";
    }
}