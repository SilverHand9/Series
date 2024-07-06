using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Common.UnityUI;
using UnityEngine;
using UnityEngine.UI;

namespace NinjutsuGames.Runtime.Factions.UnityUI
{
    
    [DisallowMultipleComponent]
    
    [DefaultExecutionOrder(ApplicationManager.EXECUTION_ORDER_LAST_LATER)]
    
    [Serializable]
    public abstract class TFactionUI : BaseFactionUI
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private TextReference m_Title = new TextReference();
        [SerializeField] private TextReference m_Description = new TextReference();
        [SerializeField] private TextReference m_MemberCount = new TextReference();
        [SerializeField] private Graphic m_Color;
        [SerializeField] private Image m_Sprite;

        // [SerializeField] private FormatQuestUI m_StyleGraphics = new();
        [SerializeField] private ReputationFactionUI m_ReputationElements = new();
        [SerializeField] private ActiveFactionUI m_ActiveElements = new();
        [SerializeField] private InteractionFactionUI m_Interactions = new();
        private Args args;
        
        // INITIALIZERS: --------------------------------------------------------------------------

        protected virtual void OnEnable()
        {
            m_ActiveElements.OnEnable();
            Faction.EventMembersCountChange += MemberCountChange;
        }
        
        protected virtual void OnDisable()
        {
            m_ActiveElements.OnDisable();
            Faction.EventMembersCountChange -= MemberCountChange;
        }
        
        private void MemberCountChange(Faction obj)
        {
            Refresh();
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Select()
        {
            FactionItemUI.SelectFactionUI(Member, Faction);
            Refresh();
        }
        
        public void Deselect()
        {
            if (FactionItemUI.UI_LastFactionSelected != Faction) return;
            Refresh();
            FactionItemUI.DeselectFactionUI();
        }
        
        // PROTECTED METHODS: ---------------------------------------------------------------------
        
        protected virtual void Refresh(Member member, Faction faction)
        {
            if (!faction) return;

            Member = member;
            Faction = faction;

            args = member ? new Args(gameObject, member.gameObject) : new Args(gameObject);
            m_Interactions.Setup(this);
            
            var factionName = Faction.GetTitle(args);
            if(string.IsNullOrEmpty(factionName)) factionName = Faction.Name;
            m_Title.Text = factionName;
            m_Description.Text = Faction.GetDescription(args);
            m_MemberCount.Text = Faction.GetMembers().Count.ToString();

            Refresh();
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        protected void Refresh()
        {
            if(!Faction) return;
            
            m_ActiveElements.Refresh(Faction);
            
            m_ReputationElements.Refresh(Member, Faction);
            if (m_Color) m_Color.color = Faction.GetColor(args);
            if (m_Sprite) m_Sprite.overrideSprite = Faction.GetSprite(args);
            
            var members = Faction.GetMembers();
            m_MemberCount.Text = members != null ? members.Count.ToString() : "0";
        }
    }
}