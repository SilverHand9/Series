using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the Color value of a Faction Name Variable")]

    [Serializable]
    public class GetColorFactionName : PropertyTypeGetColor
    {
        [SerializeField]
        protected FieldGetFactionName m_Variable = new(ValueColor.TYPE_ID);

        public override Color Get(Args args) => this.m_Variable.Get<Color>(args);
        public override string String => this.m_Variable.ToString();
    }
}