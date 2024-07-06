using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Direction Faction Name Variable")]
    [Category("Variables/Direction Faction Name Variable")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the direction vector value of a Faction Name Variable")]

    [Serializable]
    public class GetRotationDirectionFactionName : PropertyTypeGetRotation
    {
        [SerializeField]
        protected FieldGetFactionName m_Variable = new(ValueVector3.TYPE_ID);

        public override Quaternion Get(Args args)
        {
            return Quaternion.LookRotation(this.m_Variable.Get<Vector3>(args));
        }

        public override string String => this.m_Variable.ToString();
    }
}