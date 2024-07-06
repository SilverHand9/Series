using System;
//using System.Runtime.Remoting.Contexts;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace GameCreator.Runtime.VisualScripting
{
	[Version(1, 0, 1)]
    [Title("On Input System Interaction")]
    [Category("Input/On Input System Interaction")]
    [Description("Detects when an action of InputSystem performs an interaction. This is a modified script from [on input system] event. The detection is based on performed and canceled callback. If an inputaction has no interaction assigned, it will be treated as press, so both on press and release still work. Please check the input system manual for detailed timing of performed and canceled for each interaction type. Please note that OnHoldRelase only works with the Hold interaction type.")]

    [Image(typeof(IconButton), ColorTheme.Type.Blue)]
    [Keywords("Press", "Release", "Hold", "Tap", "Input")]
    [Keywords("Keyboard", "Mouse", "Button", "Gamepad", "Controller", "Joystick")]

    [Serializable]
	public class EventOnInputSystemInteraction : Event
    {

	    [SerializeField] private InputActionFromAsset m_Input = new InputActionFromAsset();
	    private enum InputEvent
	    {
		    OnPress, OnHold, OnHoldRelease, OnRelease, OnTap, OnMultiTap, OnSlowTap
	    }
	    [SerializeField] private InputEvent typeInput;
	    
       
       protected override void OnStart(Trigger trigger)
        {
            base.OnStart(trigger);
            //Enable the input action
	        m_Input.InputAction.Enable();
        }
        

        protected override void OnEnable(Trigger trigger)
        {		 

            m_Input.InputAction.performed += this.OnPerformed;
            m_Input.InputAction.canceled += this.OnCanceled;
        }
        
        protected override void OnDisable(Trigger trigger)
        {
            m_Input.InputAction.performed -= this.OnPerformed;
            m_Input.InputAction.canceled -= this.OnCanceled;
            //Disable the input action
            m_Input.InputAction.Disable();
        }

        protected void OnPerformed(InputAction.CallbackContext context)
        {

            if(context.interaction is HoldInteraction)
            {
                if (typeInput == InputEvent.OnHold)
                {
                    _ = this.m_Trigger.Execute(this.Self);
                }
            }
            else if(context.interaction is TapInteraction)
            {
                if (typeInput == InputEvent.OnTap)
                {
                    _ = this.m_Trigger.Execute(this.Self);
                }
            }
            else if(context.interaction is PressInteraction)
            {
                if (typeInput == InputEvent.OnPress)
                {
                    _ = this.m_Trigger.Execute(this.Self);
                }
            }
            else if(context.interaction is SlowTapInteraction)
            {
                if (typeInput == InputEvent.OnSlowTap)
                {
                    _ = this.m_Trigger.Execute(this.Self);
                }
            }
            else if(context.interaction is MultiTapInteraction)
            {
                if (typeInput == InputEvent.OnMultiTap)
                {
                    _ = this.m_Trigger.Execute(this.Self);
                }
            }
            else
            {
                if (typeInput == InputEvent.OnPress)
                {
                    _ = this.m_Trigger.Execute(this.Self);
                }
            }
        }

        protected void OnCanceled(InputAction.CallbackContext context)
        {
            if(context.interaction is HoldInteraction)
            {
                if (typeInput == InputEvent.OnHoldRelease)
                {
                    _ = this.m_Trigger.Execute(this.Self);
                }
            }
            else if(context.interaction is TapInteraction)
            {
                if (typeInput == InputEvent.OnTap)
                {
                    _ = this.m_Trigger.Execute(this.Self);
                }
            }
            else if(context.interaction is PressInteraction)
            {
                if (typeInput == InputEvent.OnRelease)
                {
                    _ = this.m_Trigger.Execute(this.Self);
                }
            }
            else if(context.interaction is SlowTapInteraction)
            {
                if (typeInput == InputEvent.OnSlowTap)
                {
                    _ = this.m_Trigger.Execute(this.Self);
                }
            }
            else if(context.interaction is MultiTapInteraction)
            {
                if (typeInput == InputEvent.OnMultiTap)
                {
                    _ = this.m_Trigger.Execute(this.Self);
                }
            }
            else
            {
                if (typeInput == InputEvent.OnRelease)
                {
                    _ = this.m_Trigger.Execute(this.Self);
                }
            }
        }
        
    }
}