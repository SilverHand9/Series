using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]

    [Description("Sets the Material value of a Faction Name Variable")]
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]

    [Serializable]
    public class SetMaterialFactionName : PropertyTypeSetMaterial
    {
        [SerializeField]
        protected FieldSetFactionName m_Variable = new FieldSetFactionName(ValueMaterial.TYPE_ID);

        public override void Set(Material value, Args args) => this.m_Variable.Set(value, args);
        public override Material Get(Args args) => this.m_Variable.Get(args) as Material;

        public static PropertySetMaterial Create => new PropertySetMaterial(
            new SetMaterialFactionName()
        );

        public override string String => this.m_Variable.ToString();
    }
}
