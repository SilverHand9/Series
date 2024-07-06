using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the Sprite value of a Faction Name Variable")]

    [Serializable]
    public class GetSpriteFactionName : PropertyTypeGetSprite
    {
        [SerializeField]
        protected FieldGetFactionName m_Variable = new(ValueSprite.TYPE_ID);

        public override Sprite Get(Args args) => this.m_Variable.Get<Sprite>(args);

        public override string String => this.m_Variable.ToString();
    }
}