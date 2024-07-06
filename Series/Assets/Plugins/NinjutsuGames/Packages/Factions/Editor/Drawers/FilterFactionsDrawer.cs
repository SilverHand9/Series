using System;
using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions.UnityUI.Classes;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    [CustomPropertyDrawer(typeof(FilterFactions))]
    public class FilterFactionsDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            
            var show = property.FindPropertyRelative("m_Show");
            var showHidden = property.FindPropertyRelative("m_ShowHidden");
            var hideUntracked = property.FindPropertyRelative("m_HideUntracked");
            
            root.Add(new SpaceSmaller());
            root.Add(new PropertyField(show));
            root.Add(new PropertyField(showHidden));
            root.Add(new PropertyField(hideUntracked));
            
            var filter = property.FindPropertyRelative("m_Filter");
            var localList = property.FindPropertyRelative("m_LocalList");
            var globalList = property.FindPropertyRelative("m_GlobalList");

            var fieldFilter = new PropertyField(filter);
            var fieldLocalList = new PropertyField(localList);
            var fieldGlobalList = new PropertyField(globalList);
            
            root.Add(new SpaceSmaller());
            root.Add(fieldFilter);
            root.Add(fieldLocalList);
            root.Add(fieldGlobalList);

            fieldLocalList.style.display = filter.enumValueIndex switch
            {
                0 => DisplayStyle.None,
                1 => DisplayStyle.Flex,
                2 => DisplayStyle.None,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            fieldGlobalList.style.display = filter.enumValueIndex switch
            {
                0 => DisplayStyle.None,
                1 => DisplayStyle.None,
                2 => DisplayStyle.Flex,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            fieldFilter.RegisterValueChangeCallback(changeEvent =>
            {
                fieldLocalList.style.display = changeEvent.changedProperty.enumValueIndex switch
                {
                    0 => DisplayStyle.None,
                    1 => DisplayStyle.Flex,
                    2 => DisplayStyle.None,
                    _ => throw new ArgumentOutOfRangeException()
                };
            
                fieldGlobalList.style.display = changeEvent.changedProperty.enumValueIndex switch
                {
                    0 => DisplayStyle.None,
                    1 => DisplayStyle.None,
                    2 => DisplayStyle.Flex,
                    _ => throw new ArgumentOutOfRangeException()
                };
            });

            return root;
        }
    }
}