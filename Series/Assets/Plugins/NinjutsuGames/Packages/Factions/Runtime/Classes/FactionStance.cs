using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [Serializable]
    public class FactionStance : TPolymorphicItem<FactionStance>
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        [SerializeField] private IdString m_Key;
        [SerializeField] private PropertyGetColor m_Color;
        [SerializeField] private PropertyGetInteger m_PointsRequired = new(new GetDecimalConstantZero());
        
        // Constructor
        public FactionStance(IdString key, PropertyGetColor color, PropertyGetInteger pointsRequired)
        {
            m_Key = key;
            m_Color = color;
            m_PointsRequired = pointsRequired;
        }

        public FactionStance()
        {
            m_Key = IdString.EMPTY;
            m_Color = GetColorColorsYellow.Create;
            m_PointsRequired = new PropertyGetInteger(new GetDecimalConstantZero());
        }

        // PROPERTIES: ----------------------------------------------------------------------------

        public string Key => m_Key.String;
        public int PointsRequired => (int)m_PointsRequired.Get(Args.EMPTY);

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public Color GetColor() => m_Color.Get(Args.EMPTY);
        public Color GetColor(Args args) => m_Color.Get(args);
        
        public static FactionStance GetStance(string key)
        {
            var instance = Settings.From<FactionsRepository>().Stances;
            for (var i = 0; i < instance.Length; ++i)
            {
                var stance = instance.Get[i];
                if (stance.Key == key) return stance;
            }

            return instance.Lowest;
        }
        
        // TO STRING: -----------------------------------------------------------------------------

        public override string ToString() => m_Key.ToString();
    }
}