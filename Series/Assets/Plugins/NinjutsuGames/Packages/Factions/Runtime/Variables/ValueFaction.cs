using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Image(typeof(IconFaction), ColorTheme.Type.Blue)]
    [Title("Faction")]
    [Category("Factions/Faction")]
    
    [Serializable]
    public class ValueFaction : TValue
    {
        public static readonly IdString TYPE_ID = new("faction");
        
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private Faction m_Value;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public override IdString TypeID => TYPE_ID;
        public override Type Type => typeof(Faction);
        
        public override bool CanSave => false;

        public override TValue Copy => new ValueFaction
        {
            m_Value = m_Value
        };
        
        // CONSTRUCTORS: --------------------------------------------------------------------------
        
        public ValueFaction() : base()
        { }

        public ValueFaction(Faction value) : this()
        {
            m_Value = value;
        }

        // OVERRIDE METHODS: ----------------------------------------------------------------------

        protected override object Get()
        {
            return m_Value;
        }

        protected override void Set(object value)
        {
            m_Value = value is Faction cast ? cast : null;
        }
        
        public override string ToString()
        {
            return m_Value != null ? m_Value.name : "(none)";
        }
        
        // REGISTRATION METHODS: ------------------------------------------------------------------

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void RuntimeInit() => RegisterValueType(
            TYPE_ID, 
            new TypeData(typeof(ValueFaction), CreateValue), 
            typeof(Faction)
        );
        
        #if UNITY_EDITOR
        
        [UnityEditor.InitializeOnLoadMethod]
        private static void EditorInit() => RegisterValueType(
            TYPE_ID, 
            new TypeData(typeof(ValueFaction), CreateValue),
            typeof(Faction)
        );
        
        #endif

        private static ValueFaction CreateValue(object value)
        {
            return new ValueFaction(value as Faction);
        }
    }
}