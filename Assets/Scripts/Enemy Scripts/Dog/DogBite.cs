using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBite : StateMachineBehaviour
{
    Transform player;
    Vector2 force;
    private float direction;
    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        if (rb.position.x >= player.position.x)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        force = new Vector2(direction*7, 0);
        rb.AddForce(force, ForceMode2D.Impulse);
        animator.GetComponent<Boss2>().LookAtPlayer();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("Attack", 0);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
