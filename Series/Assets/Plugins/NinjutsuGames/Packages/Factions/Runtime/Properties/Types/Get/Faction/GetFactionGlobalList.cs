using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Global List Variable")]
    [Category("Variables/Global List Variable")]
    
    [Image(typeof(IconListVariable), ColorTheme.Type.Teal, typeof(OverlayDot))]
    [Description("Returns the Faction value of a Global List Variable")]

    [Serializable] [HideLabelsInEditor]
    public class GetFactionGlobalList : PropertyTypeGetFaction
    {
        [SerializeField]
        protected FieldGetGlobalList m_Variable = new(ValueFaction.TYPE_ID);

        public override Faction Get(Args args) => m_Variable.Get<Faction>(args);

        public override string String => m_Variable.ToString();
    }
}