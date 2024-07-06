using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Status Name")]
    [Category("Factions/Status Name")]
    
    [Image(typeof(IconBookmarkSolid), ColorTheme.Type.Purple)]
    [Description("The name of a particular Faction Status")]

    [Serializable] [HideLabelsInEditor]
    public class GetStringStatusName : PropertyTypeGetString
    {
        [SerializeField, FactionStance] private string m_Status;

        public override string Get(Args args)
        {
            return m_Status;
        }

        public static PropertyGetString Create => new(
            new GetStringStatusName()
        );

        public override string String => $"{m_Status} Name";
    }
}