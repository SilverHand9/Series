using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Image(typeof(IconFaction), ColorTheme.Type.Blue)]
    
    [Serializable]
    public class FactionItem : TPolymorphicItem<FactionItem>
    {
        [SerializeField] private Faction m_Faction;
        [SerializeField] private PropertyGetInteger m_InitialPoints = new(new GetDecimalConstantZero());

        // PROPERTIES: ----------------------------------------------------------------------------

        public Faction Faction => m_Faction;
        public int InitialPoints => (int)m_InitialPoints.Get(Args.EMPTY);

        public override string Title => m_Faction != null 
            ? m_Faction.Name
            : "(none)";
    }
}