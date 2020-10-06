using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMove : StateMachineBehaviour
{
    public float speed;
    private float timer;
    private float moveTime;
    private int direction;

    Transform player;
    Rigidbody2D rb;
    Vector3 flipped;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Physics2D.IgnoreLayerCollision(8, 11, true);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        if (rb.GetComponent<Boss2>().bottom)
        {
            rb.position = new Vector2(rb.position.x, -6.35f);
            rb.GetComponent<Boss2>().bottom = false;
        }
        timer = Time.time;
        moveTime = Random.Range(0, 3) + Random.value;
        Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), true);

        direction = -2;//Random.Range(0, 1) == 0 ? -2 : 2;
        //Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), false);
        flipped = animator.transform.localScale;
        flipped.z *= -1f;
        if (animator.GetComponent<Boss2>().right)
        {
            animator.transform.localScale = flipped;
            animator.transform.Rotate(0f, 180f, 0f);
            animator.GetComponent<Boss2>().right = !animator.GetComponent<Boss2>().right;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (Time.time - timer < moveTime)
        {
            Vector2 target = new Vector2(rb.position.x + direction, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            if ( Random.Range(0, 400) == 0)
                animator.SetTrigger("Hide");

        }
        else if (rb.GetComponent<Boss2>().wall && Time.time - timer > .25)
        {
            timer = Time.time;
            moveTime = 4.5f;
            direction *= -1;
            animator.transform.localScale = flipped;
            animator.transform.Rotate(0f, 180f, 0f);
            animator.GetComponent<Boss2>().right = !animator.GetComponent<Boss2>().right;
        }
        else
        {
            timer = Time.time;
            moveTime = Random.Range(1, 4) + Random.value;
            direction *= -1;
            animator.transform.localScale = flipped;
            animator.transform.Rotate(0f, 180f, 0f);
            animator.GetComponent<Boss2>().right = !animator.GetComponent<Boss2>().right;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
