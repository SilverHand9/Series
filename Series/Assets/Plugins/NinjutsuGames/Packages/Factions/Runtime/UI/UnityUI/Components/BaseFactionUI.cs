using System;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions.UnityUI
{
    public abstract class BaseFactionUI : MonoBehaviour
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        [field: NonSerialized] public Member Member { get; protected set; }
        [field: NonSerialized] public Faction Faction { get; protected set; }
    }
}