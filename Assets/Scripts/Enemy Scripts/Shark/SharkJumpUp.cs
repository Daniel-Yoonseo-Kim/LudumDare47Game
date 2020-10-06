using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkJumpUp : StateMachineBehaviour
{
    private float timer;
    public float speed;

    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        timer = Time.time;
        rb.GetComponent<BoxCollider2D>().enabled = true;
        //animator.GetComponent<Boss2>().LookAtPlayer();
        rb.GetComponent<Boss2>().warning = false;
        GameObject.FindGameObjectWithTag("Warning").GetComponent<Renderer>().enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        Vector2 target = new Vector2(rb.position.x, 15);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        if (Time.time - timer >= 1.25)
        {
            animator.SetInteger("Attack", 1);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
