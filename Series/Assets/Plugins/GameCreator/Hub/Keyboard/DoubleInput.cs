using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameCreator.Runtime.Common
{
    [Title("Keyboard Double Press")]
    [Category("Keyboard/Keyboard Double Press")]

    [Description("When a keyboard key is pressed twice")]
    [Image(typeof(IconKey), ColorTheme.Type.Yellow, typeof(OverlayArrowDown))]

    [Keywords("Key", "Button", "Down", "Twice")]

    [Serializable]
    public class InputButtonKeyboardDoublePress : TInputButton
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private Key m_Key = Key.Space;
        // float touchDuration;
        // int touchCount = 0;
        float _doubleTapTimeD;


        // INITIALIZERS: --------------------------------------------------------------------------

        public static InputPropertyButton Create(Key key = Key.Space)
        {
            return new InputPropertyButton(
                new InputButtonKeyboardDoublePress
                {
                    m_Key = key
                }
            );
        }

        // UPDATE METHODS: ------------------------------------------------------------------------

        // IEnumerator singleOrDouble()
        // {
        //     yield return new WaitForSeconds(0.3f);
        //     if (touchCount == 1)
        //         Debug.Log("Single");
        //     else if (touchCount == 2)
        //     {
        //         //this coroutine has been called twice. We should stop the next one here otherwise we get two double tap
        //         new MonoBehaviour.StopCorouting("singleOrDouble");
        //         this.ExecuteEventStart();
        //         this.ExecuteEventPerform();
        //         Debug.Log("Double");
        //     }
        // }

        public override void OnUpdate()
        {
            if (Keyboard.current == null) return;
            if (!Keyboard.current[this.m_Key].wasPressedThisFrame) return;

            // if (touchCount < 3)
            // { //if there is any touch
            //     touchCount++;
            //     touchDuration += Time.deltaTime;
            //     //touch = Keyboard.current[this.m_Key].wasPressedThisFrame;

            //     if (touchDuration < 0.2f) //making sure it only check the touch once && it was a short touch/tap and not a dragging.
            //         StartCoroutine("singleOrDouble");
            // }
            // else {
            //     touchDuration = 0.0f;
            //     touchCount = 1;
            // }

            bool doubleTapD = false;

            #region doubleTapD

            // if (Input.GetKeyDown(KeyCode.D))
            // {
            if (Time.time < _doubleTapTimeD + .3f)
            {
                doubleTapD = true;
            }
            _doubleTapTimeD = Time.time;
            //}

            #endregion

            if (doubleTapD)
            {
                this.ExecuteEventStart();
                this.ExecuteEventPerform();
            }
            
        }

        

        // Update is called once per frame
        // void Update()
        // {
        //     bool doubleTapD = false;

        //     #region doubleTapD

        //     if (Input.GetKeyDown(KeyCode.D))
        //     {
        //         if (Time.time < _doubleTapTimeD + .3f)
        //         {
        //             doubleTapD = true;
        //         }
        //         _doubleTapTimeD = Time.time;
        //     }

        //     #endregion

        //     if (doubleTapD)
        //     {
        //         Debug.Log("DoubleTapD");
        //     }
        // }

    }
}