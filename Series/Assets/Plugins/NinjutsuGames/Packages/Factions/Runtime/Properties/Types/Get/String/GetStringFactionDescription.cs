using System;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Description")]
    [Category("Factions/Faction Description")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Yellow)]
    [Description("The description of a particular Faction")]

    [Serializable] [HideLabelsInEditor]
    public class GetStringFactionDescription : PropertyTypeGetString
    {
        [SerializeField] protected PropertyGetFaction m_Faction = GetFactionInstance.Create();

        public override string Get(Args args)
        {
            var faction = m_Faction.Get(args);
            return faction ? faction.GetDescription(args) : string.Empty;
        }

        public static PropertyGetString Create => new(
            new GetStringFactionDescription()
        );

        public override string String => $"{m_Faction} Description";
    }
}