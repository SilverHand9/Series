using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the string value of a Faction Name Variable")]

    [Serializable]
    public class GetStringFactionVarName : PropertyTypeGetString
    {
        [SerializeField]
        protected FieldGetFactionName m_Variable = new(ValueString.TYPE_ID);

        public override string Get(Args args) => this.m_Variable.Get<string>(args);

        public static PropertyGetString Create => new(
            new GetStringFactionVarName()
        );

        public override string String => this.m_Variable.ToString();
    }
}