using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]
    
    [Description("Sets the Game Object value of a Faction Name Variable")]
    [Image(typeof(IconNameVariable), ColorTheme.Type.Purple, typeof(OverlayDot))]

    [Serializable]
    public class SetGameObjectFactionName : PropertyTypeSetGameObject
    {
        [SerializeField]
        protected FieldSetFactionName m_Variable = new FieldSetFactionName(ValueGameObject.TYPE_ID);

        public override void Set(GameObject value, Args args) => this.m_Variable.Set(value, args);
        public override GameObject Get(Args args) => this.m_Variable.Get(args) as GameObject;

        public static PropertySetGameObject Create => new PropertySetGameObject(
            new SetGameObjectFactionName()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}