using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]
    
    [Description("Sets the Color value of a Faction Name Variable")]
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]

    [Serializable]
    public class SetColorFactionName : PropertyTypeSetColor
    {
        [SerializeField]
        protected FieldSetFactionName m_Variable = new FieldSetFactionName(ValueColor.TYPE_ID);

        public override void Set(Color value, Args args) => this.m_Variable.Set(value, args);
        public override Color Get(Args args) => (Color) this.m_Variable.Get(args);

        public static PropertySetColor Create => new PropertySetColor(
            new SetColorFactionName()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}