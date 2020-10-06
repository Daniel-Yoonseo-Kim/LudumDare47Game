using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Jump : StateMachineBehaviour
{
    public float jumpSpeed;

    Transform player;
    Rigidbody2D rb;

    //Vector2 startPos;
    //float fallX;
    float startT;
    float direction;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        startT = Time.time;
        Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), true);
        //startPos = rb.position;
        //fallX = -100f;
        /*if (rb.position.x > rb.position.x)
        {
            direction = 2;
        }
        else
        {
            direction = -2;
        }*/
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (Time.time - startT < 3)
        {
            animator.GetComponent<Boss2>().LookAtPlayer();
            Vector2 target = new Vector2(0, 6);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, jumpSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        else
        {
            animator.SetInteger("Attack", 0);
            /*
            if (fallX == -100)
                fallX = player.position.x;
            Vector2 target = new Vector2(fallX, startPos.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, jumpSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            if (rb.position.y <= startPos.y)
            {
                animator.SetInteger("Attack", 0);
            }*/
        }
            
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
