using System;
using System.Collections.Generic;
using System.Linq;
using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEngine;

namespace NinjutsuGames.Editor.Factions
{
    public class GlobalFactionsPostProcessor : AssetPostprocessor
    {
        public static event Action EventRefresh;
        
        // PROCESSORS: ----------------------------------------------------------------------------

        [InitializeOnLoadMethod]
        private static void InitializeOnLoad()
        {
            SettingsWindow.InitRunners.Add(new InitRunner(
                SettingsWindow.INIT_PRIORITY_LOW,
                CanRefreshFactions,
                RefreshFactions
            ));
        }
        
        private static void OnPostprocessAllAssets(
            string[] importedAssets, 
            string[] deletedAssets, 
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            if (importedAssets.Length == 0 && deletedAssets.Length == 0) return;
            RefreshFactions();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static bool CanRefreshFactions()
        {
            return true;
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public static void RefreshFactions()
        {
            var varSettingsGuids = AssetDatabase.FindAssets($"t:{nameof(FactionsSettings)}");
            if (varSettingsGuids.Length == 0) return;

            var varSettingsPath = AssetDatabase.GUIDToAssetPath(varSettingsGuids[0]);
            
            var varSettings = AssetDatabase.LoadAssetAtPath<FactionsSettings>(varSettingsPath);
            if (varSettings == null) return;

            var factionGuids = AssetDatabase.FindAssets($"t:{nameof(Faction)}");
            var factions = new Faction[factionGuids.Length];
            
            for (var i = 0; i < factionGuids.Length; i++)
            {
                var nameVariablesGuid = factionGuids[i];
                var nameVariablesPath = AssetDatabase.GUIDToAssetPath(nameVariablesGuid);
                factions[i] = AssetDatabase.LoadAssetAtPath<Faction>(nameVariablesPath);
            }

            var factionList = factions.ToList();
            UpdateFactionRelationships(factionList);
            UpdateFactionReputationThresholds(factionList);

            var varSettingsSerializedObject = new SerializedObject(varSettings);
            var globalVariablesProperty = varSettingsSerializedObject
                .FindProperty(TAssetRepositoryEditor.NAMEOF_MEMBER)
                .FindPropertyRelative("m_Factions");

            var factionsProperty = globalVariablesProperty.FindPropertyRelative("m_Factions");
                
            factionsProperty.arraySize = factions.Length;
            for (var i = 0; i < factions.Length; ++i)
            {
                factionsProperty.GetArrayElementAtIndex(i).objectReferenceValue = factions[i];
            }
            
            varSettingsSerializedObject.ApplyModifiedPropertiesWithoutUndo();
            EventRefresh?.Invoke();
        }

        /// <summary>
        /// Update the reputation thresholds for all factions based on the list of stances
        /// Check if the thresholds are still valid and remove any that are not keeping existing valid thresholds
        /// </summary>
        /// <param name="factionList"></param>
        private static void UpdateFactionReputationThresholds(List<Faction> factionList)
        {
            foreach (var faction in factionList)
            {
                var thresholds = faction.Reputation.Thresholds;
                var toRemove = new List<ReputationThreshold>();
                foreach (var threshold in thresholds)
                {
                    if (!Settings.From<FactionsRepository>().Stances.Get.Any(stance => stance.Key.Equals(threshold.Stance)))
                    {
                        toRemove.Add(threshold);
                    }
                }
                foreach (var threshold in toRemove)
                {
                    faction.Reputation.Remove(faction.Reputation.IndexOf(threshold.Stance));
                }

                foreach (var stance in Settings.From<FactionsRepository>().Stances.Get)
                {
                    if (!thresholds.Any(t => t.Stance.Equals(stance.Key)))
                    {
                        faction.Reputation.Add(new ReputationThreshold(stance.Key, stance.PointsRequired));
                    }
                }
                EditorUtility.SetDirty(faction);
            }
        }

        private static void UpdateFactionRelationships(List<Faction> factions)
        {
            foreach (var faction in factions)
            {
                var toRemove = new List<FactionRelationshipData>();
                foreach (var relationship in faction.Relationships)
                {
                    if (!factions.Contains(relationship.faction))
                    {
                        toRemove.Add(relationship);
                    }
                }
                foreach (var relationship in toRemove)
                {
                    faction.Relationships.Remove(relationship);
                }

                foreach (var otherFaction in factions)
                {
                    if (!faction.Relationships.Exists(r => r.faction == otherFaction))
                    {
                        faction.Relationships.Add(new FactionRelationshipData(otherFaction, Settings.From<FactionsRepository>().Stances.Lowest.Key));
                        continue;
                    }
                    var stance = faction.GetStatusTowards(otherFaction);
                    if (string.IsNullOrEmpty(stance))
                    {
                        faction.SetRelationshipStatus(otherFaction, Settings.From<FactionsRepository>().Stances.Lowest.Key);
                    }
                }
                EditorUtility.SetDirty(faction);
            }
        }
    }
}