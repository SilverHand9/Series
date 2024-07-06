using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Global Name Variable")]
    [Category("Variables/Global Name Variable")]
    
    [Image(typeof(IconNameVariable), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the Faction value of a Global Name Variable")]

    [Serializable] [HideLabelsInEditor]
    public class GetFactionGlobalName : PropertyTypeGetFaction
    {
        [SerializeField]
        protected FieldGetFactionName m_Variable = new(ValueFaction.TYPE_ID);

        public override Faction Get(Args args) => m_Variable.Get<Faction>(args);

        public override string String => m_Variable.ToString();
    }
}