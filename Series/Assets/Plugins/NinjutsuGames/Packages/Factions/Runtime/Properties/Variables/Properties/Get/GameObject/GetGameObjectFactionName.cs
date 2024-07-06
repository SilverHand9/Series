using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name Variable")]
    [Category("Variables/Faction Name Variable")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Purple, typeof(OverlayDot))]
    [Description("Returns the Game Object value of a Faction Name Variable")]

    [Serializable]
    public class GetGameObjectFactionName : PropertyTypeGetGameObject
    {
        [SerializeField]
        protected FieldGetFactionName m_Variable = new(ValueGameObject.TYPE_ID);

        public override GameObject Get(Args args) => this.m_Variable.Get<GameObject>(args);

        public override string String => this.m_Variable.ToString();
    }
}