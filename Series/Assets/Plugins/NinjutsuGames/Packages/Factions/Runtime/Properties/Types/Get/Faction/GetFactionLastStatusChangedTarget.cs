using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Last Status Changed Target")]
    [Category("Factions/Last Status Changed Target")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Blue, typeof(OverlayBolt))]
    [Description("The target Faction of the last status changed Faction.")]

    [Serializable] [HideLabelsInEditor]
    public class GetFactionLastStatusChangedTarget : PropertyTypeGetFaction
    {
        public override Faction Get(Args args) => Faction.LastStatusChangedTarget;
        public override Faction Get(GameObject gameObject) => Faction.LastStatusChangedTarget;

        public static PropertyGetFaction Create()
        {
            var instance = new GetFactionLastStatusChangedTarget();
            return new PropertyGetFaction(instance);
        }

        public override string String => "Last Status Changed Target";
    }
}