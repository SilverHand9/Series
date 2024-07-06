using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Euler Faction Name Variable")]
    [Category("Variables/Euler Faction Name Variable")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the euler rotation value of a Faction Name Variable")]

    [Serializable]
    public class GetRotationEulerFactionName : PropertyTypeGetRotation
    {
        [SerializeField]
        protected FieldGetFactionName m_Variable = new(ValueVector3.TYPE_ID);

        public override Quaternion Get(Args args)
        {
            return Quaternion.Euler(this.m_Variable.Get<Vector3>(args));
        }

        public override string String => this.m_Variable.ToString();
    }
}