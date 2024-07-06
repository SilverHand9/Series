using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Local List Variable")]
    [Category("Variables/Local List Variable")]
    
    [Image(typeof(IconListVariable), ColorTheme.Type.Teal)]
    [Description("Returns the Faction value of a Local List Variable")]

    [Serializable] [HideLabelsInEditor]
    public class GetFactionLocalList : PropertyTypeGetFaction
    {
        [SerializeField]
        protected FieldGetLocalList m_Variable = new(ValueFaction.TYPE_ID);

        public override Faction Get(Args args) => m_Variable.Get<Faction>(args);

        public override string String => m_Variable.ToString();
    }
}