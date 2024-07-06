using System;
using System.Threading.Tasks;
using UnityEngine;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.Cameras;

[Version(1, 0, 1)]

[Title("Store Camera Direction")]
[Description("Stores the camera direction in a Vector3 variable")]

[Category("Camera/Store Camera Direction")]

[Parameter("Camera", "The target camera")]

[Keywords("Store", "Camera", "Direction", "Vector3")]
[Image(typeof(IconVector3), ColorTheme.Type.Blue)]

[Serializable]
public class InstructionStoreCameraDirection : Instruction
{
	// MEMBERS: -------------------------------------------------------------------------------

	[SerializeField] private PropertyGetGameObject m_Camera = GetGameObjectCameraMain.Create;
	[SerializeField] private PropertySetVector3 m_Direction = SetVector3GlobalName.Create;

	// PROPERTIES: ----------------------------------------------------------------------------

	public override string Title => string.Format("Store Camera Direction");

	// RUN METHOD: ----------------------------------------------------------------------------

	protected override Task Run(Args args)
	{

		TCamera cameraType = this.m_Camera.Get<TCamera>(args);
		Camera camera = cameraType.Get<Camera>();

		if (camera == null)
		{
			Debug.Log("Instruction Store Camera Direction missing camera");
		}

		Vector3 direction = camera.transform.forward;

		this.m_Direction.Set(direction, args);

		return DefaultResult;
	}
}
