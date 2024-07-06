using GameCreator.Editor.Common;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{

    public abstract class TFactionUIEditor : UnityEditor.Editor
    {
        protected abstract string Message { get; }
        
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            if(!string.IsNullOrEmpty(Message))
            {
                var info = new InfoMessage(Message);
                root.Add(info);
            }

            CreateAdditionalProperties(root);

            var title = serializedObject.FindProperty("m_Title");
            var description = serializedObject.FindProperty("m_Description");
            var memberCount = serializedObject.FindProperty("m_MemberCount");
            var color = serializedObject.FindProperty("m_Color");
            var sprite = serializedObject.FindProperty("m_Sprite");
            
            root.Add(new SpaceSmall());
            root.Add(new PropertyField(title));
            root.Add(new PropertyField(description));
            root.Add(new PropertyField(memberCount));
            root.Add(new SpaceSmall());
            root.Add(new PropertyField(color));
            root.Add(new PropertyField(sprite));
            
            var reputationElements = this.serializedObject.FindProperty("m_ReputationElements");
            var activeElements = this.serializedObject.FindProperty("m_ActiveElements");
            var interactions = serializedObject.FindProperty("m_Interactions");

            root.Add(new SpaceSmall());
            root.Add(new PropertyField(reputationElements));
            root.Add(new PropertyField(activeElements));
            root.Add(new PropertyField(interactions));
            return root;
        }

        protected virtual void CreateAdditionalProperties(VisualElement root)
        { }
    }
}