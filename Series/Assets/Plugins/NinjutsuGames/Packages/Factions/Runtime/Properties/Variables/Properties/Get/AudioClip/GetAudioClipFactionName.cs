using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]

    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the Audio Clip value of a Faction Name Variable")]

    [Serializable]
    public class GetAudioClipFactionName : PropertyTypeGetAudio
    {
        [SerializeField]
        protected FieldGetFactionName m_Variable = new(ValueAudioClip.TYPE_ID);

        public override AudioClip Get(Args args) => this.m_Variable.Get<AudioClip>(args);

        public override string String => this.m_Variable.ToString();
    }
}
