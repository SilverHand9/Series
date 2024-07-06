using System.Collections.Generic;
using System.Linq;
using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    public class FactionsView : TFactionMemberView
    {
        private static IIcon DEFAULT_ICON = new IconFaction(ColorTheme.Type.TextLight);
        private static IIcon COLORED_ICON = new IconFaction(ColorTheme.Type.TextNormal);
        private InfoMessage infoMessage;
        private readonly Member player;
        private const float BUTTON_SIZE = 18f;

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public FactionsView(Member member, bool joinedFactions = true, bool displayStatusTowardsPlayer = false) : base(member, joinedFactions, displayStatusTowardsPlayer)
        {;
            m_Member.EventChange -= Rebuild;
            m_Member.EventChange += Rebuild;
            m_Member.EventPointsChanged -= Rebuild;
            m_Member.EventPointsChanged += Rebuild;
            
            if(!Application.isPlaying) return;
            
            Faction.EventStatusChange -= StatusChange;
            Faction.EventStatusChange += StatusChange;
            
            if (ShortcutPlayer.Instance && ShortcutPlayer.Instance.gameObject != m_Member.gameObject)
            {
                player = ShortcutPlayer.Instance.GetComponent<Member>();
                player.EventChange -= Rebuild;
                player.EventChange += Rebuild;
            }
        }

        private void StatusChange(Faction arg1, Faction arg2, string arg3)
        {
            Rebuild();
        }

        ~FactionsView()
        {
            Faction.EventStatusChange -= StatusChange;

            if(player) player.EventChange -= Rebuild;
            
            if (!m_Member) return;
            m_Member.EventChange -= Rebuild;
            m_Member.EventPointsChanged -= Rebuild;
        }

        // IMPLEMENTATIONS: -----------------------------------------------------------------------

        protected override void Rebuild()
        {
            m_Box.Content.Clear();

            var factions = m_JoinedFactions ? m_Member.Factions : GetOtherFactions(m_Member.Factions);
            if (factions == null) return;
            var i = 0;
            foreach (var faction in factions)
            {
                m_Box.Content.Add(PaintFaction(i, m_Member, faction, m_JoinedFactions));
                i++;
            }

            if (!m_ShowStatusTowardsPlayer) return;
            if(!Application.isPlaying) return;
            if(!m_Member) return;
            if(!m_Member.gameObject) return;
            if(!ShortcutPlayer.Instance) return;
            if(!ShortcutPlayer.Instance.gameObject) return;

            var isPlayer = ShortcutPlayer.Instance && ShortcutPlayer.Instance.gameObject == m_Member.gameObject;
            if (isPlayer) return;
            
            var playerMember = ShortcutPlayer.Instance.GetComponent<Member>();
            var stance = playerMember ? m_Member.HighestStatusToMember(playerMember) : null;
            var status = stance != null ? stance.Key : "Unknown";
            var color = stance?.GetColor() ?? ColorTheme.Get(ColorTheme.Type.TextNormal);
                    
            var msg = $"Status towards Player: <b><color=#{ColorUtility.ToHtmlStringRGB(color)}>{status}</color></b>";

            if (infoMessage == null)
            {
                infoMessage = new InfoMessage(msg);
                m_Box.Add(infoMessage);
            }
            else infoMessage.Text = msg;
        }

        private static List<Faction> GetOtherFactions(List<Faction> exceptions)
        {
            var factions = Settings.From<FactionsRepository>().Factions.Factions.ToList();
            factions.RemoveAll(exceptions.Contains);
            return factions;
        }

        private static VisualElement PaintFaction(int i, Member member, Faction faction, bool joinedFaction)
        {
            if(!member) return null;
            
            var relationshipElement = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    alignItems = Align.Center,
                    paddingLeft = 4
                }
            };
            relationshipElement.style.paddingBottom = relationshipElement.style.paddingTop = 2;
                    
            // Apply alternate background color
            relationshipElement.style.backgroundColor = GlobalFactionsDrawer.AlternateColor(i);

            const int border = 4;
            relationshipElement.style.borderBottomLeftRadius = border;
            relationshipElement.style.borderBottomRightRadius = border;
            relationshipElement.style.borderTopLeftRadius = border;
            relationshipElement.style.borderTopRightRadius = border;
            
            relationshipElement.style.marginBottom = 2;

            var icon = new Image
            {
                image = joinedFaction ? COLORED_ICON.Texture : DEFAULT_ICON.Texture
            };
            icon.style.width = icon.style.height = 16;

            var stance = member.HighestStatusToFaction(faction);
            
            var label = new Label($"<b>{faction.Name}</b>")
            {
                style =
                {
                    unityTextAlign = TextAnchor.MiddleLeft,
                    flexGrow = 1,
                    paddingLeft = 4
                }
            };
            relationshipElement.Add(icon);
            relationshipElement.Add(label);

            const int radius = 4;
            var points = member.GetReputationPoints(faction);
            var statusLabel = new Label(faction.ReputationEnabled ? $"{stance.Key} ({points}/{faction.Reputation.GetNextThreshold(points)})" : $"{stance.Key}")
            {
                style =
                {
                    unityTextAlign = TextAnchor.MiddleRight,
                    paddingLeft = 4,
                    paddingRight = 4,
                    paddingBottom = 2,
                    paddingTop = 2,
                    color = stance.GetColor(),
                    backgroundColor = ColorTheme.Get(ColorTheme.Type.Dark),
                    borderBottomLeftRadius = radius,
                    borderBottomRightRadius = radius,
                    borderTopLeftRadius = radius,
                    borderTopRightRadius = radius,
                    fontSize = 11,
                }
            };
            relationshipElement.Add(statusLabel);

            relationshipElement.Add(CreateRelationshipElement(stance, member, faction));
            
            var button = new Button
            {
                text = joinedFaction ? "Leave" : "Join",
                style =
                {
                    width = 60,
                    height = 20,
                    marginLeft = 4
                }
            };
            button.RegisterCallback<ClickEvent>(evt =>
            {
                if(joinedFaction) member.LeaveFaction(faction);
                else member.JoinFaction(faction);
            });
            relationshipElement.Add(button);
            return relationshipElement;
        }

        private static VisualElement CreateRelationshipElement(FactionStance status, Member member, Faction targetFaction)
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
            GlobalFactionsDrawer.SetBorderColor(button, normalColor);
            GlobalFactionsDrawer.SetBorderWidth(button, 1);

            return button;
        }
    }
}