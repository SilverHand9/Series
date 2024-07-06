using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction from String")]
    [Category("Faction from String")]
    
    [Image(typeof(IconString), ColorTheme.Type.Blue)]
    [Description("Returns a Faction by it's name.")]

    [Serializable] [HideLabelsInEditor]
    public class GetFactionFromString : PropertyTypeGetFaction
    {
        [SerializeField] protected PropertyGetString m_Name = GetStringString.Create;

        public override Faction Get(Args args)
        {
            var repository = Settings.From<FactionsRepository>().Factions;
            var name = m_Name.Get(args);
            return string.IsNullOrEmpty(name) ? null : repository.GetByString(name);
        }

        public static PropertyGetFaction Create()
        {
            var instance = new GetFactionFromMember();
            return new PropertyGetFaction(instance);
        }

        public override string String => $"from {m_Name}";
    }
}