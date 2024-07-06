using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]
    
    [Description("Sets the Sprite value of a Faction Name Variable")]
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]

    [Serializable]
    public class SetSpriteFactionName : PropertyTypeSetSprite
    {
        [SerializeField]
        protected FieldSetFactionName m_Variable = new FieldSetFactionName(ValueSprite.TYPE_ID);

        public override void Set(Sprite value, Args args) => this.m_Variable.Set(value, args);
        public override Sprite Get(Args args) => this.m_Variable.Get(args) as Sprite;

        public static PropertySetSprite Create => new PropertySetSprite(
            new SetSpriteFactionName()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}