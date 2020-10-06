using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Idle : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    private float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        if (!rb.GetComponent<Boss2>().playerDead)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

            /*if (rb.GetComponent<Boss2>().bottom)
            {
                rb.position = new Vector2(rb.position.x, -6.35f);
            }*/
            Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), true);

            //rb.Sleep();
            Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Floor").transform.GetComponent<BoxCollider2D>(), false);
            timer = Time.time;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(Vector2.Distance(rb.position, player.position));
        //Debug.Log(rb.position.y);
        //if (Vector2.Distance(rb.position, player.position) < 20)
        /*if (rb.GetComponent<Boss2>().bottom && Time.time - timer > .1)
        {
            rb.position = new Vector2(rb.position.x, -6.35f);
        }*/
        if (!rb.GetComponent<Boss2>().playerDead)
            animator.SetTrigger("Activate");

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Activate");
        //rb.WakeUp();
    }
}
