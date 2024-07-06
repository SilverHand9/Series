using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Global Name Variable")]
    [Category("Global Name Variable")]
    
    [Description("Sets the Faction value on a Global Name Variable")]
    [Image(typeof(IconNameVariable), ColorTheme.Type.Purple, typeof(OverlayDot))]

    [Serializable] [HideLabelsInEditor]
    public class SetFactionGlobalName : PropertyTypeSetFaction
    {
        [SerializeField]
        protected FieldSetFactionName m_Variable = new(ValueFaction.TYPE_ID);

        public override void Set(Faction value, Args args) => m_Variable.Set(value, args);
        public override Faction Get(Args args) => m_Variable.Get(args) as Faction;

        public static PropertySetFaction Create => new(
            new SetFactionGlobalName()
        );
        
        public override string String => m_Variable.ToString();
    }
}