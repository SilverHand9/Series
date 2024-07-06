using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using UnityEngine;

namespace NinjutsuGames.Runtime.Factions
{
    [AddComponentMenu("")]
    public class FactionsManager : Singleton<FactionsManager>, IGameSave
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnSubsystemsInit()
        {
            Instance.WakeUp();
        }
        
        // PROPERTIES: ----------------------------------------------------------------------------

        [field: NonSerialized] private Dictionary<IdString, NameVariableRuntime> Values { get; set; }
        [field: NonSerialized] private HashSet<IdString> SaveValues { get; set; }

        // INITIALIZERS: --------------------------------------------------------------------------

        protected override void OnCreate()
        {
            base.OnCreate();

            Values = new Dictionary<IdString, NameVariableRuntime>();
            SaveValues = new HashSet<IdString>();

            var factions = Settings.From<FactionsRepository>().Factions.Factions;
            foreach (var entry in factions)
            {
                if (!entry) return;
                entry.RuntimeInit();
                Instance.RequireInit(entry);   
            }

            _ = SaveLoadManager.Subscribe(this);
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public bool Exists(Faction asset, string name)
        {
            return Values.TryGetValue(
                asset.UniqueID,
                out var runtime
            ) && runtime.Exists(name);
        }
        
        public object Get(Faction asset, string name)
        {
            return Values.TryGetValue(asset.UniqueID, out var runtime) 
                ? runtime.Get(name) 
                : null;
        }
        
        public string Title(Faction asset, string name)
        {
            return Values.TryGetValue(asset.UniqueID, out var runtime) 
                ? runtime.Title(name) 
                : string.Empty;
        }
        
        public Texture Icon(Faction asset, string name)
        {
            return Values.TryGetValue(asset.UniqueID, out var runtime) 
                ? runtime.Icon(name) 
                : null;
        }

        public void Set(Faction asset, string name, object value)
        {
            if (!Values.TryGetValue(asset.UniqueID, out var runtime)) return;
            
            runtime.Set(name, value);
            if (asset.Save) SaveValues.Add(asset.UniqueID);
        }

        public void Register(Faction asset, Action<string> callback)
        {
            if (Values.TryGetValue(asset.UniqueID, out var runtime))
            {
                runtime.EventChange += callback;
            }
        }
        
        public void Unregister(Faction asset, Action<string> callback)
        {
            if (Values.TryGetValue(asset.UniqueID, out var runtime))
            {
                runtime.EventChange -= callback;
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void RequireInit(Faction asset)
        {
            if (Values.ContainsKey(asset.UniqueID)) return;
            
            var runtime = new NameVariableRuntime(asset.NameList);
            runtime.OnStartup();

            Values[asset.UniqueID] = runtime;
        }

        // IGAMESAVE: -----------------------------------------------------------------------------

        public string SaveID => "factions-name-variables";

        public LoadMode LoadMode => LoadMode.Greedy;
        public bool IsShared => false;

        public Type SaveType => typeof(SaveGroupFactions);

        public object GetSaveData(bool includeNonSavable)
        {
            var saveValues = new Dictionary<string, NameVariableRuntime>();
            var saveRelationships = new Dictionary<string, List<FactionRelationshipData>>();
                        
            foreach (var entry in Values)
            {
                var asset = Settings.From<FactionsRepository>().Factions.Get(entry.Key);
                if (includeNonSavable)
                {
                    saveValues[entry.Key.String] = entry.Value;
                    saveRelationships[entry.Key.String] = asset.Relationships;
                    continue;
                }

                if (!asset || !asset.Save) continue;
                
                saveValues[entry.Key.String] = entry.Value;
                saveRelationships[entry.Key.String] = asset.Relationships;
            }

            var saveData = new SaveGroupFactions(saveValues, saveRelationships);
            return saveData;
        }

        public Task OnLoad(object value)
        {
            if (value is not SaveGroupFactions saveData) return Task.FromResult(false);
            
            var repository = Settings.From<FactionsRepository>().Factions;
        
            var numGroups = saveData.Count();
            for (var i = 0; i < numGroups; ++i)
            {
                var uniqueID = new IdString(saveData.GetID(i));
                var candidates = saveData.GetData(i).Variables;
                
                // Restore faction relationships
                if (Values.TryGetValue(uniqueID, out var runtime))
                {
                    var relationships = saveData.GetData(i).RelationShips;
                    var asset = repository.Get(uniqueID);
                    asset.OnLoadRelationships(relationships);
                }

                if (!Values.TryGetValue(uniqueID, out var variables))
                {
                    continue;
                }
                
                foreach (var candidate in candidates)
                {
                    if (!variables.Exists(candidate.Name)) continue;
                    variables.Set(candidate.Name, candidate.Value);
                }
            }
            
            return Task.FromResult(true);
        }
    }
}