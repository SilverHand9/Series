using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the decimal value of a Faction Name Variable")]

    [Serializable]
    public class GetDecimalFactionName : PropertyTypeGetDecimal
    {
        [SerializeField]
        protected FieldGetFactionName m_Variable = new(ValueNumber.TYPE_ID);

        public override double Get(Args args) => this.m_Variable.Get<double>(args);

        public static PropertyGetDecimal Create => new PropertyGetDecimal(
            new GetDecimalFactionName()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}