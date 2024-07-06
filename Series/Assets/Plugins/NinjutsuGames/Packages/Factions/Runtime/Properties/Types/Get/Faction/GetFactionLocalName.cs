using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Local Name Variable")]
    [Category("Variables/Local Name Variable")]

    [Image(typeof(IconNameVariable), ColorTheme.Type.Purple)]
    [Description("Returns the Quest value of a Local Name Variable")]
    
    [Serializable] [HideLabelsInEditor]
    public class GetFactionLocalName : PropertyTypeGetFaction
    {
        [SerializeField]
        protected FieldGetLocalName m_Variable = new(ValueFaction.TYPE_ID);

        public override Faction Get(Args args) => m_Variable.Get<Faction>(args);

        public override string String => m_Variable.ToString();
    }
}