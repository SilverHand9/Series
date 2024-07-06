using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Local Name Variable")]
    [Category("Local Name Variable")]
    
    [Description("Sets the Faction value on a Local Name Variable")]
    [Image(typeof(IconNameVariable), ColorTheme.Type.Purple)]

    [Serializable] [HideLabelsInEditor]
    public class SetFactionLocalName : PropertyTypeSetFaction
    {
        [SerializeField]
        protected FieldSetLocalName m_Variable = new(ValueFaction.TYPE_ID);

        public override void Set(Faction value, Args args) => this.m_Variable.Set(value, args);
        public override Faction Get(Args args) => this.m_Variable.Get(args) as Faction;

        public static PropertySetFaction Create => new(
            new SetFactionLocalName()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}