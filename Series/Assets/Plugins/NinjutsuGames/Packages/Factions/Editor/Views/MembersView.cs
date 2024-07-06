using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    public class MembersView : VisualElement
    {
        private static IIcon DEFAULT_ICON = new IconFaction(ColorTheme.Type.TextLight);
        private static IIcon COLORED_ICON = new IconFaction(ColorTheme.Type.TextNormal);
        private InfoMessage infoMessage;
        private readonly Faction _faction;
        private const float BUTTON_SIZE = 18f;
        protected readonly ContentBox m_Box;

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public MembersView(Faction faction)
        {
            m_Box = new ContentBox("Members", true);
            Add(m_Box);
            _faction = faction;
            
            Faction.EventStatusChange -= StatusChange;
            Faction.EventStatusChange += StatusChange;
            Faction.EventMembersCountChange -= MembersCountChange;
            Faction.EventMembersCountChange += MembersCountChange;
            Member.EventMembersChanged -= Rebuild;
            Member.EventMembersChanged += Rebuild;
            
            Rebuild();
        }

        private void MembersCountChange(Faction obj)
        {
            Rebuild();
        }

        private void StatusChange(Faction arg1, Faction arg2, string arg3)
        {
            Rebuild();
        }

        ~MembersView()
        {
            Faction.EventStatusChange -= StatusChange;
            Faction.EventMembersCountChange -= MembersCountChange;
            Member.EventMembersChanged -= Rebuild;
        }

        // IMPLEMENTATIONS: -----------------------------------------------------------------------

        private void Rebuild()
        {
            PaintMemberList();
        }
        
        private void PaintMemberList()
        {
            m_Box.Content.Clear();

            var itemsCount = _faction.GetMembers().Count;
            for (var i = 0; i < itemsCount; ++i)
            {
                var item =_faction.GetMembers()[i];
                var itemField = new ObjectField
                {
                    label = string.Empty,
                    value = item,
                    objectType = typeof(Member)
                };

                itemField.SetEnabled(false);
                m_Box.Content.Add(itemField);

                if (i < itemsCount - 1)
                {
                    m_Box.Content.Add(new SpaceSmaller());
                }
            }
        }
    }
}