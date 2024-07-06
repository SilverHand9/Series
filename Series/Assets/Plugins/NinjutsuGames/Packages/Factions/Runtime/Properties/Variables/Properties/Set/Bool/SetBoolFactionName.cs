using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]
    
    [Description("Sets the boolean value of a Faction Name Variable")]
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]

    [Serializable]
    public class SetBoolFactionName : PropertyTypeSetBool
    {
        [SerializeField]
        protected FieldSetFactionName m_Variable = new(ValueBool.TYPE_ID);

        public override void Set(bool value, Args args) => this.m_Variable.Set(value, args);
        public override bool Get(Args args) => (bool) this.m_Variable.Get(args);

        public static PropertySetBool Create => new PropertySetBool(
            new SetBoolFactionName()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}