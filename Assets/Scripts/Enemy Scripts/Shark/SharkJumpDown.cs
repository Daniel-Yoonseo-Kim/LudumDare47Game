using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkJumpDown : StateMachineBehaviour
{
    public float speed;

    Rigidbody2D rb;
    Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb.position = new Vector2(player.position.x, rb.position.y);
        //animator.GetComponent<Boss2>().LookAtPlayer();
        Vector2 force = new Vector2(0, -20);
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*Vector2 target = new Vector2(rb.position.x, -12);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);*/
        Vector2 force = new Vector2(0, -5);
        //animator.GetComponent<Boss2>().LookAtPlayer();
        rb.AddForce(force, ForceMode2D.Force);
        if (rb.GetComponent<Boss2>().floor)
        {
            animator.SetInteger("Attack", 0);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), false);
    }
}
