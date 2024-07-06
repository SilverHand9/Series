using System;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NinjutsuGames.Runtime.Factions.UnityUI
{
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Selectable))]

    public class SelectableHelper : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        private event Action EventSelect;
        private event Action EventDeselect;

        // INITIALIZERS: --------------------------------------------------------------------------

        private void Awake()
        {
            hideFlags = (
                HideFlags.HideInInspector |
                HideFlags.HideInHierarchy |
                HideFlags.DontSave
            );
        }

        private void OnDestroy()
        {
            EventSelect = null;
            EventDeselect = null;
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void Register(Selectable selectable, Action onSelect, Action onDeselect)
        {
            if (selectable == null) return;

            var helper = selectable.Get<SelectableHelper>();
            if (!helper) helper = selectable.Add<SelectableHelper>();

            helper.EventSelect = null;
            helper.EventDeselect = null;

            helper.EventSelect += onSelect;
            helper.EventDeselect += onDeselect;
        }

        // CALLBACKS: -----------------------------------------------------------------------------

        void ISelectHandler.OnSelect(BaseEventData eventData)
        {
            EventSelect?.Invoke();
        }

        void IDeselectHandler.OnDeselect(BaseEventData eventData)
        {
            EventDeselect?.Invoke();
        }
    }
}