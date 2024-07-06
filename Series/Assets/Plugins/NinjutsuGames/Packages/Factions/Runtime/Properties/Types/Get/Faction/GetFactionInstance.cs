using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction")]
    [Category("Faction")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Blue)]
    [Description("A reference to a Faction asset")]

    [Serializable] [HideLabelsInEditor]
    public class GetFactionInstance : PropertyTypeGetFaction
    {
        [SerializeField] protected Faction m_Faction;

        public override Faction Get(Args args) => m_Faction;
        public override Faction Get(GameObject gameObject) => m_Faction;

        public static PropertyGetFaction Create(Faction Faction = null)
        {
            var instance = new GetFactionInstance
            {
                m_Faction = Faction
            };
            
            return new PropertyGetFaction(instance);
        }

        public override string String => m_Faction != null
            ? m_Faction.name
            : "(none)";
    }
}