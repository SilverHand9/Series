using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the Vector3 value of a Faction Name Variable")]

    [Serializable]
    public class GetDirectionFactionName : PropertyTypeGetDirection
    {
        [SerializeField]
        protected FieldGetFactionName m_Variable = new(ValueVector3.TYPE_ID);

        public override Vector3 Get(Args args) => this.m_Variable.Get<Vector3>(args);

        public override string String => this.m_Variable.ToString();
    }
}