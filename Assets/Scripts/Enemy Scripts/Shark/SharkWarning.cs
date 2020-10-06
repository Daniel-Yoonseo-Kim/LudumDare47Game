using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkWarning : StateMachineBehaviour
{

    public Renderer rend;

    private float timer;
    private int direction;

    Transform player;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        rend = animator.GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject.FindGameObjectWithTag("Warning").GetComponent<Renderer>().enabled = true;
        rb = animator.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Floor").transform.GetComponent<BoxCollider2D>(), false);
        rb.GetComponent<Boss2>().warning = true;
        rb.position = new Vector2(player.position.x, rb.position.y - 4);
        rend.enabled = true;
        timer = Time.time;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Boss2>().LookAtPlayer();
        if (Time.time - timer >= .75)
        {
            animator.SetInteger("Attack", 0);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.FindGameObjectWithTag("Warning").GetComponent<Renderer>().enabled = false;
    }

}
