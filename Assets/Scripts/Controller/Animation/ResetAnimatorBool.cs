using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    [Header("Disabel Root Motion")] public string disableRootMotion = "disabelRoolMotion";
    public bool disableRootMotionStatus = false;
    
    [Header("Is Preforming Action Bool")] public string isPreformingAction = "isPreformingAction";
    public bool isPreformingActionStatus = false;

    [Header("Is Preforming  Quick Turn")] public string isPreformingQiuckTurn = "isPreformingQuickTurn";
    public bool isPreformingQuickTurnStatus = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(disableRootMotion, disableRootMotionStatus);
        animator.SetBool(isPreformingAction, isPreformingActionStatus);
        animator.SetBool(isPreformingQiuckTurn, isPreformingQuickTurnStatus);
    }
}