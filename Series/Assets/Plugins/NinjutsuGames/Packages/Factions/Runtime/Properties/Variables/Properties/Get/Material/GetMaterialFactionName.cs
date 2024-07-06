using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]

    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the Material value of a Faction Name Variable")]

    [Serializable]
    public class GetMaterialFactionName : PropertyTypeGetMaterial
    {
        [SerializeField]
        protected FieldGetFactionName m_Variable = new(ValueMaterial.TYPE_ID);

        public override Material Get(Args args) => this.m_Variable.Get<Material>(args);

        public override string String => this.m_Variable.ToString();
    }
}
