using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkWarn2 : StateMachineBehaviour
{

    private float timer;
    private int direction;
    public Renderer rend;

    Transform player;
    Transform floor;
    Transform warning;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rend = animator.GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        floor = GameObject.FindGameObjectWithTag("Floor").transform;
        warning = GameObject.FindGameObjectWithTag("Warning").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        rb.GetComponent<Boss2>().warning = true;
        rb.position = new Vector2(player.position.x, rb.position.y - 4);
        Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), floor.GetComponent<BoxCollider2D>(), true);
        timer = Time.time;
        warning.position = new Vector2(rb.position.x, warning.position.y);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        warning.GetComponent<Renderer>().enabled = true;
        //animator.GetComponent<Boss2>().LookAtPlayer();
        if (Time.time - timer >= .75)
        {
            rend.enabled = true;
            animator.SetInteger("Attack", 0);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //warning.GetComponent<Renderer>().enabled = false;
    }

}
