using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogTeleport : StateMachineBehaviour
{
    private float timer;
    public float speed;

    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        animator.GetComponent<Boss2>().LookAtPlayer();
        int rand = Random.Range(-1, 2);
        rb.position = new Vector2(rand * 10, -7.39f);
        animator.SetInteger("Attack", 0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
