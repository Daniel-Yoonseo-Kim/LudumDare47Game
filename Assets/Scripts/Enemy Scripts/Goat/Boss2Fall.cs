using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Fall : StateMachineBehaviour
{
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        if (rb.GetComponent<Boss2>().floor)
        {
            animator.SetTrigger("Ground");
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (rb.GetComponent<Boss2>().floor)
        {
            animator.SetTrigger("Ground");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Ground");
    }

}
