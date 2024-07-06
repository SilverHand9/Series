using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]

    [Description("Sets the Animation Clip value of a Faction Name Variable")]
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]

    [Serializable]
    public class SetAnimationFactionName : PropertyTypeSetAnimation
    {
        [SerializeField]
        protected FieldSetFactionName m_Variable = new(ValueAnimClip.TYPE_ID);

        public override void Set(AnimationClip value, Args args) => this.m_Variable.Set(value, args);
        public override AnimationClip Get(Args args) => this.m_Variable.Get(args) as AnimationClip;

        public static PropertySetAnimation Create => new PropertySetAnimation(
            new SetAnimationFactionName()
        );

        public override string String => this.m_Variable.ToString();
    }
}
