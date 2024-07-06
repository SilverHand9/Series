using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Title("Faction from Member")]
    [Category("Faction from Member")]
    
    [Image(typeof(IconFactionMember), ColorTheme.Type.Blue)]
    [Description("A reference to a Faction from a Member (if any)")]

    [Serializable] [HideLabelsInEditor]
    public class GetFactionFromMember : PropertyTypeGetFaction
    {
        [SerializeField] protected PropertyGetGameObject m_Member = GetGameObjectPlayer.Create();
        [SerializeReference] protected TListGetPick m_Select = new GetPickFirst();

        public override Faction Get(Args args)
        {
            var member = m_Member.Get<Member>(args);
            if (!member) return null;
            
            var index = m_Select.GetIndex(member.Factions.Count, args);
            if(index < 0 || index >= member.Factions.Count) return null;
            return member ? member.GetFaction(index) : null;
        }

        public static PropertyGetFaction Create()
        {
            var instance = new GetFactionFromMember();
            return new PropertyGetFaction(instance);
        }

        public override string String => $"Faction<b>[{m_Select}]</b> from {m_Member}";
    }
}