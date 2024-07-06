using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions.UnityUI;
using UnityEditor;

namespace NinjutsuGames.Editor.Factions
{
    [CustomPropertyDrawer(typeof(TActiveUI), true)]
    public class TActiveUIDrawer : TBoxDrawer
    {
        protected override string Name(SerializedProperty property) => "Active Elements";
    }
}