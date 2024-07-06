using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Local List Variable")]
    [Category("Local List Variable")]
    
    [Description("Sets the Faction value on a Local List Variable")]
    [Image(typeof(IconListVariable), ColorTheme.Type.Teal)]

    [Serializable] [HideLabelsInEditor]
    public class SetFactionLocalList : PropertyTypeSetFaction
    {
        [SerializeField]
        protected FieldSetLocalList m_Variable = new(ValueFaction.TYPE_ID);

        public override void Set(Faction value, Args args) => m_Variable.Set(value, args);
        public override Faction Get(Args args) => m_Variable.Get(args) as Faction;

        public static PropertySetFaction Create => new(
            new SetFactionLocalList()
        );
        
        public override string String => m_Variable.ToString();
    }
}