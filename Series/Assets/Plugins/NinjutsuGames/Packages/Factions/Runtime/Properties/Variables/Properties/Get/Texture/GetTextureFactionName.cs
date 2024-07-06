using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the Texture value of a Faction Name Variable")]

    [Serializable]
    public class GetTextureFactionName : PropertyTypeGetTexture
    {
        [SerializeField]
        protected FieldGetFactionName m_Variable = new(ValueTexture.TYPE_ID);

        public override Texture Get(Args args) => this.m_Variable.Get<Texture>(args);

        public override string String => this.m_Variable.ToString();
    }
}