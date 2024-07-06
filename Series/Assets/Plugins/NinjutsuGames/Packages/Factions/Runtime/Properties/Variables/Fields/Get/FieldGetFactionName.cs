using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class FieldGetFactionName : TFieldGetVariable
    {
        [SerializeField] protected Faction m_Variable;
        [SerializeField] protected IdPathString m_Name;

        // CONSTRUCTORS: --------------------------------------------------------------------------

        public FieldGetFactionName(IdString typeID)
        {
            this.m_TypeID = typeID;
        }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override object Get(Args args)
        {
            return this.m_Variable != null ? this.m_Variable.Get(m_Name.String) : null;
        }

        public override string ToString()
        {
            return string.Format(
                "{0}{1}",
                this.m_Variable != null ? m_Variable.name : "(none)",
                string.IsNullOrEmpty(this.m_Name.String) ? string.Empty : $"[{this.m_Name.String}]" 
            );
        }
    }
}