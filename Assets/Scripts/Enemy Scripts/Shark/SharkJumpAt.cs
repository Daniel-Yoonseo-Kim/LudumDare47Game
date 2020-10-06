using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkJumpAt : StateMachineBehaviour
{
    public Renderer rend;

    private float timer;
    private int direction;
    private bool first;
    public float speed;

    Transform player;
    Rigidbody2D rb;
    Vector2 jumpDir;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rend = animator.GetComponent<Renderer>();
        rend.enabled = true;
        first = true;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        timer = Time.time;
        Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), true);
        if (rb.position.x > player.position.x)
        {
            direction = -1;
        }
        else
        {
            direction = -1;
        }
        jumpDir = new Vector2((rb.position.x - player.position.x)*direction/*direction * 2*/, 20);
        jumpDir.Normalize();
        animator.GetComponent<Boss2>().LookAtPlayer();
        rb.AddForce(jumpDir*13, ForceMode2D.Impulse); 

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(rb.velocity.y <= 0 && first)
        {
            animator.GetComponent<Boss2>().LookAtPlayer();
            rb.velocity = new Vector2(0, 0);
            jumpDir = new Vector2(player.position.x - rb.position.x, player.position.y - rb.position.y);
            jumpDir.Normalize();
            rb.AddForce(jumpDir * 10, ForceMode2D.Impulse);
            first = false;
            /*Vector2 target = new Vector2(rb.position.x + direction, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);*/
        }
        if (rb.GetComponent<Boss2>().floor && Time.time - timer > .25)
        {
            animator.SetInteger("Attack", 0);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>(), false);
    }
}
