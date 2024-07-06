using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Version(1, 0, 0)]

[Title("If Else Container")]
[Description("Runs either of two Instruction Lists based on Conditions. Can also be used" +
    " to check conditions within the same set of instructions")]

[Category("Logic/If Else Container")]

[Parameter(
        "Title",
        "The title of the instruction. For readability"
    )]
[Parameter(
        "Conditions",
        "The conditions to check for the if/else statement"
    )]
[Parameter(
        "On True",
        "The instructions to run if the conditions are True"
    )]
[Parameter(
        "On False",
        "The instructions to run if the conditions are False"
    )]
[Parameter(
        "Wait To Finish",
        "If checked, Will wait to finish these instructions before continuing"
    )]

[Keywords("From HLS", "Container", "Contain", "Utility", "Collapse", 
    "Actions", "Con", "Conditions", "If", "Else", "IfElse", "Check")]
[Image(typeof(IconConditions), ColorTheme.Type.Green, typeof(OverlayBolt))]

[Serializable]
public class InstructionIfElseContainer : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------

    [SerializeField]
    private string m_Title;
    [SerializeField] private ConditionList m_Conditions = new ConditionList();
    [SerializeField] private Nothing IfTrue;
    [SerializeField] protected InstructionList m_OnTrue = new InstructionList();
    [SerializeField] private Nothing IfFalse;
    [SerializeField] protected InstructionList m_OnFalse = new InstructionList();
    [SerializeField] private bool m_WaitToFinish = true;

    public enum Nothing { }

    // PROPERTIES: ----------------------------------------------------------------------------

    public override string Title =>
        String.IsNullOrEmpty(this.m_Title) ? "If / Else" : this.m_Title;

    // RUN METHOD: ----------------------------------------------------------------------------

    protected override async Task Run(Args args)
    {
        InstructionList toRun = this.m_Conditions.Check(args, CheckMode.And) ? m_OnTrue : m_OnFalse;

        if (this.m_WaitToFinish)
            await toRun.Run(args);
        else
            _ = toRun.Run(args);
    }
}