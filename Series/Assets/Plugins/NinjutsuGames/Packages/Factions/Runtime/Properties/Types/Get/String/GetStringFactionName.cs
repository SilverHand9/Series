using System;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Name")]
    [Category("Factions/Faction Name")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Blue)]
    [Description("The name of a particular Faction")]

    [Serializable] [HideLabelsInEditor]
    public class GetStringFactionName : PropertyTypeGetString
    {
        [SerializeField] protected PropertyGetFaction m_Faction = GetFactionInstance.Create();

        public override string Get(Args args)
        {
            var faction = m_Faction.Get(args);
            return faction ? faction.GetTitle(args) : string.Empty;
        }

        public static PropertyGetString Create => new(
            new GetStringFactionName()
        );

        public override string String => $"{m_Faction} Name";
    }
}