using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions.UnityUI;
using UnityEditor;

namespace NinjutsuGames.Editor.Factions
{
    
    [CustomPropertyDrawer(typeof(InteractionFactionUI), true)]
    public class InteractionsFactionUIDrawer : TBoxDrawer
    {
        protected override string Name(SerializedProperty property) => "Interactive Elements";
    }
}