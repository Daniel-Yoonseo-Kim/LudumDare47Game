using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMove : StateMachineBehaviour
{
    public float speed;
    public float biteRange;

    Transform player;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), true);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Boss2>().LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rb.position.y); //could make it able to chase in air
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        //animator.SetInteger("Attack", 1); //for testing
        if (Vector2.Distance(rb.position, player.position) < biteRange && Random.Range(0, 100) == 0)
            animator.SetInteger("Attack", 1);
        if(Random.Range(0, 400) == 0)
        {
            animator.SetInteger("Attack", 2);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
