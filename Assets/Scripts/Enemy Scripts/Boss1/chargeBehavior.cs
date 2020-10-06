using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargeBehavior : StateMachineBehaviour
{
    Transform player, leftWall, rightWall;
    Rigidbody2D rb;
    Vector2 target;
    Enemy enemy;
    GameObject wall;

    public float speed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player    = GameObject.FindGameObjectWithTag("Player").transform;
        rb        = animator.GetComponent<Rigidbody2D>();
        enemy     = animator.GetComponent<Enemy>();
        leftWall  = GameObject.FindWithTag("leftWall").transform;
        rightWall = GameObject.FindWithTag("rightWall").transform;
        enemy.LookAtPlayer();  
        if (player.position.x > rb.position.x) {
            target = new Vector2(rightWall.position.x, rb.position.y);
        } else {
            target = new Vector2(leftWall.position.x, rb.position.y);
        }

        Physics2D.IgnoreCollision(GameController.instance.playerObject.GetComponent<Collider2D>(), animator.GetComponent<Collider2D>(), true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((animator.gameObject.transform.position.x > player.position.x && enemy.isFlipped)
            || (animator.gameObject.transform.position.x < player.position.x && !enemy.isFlipped)) {
            animator.SetTrigger("idle");
            // enemy.LookAtPlayer();
            return;
        }


        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newPos);

        void OnCollisionEnter2D(Collision2D other) {
            Debug.Log("Entered OnCollision");
            wall = GameObject.FindWithTag("Wall");
            if (other.gameObject == wall) {
                animator.SetTrigger("idle");
            }
        }

        if (Vector2.Distance(rb.position, player.position) <= animator.GetComponent<Boss1_Grab>().attackRange) {
            animator.SetTrigger("grab");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        
        Physics2D.IgnoreCollision(GameController.instance.playerObject.GetComponent<Collider2D>(), animator.GetComponent<Collider2D>(), false);
    }
}
