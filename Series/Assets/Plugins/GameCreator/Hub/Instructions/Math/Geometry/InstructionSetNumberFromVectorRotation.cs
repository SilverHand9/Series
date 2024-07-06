using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using UnityEngine;

namespace GameCreator.Runtime.VisualScripting
{
	[Title("Set Number from Vector Rotation")]
	[Description("Sets a value equal to one of Vector Rotation")]
	[Version (0,0,6)]
	[Category("Math/Geometry/Set Number from Vector Rotation")]

    [Parameter("Set", "Where the value is set")]
    [Parameter("From", "The value that is set")]

	[Keywords("Set", "Vector", "Rotation", "Variable")]
	[Image(typeof(IconVector3), ColorTheme.Type.Green, typeof(OverlayArrowRight))]
    
    [Serializable]
	public class InstructionSetNumberFromVectorRotation : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
       [SerializeField] 
       private PropertySetNumber m_Set = SetNumberGlobalName.Create;
	    [Space(5)]
	    [SerializeField] private GameObject m_GameObject; 
	    private float m_FloatVector;
	    
	    private enum myEnum 
	    {
		    X, 
		    Y,
		    Z
		 };
	    [SerializeField] private myEnum vectorRotation ;
        
        // PROPERTIES: ----------------------------------------------------------------------------
        
	     public override string Title => $"Set {this.m_Set} = Rotation.{this.vectorRotation}";

        // RUN METHOD: ----------------------------------------------------------------------------
        
        protected override Task Run(Args args)
	    {
		    
		   switch (vectorRotation)
			    
			    { 
			    case myEnum.X:
				    
				    m_FloatVector = m_GameObject.transform.localRotation.eulerAngles.x;
					 break;
			    case myEnum.Y:
				    
				    m_FloatVector = m_GameObject.transform.localRotation.eulerAngles.y;
				    break;
			    case myEnum.Z:
				    
				    m_FloatVector = m_GameObject.transform.localRotation.eulerAngles.z;
				    break;
			    }
			    
		      double value = m_FloatVector; 
            this.m_Set.Set(value, args);

            return DefaultResult;
        }
    }
}