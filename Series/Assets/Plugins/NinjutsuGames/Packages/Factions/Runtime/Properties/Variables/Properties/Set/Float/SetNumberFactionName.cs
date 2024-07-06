using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]
    
    [Description("Sets the numeric value of a Faction Name Variable")]
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]

    [Serializable]
    public class SetNumberFactionName : PropertyTypeSetNumber
    {
        [SerializeField]
        protected FieldSetFactionName m_Variable = new FieldSetFactionName(ValueNumber.TYPE_ID);

        public override void Set(double value, Args args) => this.m_Variable.Set(value, args);
        public override double Get(Args args) => (double) this.m_Variable.Get(args);

        public static PropertySetNumber Create => new PropertySetNumber(
            new SetNumberFactionName()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}