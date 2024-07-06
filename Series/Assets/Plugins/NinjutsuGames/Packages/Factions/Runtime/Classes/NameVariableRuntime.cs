using System;
using System.Collections.Generic;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class NameVariableRuntime : TVariableRuntime<NameVariable>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeReference] private NameList m_List = new();
        
        // PROPERTIES: ----------------------------------------------------------------------------
        
        internal Dictionary<string, NameVariable> Variables { get; private set; }

        public NameList TemplateList => m_List;

        // EVENTS: --------------------------------------------------------------------------------

        public event Action<string> EventChange;
        
        // CONSTRUCTORS: --------------------------------------------------------------------------

        public NameVariableRuntime()
        {
            Variables = new Dictionary<string, NameVariable>();
        }
        
        public NameVariableRuntime(NameList nameList) : this()
        {
            m_List = nameList;
        }

        public NameVariableRuntime(params NameVariable[] nameList) : this()
        {
            m_List = new NameList(nameList);
        }
        
        // INITIALIZERS: --------------------------------------------------------------------------

        public override void OnStartup()
        {
            Variables = new Dictionary<string, NameVariable>();
            
            for (var i = 0; i < m_List.Length; ++i)
            {
                var variable = m_List.Get(i);
                if (variable == null) continue;
                
                if (Variables.ContainsKey(variable.Name)) continue;
                Variables.Add(variable.Name, variable.Copy as NameVariable);
            }
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public bool Exists(string name)
        {
            return Variables.ContainsKey(name);
        }

        public object Get(string name)
        {
            var variable = AccessRuntimeVariable(name);
            return variable?.Value;
        }

        public string Title(string name)
        {
            var variable = AccessRuntimeVariable(name);
            return variable?.Title;
        }
        
        public Texture Icon(string name)
        {
            var variable = AccessRuntimeVariable(name);
            return variable?.Icon;
        }

        public void Set(string name, object value)
        {
            var variable = AccessRuntimeVariable(name);
            if (variable == null) return;
            
            variable.Value = value;
            EventChange?.Invoke(name);
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private NameVariable AccessRuntimeVariable(string name)
        {
            var keys = name.Split('/', 2, StringSplitOptions.RemoveEmptyEntries);
            var firstKey = keys.Length > 0 ? keys[0] : string.Empty;
            
            var variable = Variables.GetValueOrDefault(firstKey);
        
            if (keys.Length <= 1) return variable;
            if (variable?.Value is not GameObject gameObject) return null;
            
            return null;

            // var variables = gameObject.Get<LocalNameVariables>();
            // return variables != null ? variables.Runtime.AccessRuntimeVariable(keys[1]) : null;
        }

        // IMPLEMENTATIONS: -----------------------------------------------------------------------

        public override IEnumerator<NameVariable> GetEnumerator()
        {
            return Variables.Values.GetEnumerator();
        }
    }
}