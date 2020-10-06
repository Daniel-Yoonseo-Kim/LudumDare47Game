using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkJumpAlong : StateMachineBehaviour
{
    public Renderer rend;

    private bool first;
    private int direction;
    public float speed;

    Transform player;
    Rigidbody2D rb;
    Vector2 jumpDir;
    Vector3 flipped;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rend = animator.GetComponent<Renderer>();
        rend.enabled = true;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        first = true;
        Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), true);
        if (player.position.x <= 0)
        {
            direction = -5;
        }
        else
        {
            direction = 5;
        }
        rb.position = new Vector2(player.position.x - direction, rb.position.y);
        //jumpDir = new Vector2((player.position.x - rb.position.x ), rb.position.y);
        //jumpDir.Normalize();
        //animator.GetComponent<Boss2>().LookAtPlayer();
        //rb.AddForce(jumpDir * 10, ForceMode2D.Impulse);
        animator.GetComponent<Boss2>().lunging = true;
        flipped = animator.transform.localScale;
        flipped.z *= -1f;
        if (direction == -5 && animator.GetComponent<Boss2>().right)
        {
                animator.transform.localScale = flipped;
                animator.transform.Rotate(0f, 180f, 0f);
                animator.GetComponent<Boss2>().right = false;
        }
        else if(direction == 5 && !animator.GetComponent<Boss2>().right)
        {
            animator.transform.localScale = flipped;
            animator.transform.Rotate(0f, 180f, 0f);
            animator.GetComponent<Boss2>().right = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*if (first)
        {
            animator.GetComponent<Boss2>().LookAtPlayer();
            first = false;
            Debug.Log("ddddddd");
        }*/
        Vector2 target = new Vector2(rb.position.x + direction, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        /*if (rb.velocity.y <= 0)
        {
            animator.GetComponent<Boss2>().LookAtPlayer();
            rb.velocity = new Vector2(0, 0);
            jumpDir = new Vector2(player.position.x - rb.position.x, player.position.y - rb.position.y);
            jumpDir.Normalize();
            rb.AddForce(jumpDir * 10, ForceMode2D.Impulse);
            first = false;
            *//*Vector2 target = new Vector2(rb.position.x + direction, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);*//*
        }*/
        /*if (rb.GetComponent<Boss2>().floor && Time.time - timer > .25)
        {
            animator.SetInteger("Attack", 0);
        }*/

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("Attack", 0);
        animator.GetComponent<Boss2>().lunging = false;
    }
}
