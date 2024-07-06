using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Global List Variable")]
    [Category("Global List Variable")]
    
    [Description("Sets the Faction value on a Global List Variable")]
    [Image(typeof(IconListVariable), ColorTheme.Type.Teal, typeof(OverlayDot))]

    [Serializable] [HideLabelsInEditor]
    public class SetFactionGlobalList : PropertyTypeSetFaction
    {
        [SerializeField]
        protected FieldSetGlobalList m_Variable = new(ValueFaction.TYPE_ID);

        public override void Set(Faction value, Args args) => this.m_Variable.Set(value, args);
        public override Faction Get(Args args) => this.m_Variable.Get(args) as Faction;

        public static PropertySetFaction Create => new(
            new SetFactionGlobalList()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}