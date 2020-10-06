using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogLunge : StateMachineBehaviour
{
    public float speed;
    private float timer;
    private float startY;
    private int direction;

    Transform player;
    Rigidbody2D rb;
    Vector2 force;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        startY = rb.position.y;
        Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), true);
        if (rb.position.x > player.position.x)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        rb.velocity = new Vector2(direction*speed, -14);
        force = new Vector2(direction*3, 0);
        rb.AddForce(force, ForceMode2D.Force);
        timer = Time.time;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Boss2>().LookAtPlayer();
        force = new Vector2(0, 16);
        rb.AddForce(force, ForceMode2D.Force);
        if ((rb.position.y >= startY && Time.time - timer > .25) || rb.GetComponent<Boss2>().wall)
            animator.SetInteger("Attack", 0);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>(), false);
        rb.velocity = new Vector2(0, -2);


    }
}
