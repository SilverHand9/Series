using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;
using GameCreator.Runtime.VisualScripting;

[Version(1, 0, 0)]

[Title("Store GameObject Rotation")]
[Description("Stores a GameObject's Rotation")]

[Category("Game Objects/Store GameObject Rotation")]

[Parameter("Game Object", "The target game object")]
[Parameter("Target", "The target location to store the rotation")]


[Keywords("Game Object", "Rotation", "Store")]
[Image(typeof(IconVector3), ColorTheme.Type.Green)]

[Serializable]
public class InstructionStoreGameObjectRotation : Instruction
{
	// MEMBERS: -------------------------------------------------------------------------------

	[SerializeField] private PropertyGetGameObject m_GameObject = new PropertyGetGameObject();
    [SerializeField] private PropertySetVector3 m_Target;


	// PROPERTIES: ----------------------------------------------------------------------------

	public override string Title => string.Format(
		"Store Rotation from {0} to {1} ", //{2}
		this.m_GameObject, 
		this.m_Target
	);

	// RUN METHOD: ----------------------------------------------------------------------------

	protected override Task Run(Args args)
	{

		GameObject gameObject = this.m_GameObject.Get(args);
		if (gameObject == null) return DefaultResult;

		Vector3 rot = gameObject.transform.eulerAngles;
		this.m_Target.Set(rot, args);

// Debug.Log("Rotation "+rot);

		return DefaultResult;
	}
}
