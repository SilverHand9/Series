using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Image(typeof(IconFaction), ColorTheme.Type.Green)]
    
    [Title("Faction")]
    [Category("Factions/Faction")]
    
    [Description("Remembers the state of factions from a Member")]

    [Serializable]
    public class MemoryFactionMember : Memory
    {
        public override string Title => "Faction";

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public override Token GetToken(GameObject target)
        {
            var member = target.Get<Member>();
            return member ? new TokenFactionMember(member) : null;
        }

        public override void OnRemember(GameObject target, Token token)
        {
            var member = target.Get<Member>();
            if (!member) return;
            
            TokenFactionMember.OnRemember(member, token);
        }
    }
}