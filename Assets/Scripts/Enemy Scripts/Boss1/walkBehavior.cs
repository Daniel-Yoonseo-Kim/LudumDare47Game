using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkBehavior : StateMachineBehaviour
{
    public float speed;

    Transform player;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Enemy>().LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(rb.position, player.position) <= animator.GetComponent<Boss1_Attack>().attackRange * 2) {
            animator.SetTrigger("attack");
        }
        else if (Vector2.Distance(rb.position, player.position) > animator.GetComponent<Boss1_Attack>().attackRange * 10) {
            animator.SetTrigger("charge");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("charge");
    }
}