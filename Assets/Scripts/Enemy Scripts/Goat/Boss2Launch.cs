using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Launch : StateMachineBehaviour
{
    public float launchSpeed;

    Transform player;
    Rigidbody2D rb;

    private Vector2 target;
    private float startT;
    private float xDir;
    private float yDir;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        startT = Time.time;
        target = new Vector2(100, 100);
        Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), true);
        rb.GetComponent<Boss2>().damage = 25;
        /*if (rb.position.x > player.position.x)
        {
            xDir = -2;
        }
        else
        {
            xDir = ;
        }
        if (rb.position.y > player.position.y)
        {
            yDir = 30;
        }
        else
        {
            yDir = -30;
        }*/
        target = new Vector2(player.position.x - rb.position.x, player.position.y - rb.position.y).normalized;
        rb.gravityScale = 0;
        rb.GetComponent<Boss2>().lunging = true;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*if (rb.GetComponent<Boss2>().wall)
            Debug.Log("aa");*/
        if (Time.time - startT < 1.2 && !rb.GetComponent<Boss2>().wall)
        {
            if (target.x == 100)
            {
                target.x = player.position.x * 3;
                target.y = player.position.y * 3;
            }
            //Vector2 target = new Vector2(player.position.x, player.position.y);
            //Vector2 newPos = Vector2.MoveTowards(rb.position, target, launchSpeed * Time.fixedDeltaTime);
            //rb.MovePosition(newPos);
            rb.position += target * launchSpeed * Time.deltaTime;
        }
        else //if (Time.time - startT >= 1.5)
            animator.SetInteger("Attack", 0);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), false);
        rb.GetComponent<Boss2>().damage = 10;
        rb.gravityScale = 1;
        rb.GetComponent<Boss2>().lunging = false;
    }
}
