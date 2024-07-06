using System.Collections.Generic;
using System.Linq;
using GameCreator.Editor.Common;
using GameCreator.Editor.Variables;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomEditor(typeof(Faction))]
    public class FactionEditor : TGlobalVariablesEditor
    {
        private const string USS_PATH = EditorPaths.VARIABLES + "StyleSheets/RuntimeGlobalList";
        
        private const string NAME_LIST = "GC-RuntimeGlobal-List-Head";
        private const string CLASS_LIST_ELEMENT = "gc-runtime-global-list-element";
        
        private static IIcon DEFAULT_ICON;
        private const string ERR_DUPLICATE_ID = "Another Faction has the same ID as this one";
        
        private PropertyField m_FieldId;
        private ContentBox boxRelationships;

        private const string KEY_EXPANDED = "gc:faction-tab:is-expanded:{0}:{1}";

        private void SetIsExpanded(string id, bool isExpanded)
        {
            SessionState.SetBool(KeyIsExpanded(id), isExpanded);
        }
        
        private string KeyIsExpanded(string id)
        {
            if (serializedObject?.targetObject == null) return default;
            
            return string.Format(
                KEY_EXPANDED, 
                serializedObject.targetObject.GetInstanceID(),
                id
            );
        }

        private bool IsExpanded(string id)
        {
            return SessionState.GetBool(KeyIsExpanded(id), false);
        }

        ~FactionEditor()
        {
            Faction.EventStatusChange -= RepaintRelationships;
        }

        public override VisualElement CreateInspectorGUI()
        {
            DEFAULT_ICON ??= new IconFaction(ColorTheme.Type.TextLight);

            var root =  new VisualElement();
            
            var sheets = StyleSheetUtils.Load();
            foreach (var sheet in sheets) root.styleSheets.Add(sheet);
            
            root.Add(new SpaceSmallest());

            var relationshipsProperty = serializedObject.FindProperty("relationships");

            var boxReputation = new ContentBox("Reputation Thresholds", IsExpanded("reputation"));
            var reputationProperty = serializedObject.FindProperty("reputation");
            var reputationField = new PropertyField(reputationProperty);
            boxReputation.Content.Clear();
            boxReputation.Content.Add(reputationField);
            boxReputation.Children().ElementAt(0).RegisterCallback<ClickEvent>(_ =>
            {
                SetIsExpanded("reputation", boxReputation.IsExpanded);
            });
            
            var boxVariables = new ContentBox("Faction Variables", IsExpanded("variables"));
            boxVariables.Content.Clear();
            boxVariables.Content.Add(base.CreateInspectorGUI());
            boxVariables.Children().ElementAt(0).RegisterCallback<ClickEvent>(_ =>
            {
                SetIsExpanded("variables", boxVariables.IsExpanded);
            });
            
            boxRelationships = new ContentBox("Faction Relationships", IsExpanded("relationships"));
            PaintRelationships(relationshipsProperty, boxRelationships);
            boxRelationships.Children().ElementAt(0).RegisterCallback<ClickEvent>(_ =>
            {
                SetIsExpanded("relationships", boxRelationships.IsExpanded);
            });
            
            Faction.EventStatusChange -= RepaintRelationships;
            Faction.EventStatusChange += RepaintRelationships;

            // Name field
            var nameProperty = serializedObject.FindProperty("title");
            var nameField = new PropertyField(nameProperty);
            root.Add(nameField);
            
            // Description field
            var descriptionProperty = serializedObject.FindProperty("description");
            var descriptionField = new PropertyField(descriptionProperty);
            root.Add(descriptionField);
            
            // Color field
            var colorProperty = serializedObject.FindProperty("color");
            var colorField = new PropertyField(colorProperty);
            root.Add(colorField);
            
            // Sprite field
            var spriteProperty = serializedObject.FindProperty("sprite");
            var spriteField = new PropertyField(spriteProperty);
            root.Add(spriteField);
            
            var type = serializedObject.FindProperty("m_Type");
            var order = serializedObject.FindProperty("m_SortOrder");
            
            root.Add(new SpaceSmall());
            root.Add(new PropertyField(type));
            root.Add(new PropertyField(order));
            
            var uniqueId = serializedObject.FindProperty(PROP_SAVE_UNIQUE_ID);
            m_FieldId = new PropertyField(uniqueId);

            root.Add(new SpaceSmall());
            root.Add(m_MessageID);
            root.Add(m_FieldId);
            root.Add(new SpaceSmall());
            
            m_MessageID.style.display = DisplayStyle.None;
            
            RefreshErrorIDFaction();
            m_FieldId.RegisterValueChangeCallback(_ =>
            {
                RefreshErrorIDFaction();
            });
            
            root.Add(boxReputation);
            root.Add(boxRelationships);
            root.Add(boxVariables);
            
            var playMode = EditorApplication.isPlayingOrWillChangePlaymode && !PrefabUtility.IsPartOfPrefabAsset(target);
            if (playMode)
            {
                root.Add(new MembersView(target as Faction));
            }
            
            return root;
        }

        private void RepaintRelationships(Faction arg1, Faction arg2, string arg3)
        {
            PaintRelationships(serializedObject.FindProperty("relationships"), boxRelationships);
        }
        
        private void RefreshErrorIDFaction()
        {
            serializedObject.Update();
            m_MessageID.style.display = DisplayStyle.None;

            var id = serializedObject.FindProperty(PROP_SAVE_UNIQUE_ID);
            
            var itemID = id
                .FindPropertyRelative(SaveUniqueIDDrawer.PROP_UNIQUE_ID)
                .FindPropertyRelative(UniqueIDDrawer.SERIALIZED_ID)
                .FindPropertyRelative(IdStringDrawer.NAME_STRING)
                .stringValue;

            var guids = AssetDatabase.FindAssets($"t:{nameof(Faction)}");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var faction = AssetDatabase.LoadAssetAtPath<Faction>(path);

                if (faction.UniqueID.String == itemID && faction != target)
                {
                    m_MessageID.Text = ERR_DUPLICATE_ID;
                    m_MessageID.style.display = DisplayStyle.Flex;
                    return;
                }
            }
        }
        
        /// <summary>
        /// This is called when a new variable is created or deleted in a faction, all factions should have the same variables
        /// So we need to update all factions to have the same variables as the one that was changed or deleted
        /// </summary>
        private void RefreshVariables()
        {
            serializedObject.Update();

            var thisVars = (target as Faction)?.NameList;
            var thisVarNames = new List<string>(thisVars.Names);

            var guids = AssetDatabase.FindAssets($"t:{nameof(Faction)}");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var faction = AssetDatabase.LoadAssetAtPath<Faction>(path);

                if (faction != target)
                {
                    // Compare nameList with thisVars and the ones it doesn't have or the ones deleted
                    // This should not replace the current value if already has it
                    
                    var factionVars = faction.NameList;
                    var factionVarName = new List<string>(factionVars.Names);

                    for (var i = 0; i < thisVars.Length; i++)
                    {
                        var id = thisVars.Names[i];
                        if(factionVarName.Contains(id)) continue;

                        var copy = thisVars.Get(i).Copy as NameVariable;
                        factionVars.Add(copy);
                    }
                    
                    for (var i = 0; i < factionVars.Length; i++)
                    {
                        var id = factionVars.Names[i];
                        if (thisVarNames.Contains(id)) continue;
                        
                        factionVars.Remove(i);
                    }
                    
                    for (var i = 0; i < thisVars.Length; i++)
                    {
                        var id = thisVars.Names[i];
                        var hasIt = factionVarName.Contains(id);
                        if(i >= factionVars.Length) continue;
                        if (hasIt && factionVars.Get(i).TypeID.Hash != thisVars.Get(i).TypeID.Hash)
                        {
                            factionVars.Remove(i);
                            
                            var copy = thisVars.Get(i).Copy as NameVariable;
                            factionVars.Add(copy);
                        }
                    }
                    
                    EditorUtility.SetDirty(faction);
                }
            }
        }
        
        private void PaintRelationships(SerializedProperty property, ContentBox box)
        {
            if(property == null) return;
            
            box.Content.Clear();
            
            var thisFaction = target as Faction;
            if (!thisFaction) return;
            
            property.serializedObject.Update();
            var itemsCount = property.arraySize;

            for (var i = 0; i < itemsCount; ++i)
            {
                var relationshipProperty = property.GetArrayElementAtIndex(i);
                var factionProperty = relationshipProperty.FindPropertyRelative("faction");
                var stanceProperty = relationshipProperty.FindPropertyRelative("stance");

                var faction = factionProperty.objectReferenceValue as Faction;

                if (faction == null) continue;
                
                var relationshipElement = PaintFaction(i, thisFaction, faction, stanceProperty);
                box.Content.Add(relationshipElement);
            }
        }

        private static VisualElement PaintFaction(int i, Faction thisFaction, Faction faction,
            SerializedProperty stanceProperty)
        {
            var element = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    alignItems = Align.Center,
                    paddingLeft = 4
                }
            };
            element.AddToClassList(CLASS_LIST_ELEMENT);
            element.style.paddingBottom = element.style.paddingTop = 2;
                    
            // Apply alternate background color
            element.style.backgroundColor = GlobalFactionsDrawer.AlternateColor(i);

            const int border = 4;
            element.style.borderBottomLeftRadius = border;
            element.style.borderBottomRightRadius = border;
            element.style.borderTopLeftRadius = border;
            element.style.borderTopRightRadius = border;
            
            element.style.marginBottom = 2;

            var icon = new Image
            {
                image = DEFAULT_ICON.Texture
            };
            icon.style.width = icon.style.height = 16;
            var iconColor = icon.style.unityBackgroundImageTintColor;
            var value = iconColor.value;
            value.a = 0.5f;
            iconColor.value = value;
            icon.style.unityBackgroundImageTintColor = iconColor;

            var stance = thisFaction?.GetStatusTowards(faction);
            var stanceRef = FactionStance.GetStance(stance);

            var label = new Label($"Status towards <b>{faction.Name}</b>")
            {
                style =
                {
                    unityTextAlign = TextAnchor.MiddleLeft,
                    flexGrow = 1,
                    paddingLeft = 4
                }
            };
            element.Add(icon);
            element.Add(label);

            const int radius = 4;
            var statusLabel = new Label($"{stance}")
            {
                style =
                {
                    unityTextAlign = TextAnchor.MiddleRight,
                    paddingLeft = 4,
                    paddingRight = 4,
                    paddingBottom = 2,
                    paddingTop = 2,
                    color = stanceRef.GetColor(),
                    backgroundColor = ColorTheme.Get(ColorTheme.Type.Dark),
                    borderBottomLeftRadius = radius,
                    borderBottomRightRadius = radius,
                    borderTopLeftRadius = radius,
                    borderTopRightRadius = radius,
                    fontSize = 11,
                }
            };
            element.Add(statusLabel);

            var button = GlobalFactionsDrawer.CreateRelationshipButton(stanceRef, thisFaction, faction);
            button.RegisterCallback<ClickEvent>(evt =>
            {
                stanceRef = stanceRef.GetNextStatus();
                stanceProperty.serializedObject.Update();
                stanceProperty.stringValue = stanceRef.Key;
                if(EditorApplication.isPlaying) thisFaction.SetRelationshipStatus(faction, stanceRef.Key, true);
                SerializationUtils.ApplyUnregisteredSerialization(stanceProperty.serializedObject);
                        
                button.style.backgroundColor = statusLabel.style.color = stanceRef.GetColor();
                statusLabel.text = $"{stanceRef.Key}";
                GlobalFactionsDrawer.UpdateButtonTooltip(button, faction, thisFaction, stanceRef);
            });
            element.Add(button);
            return element;
        }
        
        // PAINT EDITOR: --------------------------------------------------------------------------

        protected override void PaintEditor()
        {
            var nameList = serializedObject.FindProperty("m_NameList");
            var fieldNameList = new PropertyField(nameList);

            fieldNameList.RegisterValueChangeCallback(_ =>
            {
                EditorApplication.delayCall -= RefreshVariables;
                EditorApplication.delayCall += RefreshVariables;
            });
            
            m_Body.Add(fieldNameList);
            m_Body.Add(m_MessageID);
        }
        
        // PAINT RUNTIME: -------------------------------------------------------------------------

        protected override void PaintRuntime()
        {
            var faction = target as Faction;
            if (!faction) return;
            
            faction.Unregister(RuntimeOnChange);
            faction.Register(RuntimeOnChange);
            
            RuntimeOnChange(string.Empty);
        }

        private void RuntimeOnChange(string variableName)
        {
            m_Body.Clear();
            m_Body.styleSheets.Clear();
            
            var sheets = StyleSheetUtils.Load(USS_PATH);
            foreach (var styleSheet in sheets) m_Body.styleSheets.Add(styleSheet);

            var content = new VisualElement
            {
                name = NAME_LIST
            };

            var faction = target as Faction;
            if (!faction) return;

            var names = faction.Names;
            foreach (var id in names)
            {
                var image = new Image
                {
                    image = faction.VariableIcon(id)
                };
            
                var title = new Label(faction.VariableTitle(id))
                {
                    style =
                    {
                        color = ColorTheme.Get(ColorTheme.Type.TextNormal)
                    }
                };

                var element = new VisualElement();
                element.AddToClassList(CLASS_LIST_ELEMENT);

                element.Add(image);
                element.Add(title);
            
                content.Add(element);
            }
            
            m_Body.Add(content);
        }
    }
}