using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Jump2 : StateMachineBehaviour
{
    public bool isGrounded;
    public int speed;
    private float fallX;
    private float start;

    Vector2 target;
    Rigidbody2D rb;
    Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isGrounded = false;
        fallX = -100;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), true);
        rb.GetComponent<Boss2>().damage = 25;
        start = Time.time;

        target = new Vector2(player.position.x - rb.position.x, player.position.y - rb.position.y).normalized;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*if (fallX == -100)
            fallX = player.position.x;
        Vector2 target = new Vector2(fallX, -8);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);*/
        if (Time.time - start < 1.2 && !(rb.GetComponent<Boss2>().wall || rb.GetComponent<Boss2>().floor))
        {
            if (target.x == 100)
            {
                target.x = player.position.x * 3;
                target.y = player.position.y * 3;
            }
            rb.position += target * speed * Time.deltaTime;
        }
        if (rb.GetComponent<Boss2>().floor)
            animator.SetTrigger("Ground");
        else if (rb.GetComponent<Boss2>().wall)
            animator.SetTrigger("Wall");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Ground");
        //Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), false);
        rb.GetComponent<Boss2>().damage = 10;
    }

}
