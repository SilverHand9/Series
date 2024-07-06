using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]
    
    [Description("Sets the Texture value of a Faction Name Variable")]
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]

    [Serializable]
    public class SetTextureFactionName : PropertyTypeSetTexture
    {
        [SerializeField]
        protected FieldSetFactionName m_Variable = new FieldSetFactionName(ValueTexture.TYPE_ID);

        public override void Set(Texture value, Args args) => this.m_Variable.Set(value, args);
        public override Texture Get(Args args) => this.m_Variable.Get(args) as Texture;

        public static PropertySetTexture Create => new PropertySetTexture(
            new SetTextureFactionName()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}