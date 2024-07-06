using GameCreator.Editor.Installs;
using UnityEditor;

namespace NinjutsuGames.Editor.Factions
{
    public static class UninstallFactions
    {
        private const string UNINSTALL_TITLE = "Are you sure you want to uninstall {0}";
        private const string UNINSTALL_MSG = "This operation cannot be undone";
        
        [MenuItem(
            itemName: "Game Creator/Uninstall/Factions",
            isValidateFunction: false,
            priority: UninstallManager.PRIORITY
        )]
        
        private static void Uninstall()
        {
            UninstallManager.Uninstall("Factions");

            var moduleFolder = "Factions";
            var path = "Assets/Plugins/NinjutsuGames/Packages/" + moduleFolder;
            if (!AssetDatabase.IsValidFolder(path)) return;

            var delete = EditorUtility.DisplayDialog(
                string.Format(UNINSTALL_TITLE, moduleFolder),
                UNINSTALL_MSG, 
                "Yes", "Cancel"
            );
            
            if (!delete) return;
            
            AssetDatabase.MoveAssetToTrash(path);
        }
    }
}