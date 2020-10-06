using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogLunge2 : StateMachineBehaviour
{
    public float speed;
    private float timer;
    private float startY;
    private int direction;

    Transform player;
    Rigidbody2D rb;
    Vector2 target;

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
        timer = Time.time;
        target.x = player.position.x - rb.position.x;
        target.y = player.position.y - rb.position.y;
        rb.gravityScale = 0;
        animator.GetComponent<Boss2>().LookAtPlayer();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        rb.position += target * speed * Time.deltaTime;
        if (rb.GetComponent<Boss2>().floor)
            Debug.Log("floor");
        if ( Time.time - timer > 2 || rb.GetComponent<Boss2>().wall || rb.GetComponent<Boss2>().floor)
            animator.SetInteger("Attack", 0);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), false);
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 1;

    }
}
