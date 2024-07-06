using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction Icon")]
    [Category("Factions/Faction Icon")]
    
    [Image(typeof(IconFaction), ColorTheme.Type.Blue)]
    [Description("A reference to a Sprite texture of a Faction")]
    
    [Keywords("Faction", "Sprite", "Icon")]

    [Serializable]
    public class GetFactionSprite : PropertyTypeGetSprite
    {
        [SerializeField] protected PropertyGetFaction m_Faction = GetFactionInstance.Create();

        public override Sprite Get(Args args)
        {
            var faction = m_Faction.Get(args);
            return faction ? faction.GetSprite(args) : null;
        }

        public static PropertyGetSprite Create() => new(
            new GetFactionSprite()
        );

        public override string String => $"{m_Faction} Icon";
    }
}