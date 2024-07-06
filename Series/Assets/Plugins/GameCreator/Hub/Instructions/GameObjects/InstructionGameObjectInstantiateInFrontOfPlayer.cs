using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Version(1, 0, 0)]
[Title("Instantiate In Front of Player")]
[Description("Creates a new instance of a referenced game object and spawns it in front of player")]

[Category("Game Objects/Instantiate In Front of Player")]

[Parameter("Game Object", "Game Object reference that is instantiated")]
[Parameter("Direction","The direction player is facing")]
[Parameter("Distance From Player","The distance in front of the player you want to instantiate at")]
[Parameter("Height Offset","The height you want to instantiate the object at")]
[Parameter("Position", "The position of the new game object instance")]
[Parameter("Rotation", "The rotation of the new game object instance")]
[Parameter("Save", "Optional value where the newly instantiated game object is stored")]

[Image(typeof(IconCubeSolid), ColorTheme.Type.Blue, typeof(OverlayPlus))]

[Keywords("Create","New","Game Object","Instantiate")]

[Serializable]
public class InstructionGameObjectInstantiateInFrontOfPlayer : Instruction
{
    public override string Title => "Instantiate in Front of Player";
    [SerializeField] private PropertyGetInstantiate m_GameObject = new();
    [SerializeField] private PropertyGetDirection m_Direction = GetDirectionCharactersFacing.Create;
    [SerializeField] private PropertyGetDecimal m_DistanceFromPlayer = GetDecimalDecimal.Create(1f);
    [SerializeField] private PropertyGetDecimal m_HeightOffset = GetDecimalDecimal.Create(1f);
    [SerializeField] private PropertyGetPosition m_Position = GetPositionCharactersPlayer.Create;
    [SerializeField] private PropertyGetRotation m_Rotation = GetRotationCharactersPlayer.Create;   
    [SerializeField] private PropertyGetGameObject m_Parent = GetGameObjectNone.Create();
    [SerializeField] private PropertySetGameObject m_Save = SetGameObjectNone.Create;

    protected override Task Run(Args args)
    {

        Vector3 facing = this.m_Direction.Get(args);
        Vector3 position = this.m_Position.Get(args);
        Quaternion rotation = this.m_Rotation.Get(args);
        float distFromPlayer = (float)this.m_DistanceFromPlayer.Get(args);
        float heightOffset = (float)this.m_HeightOffset.Get(args);

        Vector3 inFrontPos = InFrontOfPlayerPosition(position, facing, distFromPlayer, heightOffset);

        GameObject instance = this.m_GameObject.Get(args, inFrontPos, rotation);
        if (instance != null)
        {
            Transform parent = this.m_Parent.Get<Transform>(args);
            if (parent != null) instance.transform.SetParent(parent);
            this.m_Save.Set(instance, args);
        }
        return DefaultResult;
    }
    
    /// <summary>
    /// Returns a location in front of facing direction and distance from player
    /// </summary>
    /// <param name="playerPosition">Players current location</param>
    /// <param name="playerFacing">Players forward vector or facing direction</param>
    /// <param name="distFromPlayer">The distance from player</param>
    /// <param name="heightOffset">The height offset from players feet, ex: 0=feet, playerHeight/2 = waist, playerHeight=head</param>
    /// <returns>The location in front of players facing direction</returns>
    private Vector3 InFrontOfPlayerPosition(Vector3 playerPosition, Vector3 playerFacing, float distFromPlayer, float heightOffset)
    {
        return playerPosition + (playerFacing * distFromPlayer) + new Vector3(0, heightOffset, 0);
    }
}
