using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]
    
    [Description("Sets the Vector3 value of a Faction Name Variable")]
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]

    [Serializable]
    public class SetVector3FactionName : PropertyTypeSetVector3
    {
        [SerializeField]
        protected FieldSetFactionName m_Variable = new FieldSetFactionName(ValueVector3.TYPE_ID);

        public override void Set(Vector3 value, Args args) => this.m_Variable.Set(value, args);
        public override Vector3 Get(Args args) => (Vector3) this.m_Variable.Get(args);

        public static PropertySetVector3 Create => new PropertySetVector3(
            new SetVector3FactionName()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}