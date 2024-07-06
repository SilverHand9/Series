using System;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class FactionStanceAttribute : PropertyAttribute
    {
        public FactionStanceAttribute() { }
    }
}