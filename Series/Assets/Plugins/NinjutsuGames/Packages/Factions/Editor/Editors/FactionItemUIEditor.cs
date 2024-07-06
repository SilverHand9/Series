using NinjutsuGames.Runtime.Factions.UnityUI;
using UnityEditor;

namespace NinjutsuGames.Editor.Factions
{
    [CustomEditor(typeof(FactionItemUI))]
    public class FactionItemUIEditor : TFactionUIEditor
    {
        protected override string Message =>
            "This component is configured by its 'Faction List UI' parent component";
    }
}