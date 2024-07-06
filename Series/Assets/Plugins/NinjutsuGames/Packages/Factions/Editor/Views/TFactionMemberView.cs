using GameCreator.Editor.Common;
using NinjutsuGames.Runtime.Factions;
using UnityEngine.UIElements;

namespace NinjutsuGames.Editor.Factions
{
    public abstract class TFactionMemberView : VisualElement
    {
        // MEMBERS: -------------------------------------------------------------------------------

        protected readonly Member m_Member;
        protected readonly ContentBox m_Box;
        protected bool m_JoinedFactions = true;
        protected readonly bool m_ShowStatusTowardsPlayer;

        // CONSTRUCTOR: ---------------------------------------------------------------------------
        
        protected TFactionMemberView(Member member, bool joinedFactions = true, bool displayStatusTowardsPlayer = false)
        {
            m_ShowStatusTowardsPlayer = displayStatusTowardsPlayer;
            m_JoinedFactions = joinedFactions;
            m_Member = member;
            
            m_Box = new ContentBox(joinedFactions ? "Active Factions" : "Other Factions", true);
            Rebuild();
            Add(m_Box);
        }
        
        // ABSTRACT METHODS: ----------------------------------------------------------------------

        protected abstract void Rebuild();
    }
}