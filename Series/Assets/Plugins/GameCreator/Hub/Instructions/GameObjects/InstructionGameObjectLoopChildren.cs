using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.VisualScripting
{
    [Version(1, 0, 2)]

    [Title("Loop Children")]
    [Description("Loops a Game Object Children and executes an Actions component for each value")]

    [Image(typeof(IconCubeSolid), ColorTheme.Type.Blue, typeof(OverlayBolt))]

    [Category("Game Objects/Loop Children")]

    [Parameter("Game Object", "Game Object which children are iterated")]
    [Parameter(
        "Actions",
        "The Actions component executed for each child of the Game Object. The Target argument " +
        "of any Instruction contains the object inspected"
    )]

    [Keywords(
        "Game Object", "Child", "Actions", "Run", "Iterate", "Cycle", "Every", "All", "Stack"
    )]
    
    [Serializable]
    public class InstructionGameObjectLoopChildren : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        protected PropertyGetGameObject m_GameObject = new PropertyGetGameObject();

        [SerializeField]
        private PropertyGetGameObject m_Actions = GetGameObjectActions.Create();

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Loop {this.m_GameObject} Children";

        // RUN METHOD: ----------------------------------------------------------------------------

        protected override async Task Run(Args args)
        {
            Transform parent = this.m_GameObject.Get<Transform>(args);
            if (parent != null)
            {
                Actions actions = this.m_Actions.Get<Actions>(args);
                if (actions == null) return;

                Args actionsArgs = new Args(args.Self, null);
                for (int i = 0; i < parent.childCount; ++i)
                {
                    GameObject child = parent.GetChild(i).gameObject;
                    if (child == null) continue;

                    actionsArgs.ChangeTarget(child);
                    await actions.Run(actionsArgs);
                }
            }
        }

    }
}