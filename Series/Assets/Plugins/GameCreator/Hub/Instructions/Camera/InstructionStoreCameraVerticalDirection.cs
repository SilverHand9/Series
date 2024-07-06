using System;
using System.Threading.Tasks;
using UnityEngine;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.Cameras;

[Version(1, 0, 1)]

[Title("Store Camera Vertical Direction")]
[Description("Stores the camera vertical direction in a Vector3 variable")]

[Category("Camera/Store Camera Vertical Direction")]

[Parameter("Camera", "The target camera")]

[Keywords("Store", "Camera", "VerticalDirection", "Vector3")]
[Image(typeof(IconVector3), ColorTheme.Type.Blue)]

[Serializable]
public class InstructionStoreCameraVerticalDirection : Instruction
{
	// MEMBERS: -------------------------------------------------------------------------------

	[SerializeField] private PropertyGetGameObject m_Camera = GetGameObjectCameraMain.Create;
	[SerializeField] private PropertySetNumber m_VerticalDirection = SetNumberGlobalName.Create;

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

		float verticalDirection = camera.transform.forward.y;

		this.m_VerticalDirection.Set(verticalDirection, args);

		return DefaultResult;
	}
}
