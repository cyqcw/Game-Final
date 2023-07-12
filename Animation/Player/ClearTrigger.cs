using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTrigger : StateMachineBehaviour{
    public string[] ckearAtEnter;
    public string[] ckearAtExit;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        foreach (var signal in ckearAtEnter){
            animator.ResetTrigger(signal);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        foreach (var signal in ckearAtExit){
            animator.ResetTrigger(signal);
        }
    }
}






// public class FSMCleaSignals : StateMachineBehaviour
// {
//     public string[] ckearAtEnter;
//     public string[] ckearAtExit;
//     // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
//     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     {
//         foreach (var signal in ckearAtEnter)
//         {
//             animator.ResetTrigger(signal);
//         }
//     }

//     // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
//     override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     {
//         foreach (var signal in ckearAtExit)
//         {
//             animator.ResetTrigger(signal);
//         }
//     }
// }
