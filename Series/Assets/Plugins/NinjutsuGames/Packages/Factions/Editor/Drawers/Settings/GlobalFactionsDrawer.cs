using System;
using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomPropertyDrawer(typeof(GlobalFactions))]
    public class GlobalFactionsDrawer : PropertyDrawer
    {
        private const float LABEL_WIDTH = 60f;
        private const float BUTTON_SIZE = 18f;
        private const float EXTRA_SPACE = 6f;
        private const float WIDTH_MULTIPLIER = 8f;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();

            var buttonRefresh = new Button(GlobalFactionsPostProcessor.RefreshFactions)
            {
                text = "Refresh",
                style = { height = 25 }
            };

            root.Add(new SpaceSmall());
            root.Add(buttonRefresh);
            root.Add(new SpaceSmall());

            var factionsProperty = property.FindPropertyRelative("m_Factions");

            var boxRelationships = new ContentBox("Faction Relationships", true);
            var boxFactions = new ContentBox("Factions", false);

            PaintRelationships(factionsProperty, boxRelationships);
            PaintFactionList(factionsProperty, boxFactions);
            
            root.Add(boxRelationships);
            root.Add(boxFactions);

            GlobalFactionsPostProcessor.EventRefresh += () =>
            {
                PaintFactionList(factionsProperty, boxFactions);
                PaintRelationships(factionsProperty, boxRelationships);
            };

            return root;
        }

        private void PaintFactionList(SerializedProperty property, ContentBox box)
        {
            if (property?.serializedObject == null) return;
            
            box.Content.Clear();

            property.serializedObject.Update();
            var itemsCount = property.arraySize;

            for (var i = 0; i < itemsCount; ++i)
            {
                var item = property.GetArrayElementAtIndex(i);
                var itemField = new ObjectField
                {
                    label = string.Empty,
                    value = item.objectReferenceValue,
                    objectType = typeof(Faction)
                };

                itemField.SetEnabled(false);
                box.Content.Add(itemField);

                if (i < itemsCount - 1)
                {
                    box.Content.Add(new SpaceSmaller());
                }
            }
        }

        private void PaintRelationships(SerializedProperty property, ContentBox box)
        {
            box.Content.Clear();

            property.serializedObject.Update();
            var factionsCount = property.arraySize;
            var longestLabelWidth = GetLongestLabelWidth(property);

            // Create header row
            var headerRow = new VisualElement { style = { flexDirection = FlexDirection.Row, overflow = Overflow.Hidden} };
            headerRow.Add(new VisualElement
                { style = { width = longestLabelWidth + 10, minWidth = longestLabelWidth + 10 } }); // Empty corner label

            for (var i = 0; i < factionsCount; i++)
            {
                var factionProperty = property.GetArrayElementAtIndex(i);
                var faction = factionProperty.objectReferenceValue as Faction;

                if (faction == null) continue;
                var labelContainer = new VisualElement
                {
                    style =
                    {
                        width = BUTTON_SIZE + EXTRA_SPACE,
                        height = longestLabelWidth + 10,
                        overflow = Overflow.Hidden,
                        minWidth = BUTTON_SIZE + EXTRA_SPACE,
                        justifyContent = Justify.FlexEnd,
                        alignItems = Align.FlexStart,
                        paddingBottom = 10,
                        paddingTop = 50,
                    }
                };

                var label = new Label(faction.Name)
                {
                    style =
                    {
                        unityTextAlign = TextAnchor.MiddleRight,
                        whiteSpace = WhiteSpace.NoWrap,
                        rotate = new StyleRotate(new Rotate(90)),
                        width = BUTTON_SIZE + EXTRA_SPACE,
                    }
                };

                var color = ColorTheme.Get(ColorTheme.Type.Dark);
                color.a = 0.3f;
                labelContainer.style.backgroundColor = i % 2 == 0 ? Color.clear : color;

                labelContainer.Add(label);
                
                // Add vertical line
                headerRow.Add(GetLine());
                headerRow.Add(labelContainer);
                
                // Add another vertical line if its the last one
                if (i == factionsCount - 1)
                {
                    headerRow.Add(GetLine());
                }
            }

            box.Content.style.paddingBottom = box.Content.style.paddingTop = 0;
            box.Content.style.paddingLeft = box.Content.style.paddingRight = 0;
            box.Content.style.unityOverflowClipBox = new StyleEnum<OverflowClipBox>(OverflowClipBox.ContentBox);
            box.Content.Add(headerRow);

            // Add horizontal line
            box.Content.Add(GetHorizontalLine());

            // Create rows for each faction
            for (var i = 0; i < factionsCount; i++)
            {
                var factionProperty = property.GetArrayElementAtIndex(i);
                var faction = factionProperty.objectReferenceValue as Faction;

                if (faction == null) continue;

                var row = new VisualElement
                {
                    style =
                    {
                        flexDirection = FlexDirection.Row,
                        overflow = Overflow.Hidden,
                    }
                };
                var label = new Label(faction.Name)
                {
                    style =
                    {
                        unityTextAlign = TextAnchor.MiddleRight,
                        width = longestLabelWidth + 10,
                        whiteSpace = WhiteSpace.NoWrap,
                        height = BUTTON_SIZE + EXTRA_SPACE,
                        paddingLeft = 10,
                        paddingRight = 5,
                    }
                };

                row.Add(label); // Row header

                for (var j = 0; j < factionsCount; j++)
                {
                    var targetFactionProperty = property.GetArrayElementAtIndex(j);
                    if(targetFactionProperty == null) continue;
                    if(targetFactionProperty.objectReferenceValue == null) continue;
                    
                    var targetFaction = targetFactionProperty.objectReferenceValue as Faction;
                    var relationshipStatus = faction.GetStatusTowards(targetFaction);
                    var stanceRef = FactionStance.GetStance(relationshipStatus);
                    var button = CreateRelationshipButton(stanceRef, faction, targetFaction);
                    button.style.marginTop = EXTRA_SPACE / 2;

                    button.RegisterCallback<ClickEvent>(evt =>
                    {
                        targetFactionProperty.serializedObject.Update();
                        stanceRef = stanceRef.GetNextStatus();
                        faction.SetRelationshipStatus(targetFaction, stanceRef.Key, true);
                        EditorUtility.SetDirty(faction);
                        button.style.backgroundColor = stanceRef.GetColor();
                        UpdateButtonTooltip(button, faction, targetFaction, stanceRef);

                        EditorUtility.SetDirty(targetFaction);
                        SerializationUtils.ApplyUnregisteredSerialization(targetFactionProperty.serializedObject);
                    });

                    // Add vertical line
                    row.Add(GetLine());
                    row.Add(button);
                    
                    // Add another vertical line if its the last one
                    if (j == factionsCount - 1)
                    {
                        row.Add(GetLine());
                    }
                }

                // Alternate row background color
                var color = ColorTheme.Get(ColorTheme.Type.Dark);
                color.a = 0.3f;
                row.style.backgroundColor = i % 2 == 0 ? color : Color.clear;

                box.Content.Add(row);

                // Add horizontal line
                // Don't draw line after last row
                if (i != factionsCount - 1)
                {
                    box.Content.Add(GetHorizontalLine());
                }
            }
            // box.Content.Add(new SpaceCustom(20));
        }

        private VisualElement GetLine()
        {
            var color = ColorTheme.GetDarker(ColorTheme.Type.Dark);
            color.a = 0.3f;
            var line = new VisualElement { style = { width = 1f, backgroundColor = color } };
            return line;
        }

        private VisualElement GetHorizontalLine()
        {
            var color = ColorTheme.GetDarker(ColorTheme.Type.Dark);
            color.a = 0.3f;
            var line = new VisualElement() { style = { height = 1f, backgroundColor = color } };
            return line;
        }

        private float GetLongestLabelWidth(SerializedProperty property)
        {
            property.serializedObject.Update();
            var factionsCount = property.arraySize;
            var longestLabelWidth = LABEL_WIDTH;

            for (var i = 0; i < factionsCount; i++)
            {
                var factionProperty = property.GetArrayElementAtIndex(i);
                var faction = factionProperty.objectReferenceValue as Faction;
                if (faction == null) continue;
                var labelSize = new Vector2(faction.Name.Length * WIDTH_MULTIPLIER, BUTTON_SIZE);
                // var labelSize = new Label(faction.Name).MeasureTextSize(faction.Name, LABEL_WIDTH, VisualElement.MeasureMode.AtMost, BUTTON_SIZE, VisualElement.MeasureMode.Undefined);
                if (labelSize.x > longestLabelWidth)
                {
                    longestLabelWidth = labelSize.x;
                }
            }

            return longestLabelWidth;
        }

        

        public static Button CreateRelationshipButton(FactionStance status, Faction faction,
            Faction targetFaction)
        {
            var button = new Button
            {
                style =
                {
                    width = BUTTON_SIZE,
                    height = BUTTON_SIZE
                }
            };
            var borderStyle = new StyleLength(BUTTON_SIZE / 2);
            button.style.borderTopLeftRadius = borderStyle;
            button.style.borderTopRightRadius = borderStyle;
            button.style.borderBottomLeftRadius = borderStyle;
            button.style.borderBottomRightRadius = borderStyle;
            
            button.style.paddingBottom = button.style.paddingTop = button.style.paddingLeft = button.style.paddingRight = 2;
            
            button.style.backgroundColor = status.GetColor();
            
            var normalColor = new StyleColor(ColorTheme.Get(ColorTheme.Type.Dark));
            SetBorderColor(button, normalColor);
            SetBorderWidth(button, 1);

            var hoverColor = new StyleColor(ColorTheme.Get(ColorTheme.Type.Blue));
            
            button.RegisterCallback<MouseEnterEvent>(evt =>
            {
                SetBorderWidth(button, 2);
                SetBorderColor(button, hoverColor);
            });
            button.RegisterCallback<MouseLeaveEvent>(evt =>
            {
                SetBorderWidth(button, 1);
                SetBorderColor(button, normalColor);
            });
            
            UpdateButtonTooltip(button, faction, targetFaction, status);
            return button;
        }

        internal static void SetBorderWidth(VisualElement button, StyleFloat borderWidth)
        {
            button.style.borderBottomWidth = borderWidth;
            button.style.borderTopWidth = borderWidth;
            button.style.borderLeftWidth = borderWidth;
            button.style.borderRightWidth = borderWidth;
        }

        internal static void SetBorderColor(VisualElement button, StyleColor color)
        {
            button.style.borderBottomColor = color;
            button.style.borderTopColor = color;
            button.style.borderLeftColor = color;
            button.style.borderRightColor = color;
        }
        
        internal static Color AlternateColor(int index)
        {
            var regular = ColorTheme.Get(ColorTheme.Type.Dark);
            regular.a = 0.3f;
            
            var alternate = ColorTheme.Get(ColorTheme.Type.Dark);
            alternate.a = 0.05f;
            return index % 2 == 0 ? regular : alternate;
        }

        internal static void UpdateButtonTooltip(VisualElement button, Faction faction, Faction targetFaction,
            FactionStance stance)
        {
            button.tooltip = $"<b>{faction.Name}</b> towards <b>{targetFaction.Name}</b> = <color='#{ColorUtility.ToHtmlStringRGB(stance.GetColor())}'>{stance}</color>";
        }
    }
}