using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]

    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the Animation Clip value of a Faction Name Variable")]

    [Serializable]
    public class GetAnimationFactionName : PropertyTypeGetAnimation
    {
        [SerializeField]
        protected FieldGetFactionName m_Variable = new(ValueAnimClip.TYPE_ID);

        public override AnimationClip Get(Args args) => this.m_Variable.Get<AnimationClip>(args);

        public override string String => this.m_Variable.ToString();
    }
}
