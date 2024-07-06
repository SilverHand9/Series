using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Runtime.Characters;
using UnityEngine;
using GameCreator.Runtime.Variables;

[Version(1, 0, 1)]

[Title("Can Monkey A Hit B")]
[Description("Checks if Monkey A Can hit Monkey B")]


[Image(typeof(IconCompass), ColorTheme.Type.Yellow)]

[Category("Characters/Can Hit Monkey")]
[Keywords("Character", "Vector")]

[Serializable]
public class CharacterAFacingCharacterB : Condition
{
    [SerializeField]
    private GlobalNameVariables settingsVariables = null;

    // [SerializeField]
    // private GlobalNameVariables playerVariables = null;

    [SerializeField] private PropertyGetGameObject m_CharacterA = GetGameObjectPlayer.Create();
    [SerializeField] private PropertyGetGameObject m_CharacterB = GetGameObjectPlayer.Create();

    protected override bool Run(Args args)
    {
        bool isPVP = (bool)(GlobalNameVariablesManager.Instance.Get(settingsVariables, "isPVP") ?? false);

        if(isPVP == false)
        {
            return true;
        }

        Character characterA = this.m_CharacterA.Get<Character>(args);
        if (characterA == null) {
            Debug.LogError("Character A == null");
            return false;
        }

        Character characterB = this.m_CharacterB.Get<Character>(args);
        if (characterB == null) {
            Debug.LogError("Character B == null");
            return false;
        }

        //Debug.Log("A position: " + characterA.gameObject.transform.position.x + " and B position: " + characterB.gameObject.transform.position.x);


        if (characterA.gameObject.transform.position.x < characterB.gameObject.transform.position.x)
        {
            //Debug.Log("1. A rotation:" + characterA.gameObject.transform.localRotation.eulerAngles.y);
            return characterA.gameObject.transform.localRotation.eulerAngles.y < 100;
        }
        else if (characterA.gameObject.transform.position.x >= characterB.gameObject.transform.position.x)
        {
            //Debug.Log("2. A rotation:" + characterA.gameObject.transform.localRotation.eulerAngles.y);
            return characterA.gameObject.transform.localRotation.eulerAngles.y > 230;
        }
        else
        {
            //Debug.Log("Impossible");
            return false;
        }
    }
}
