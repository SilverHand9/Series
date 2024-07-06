using System;
using System.Collections.Generic;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class TokenFactionMember : Token
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private FactionReputation m_Reputation;
        [SerializeField] private List<string> m_Factions;

        // CONSTRUCTORS: --------------------------------------------------------------------------
        
        public TokenFactionMember(Member member) : base()
        {
            m_Reputation = new FactionReputation();
            m_Factions = new List<string>();

            foreach (var r in member.Reputation)
            {
                m_Reputation.Add(r.Key, r.Value);
            }
            
            foreach (var f in member.Factions)
            {
                m_Factions.Add(f.UniqueID.String);
            }
        }
        
        // PUBLIC STATIC METHODS: -----------------------------------------------------------------

        public static void OnRemember(Member member, Token token)
        {
            if (token is not TokenFactionMember tokenMember) return;
            
            member.Factions.Clear();
            member.Reputation.Clear();

            var repository = Settings.From<FactionsRepository>().Factions;
            foreach (var f in tokenMember.m_Factions)
            {
                var uniqueID = new IdString(f);
                var faction = repository.Get(uniqueID);
                if (faction) member.JoinFaction(faction, tokenMember.m_Reputation[faction.Name].points, false);
            }

            member.OnRemember();
        }
    }
}