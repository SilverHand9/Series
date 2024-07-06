using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the boolean value of a Faction Name Variable")]

    [Serializable]
    public class GetBoolFactionName : PropertyTypeGetBool
    {
        [SerializeField]
        protected FieldGetFactionName m_Variable = new(ValueBool.TYPE_ID);

        public override bool Get(Args args) => this.m_Variable.Get<bool>(args);
        public override string String => this.m_Variable.ToString();
    }
}