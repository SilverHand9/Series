using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using NinjutsuGames.Runtime.Factions.UnityUI;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction from UI")]
    [Category("Faction from UI")]
    
    [Image(typeof(IconUIImage), ColorTheme.Type.Blue)]
    [Description("Returns a Faction from a UI element with a Faction UI component.")]

    [Serializable] [HideLabelsInEditor]
    public class GetFactionFromUI : PropertyTypeGetFaction
    {
        [SerializeField] protected PropertyGetGameObject m_UI = GetGameObjectInstance.Create();

        public override Faction Get(Args args)
        {
            var factionUI = m_UI.Get<BaseFactionUI>(args);
            return factionUI ? factionUI.Faction : null;
        }

        public static PropertyGetFaction Create()
        {
            var instance = new GetFactionFromMember();
            return new PropertyGetFaction(instance);
        }

        public override string String => $"from {m_UI}";
    }
}