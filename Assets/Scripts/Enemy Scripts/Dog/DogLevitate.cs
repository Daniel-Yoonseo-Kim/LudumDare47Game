using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogLevitate : StateMachineBehaviour
{
    private float timer;
    public float speed;

    Rigidbody2D rb;
    Vector2 target;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        timer = Time.time;
        target = new Vector2(rb.position.x, rb.position.y + 4.5f);
        animator.GetComponent<Boss2>().LookAtPlayer();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        if (Time.time - timer >= 1.75)
        {
            int rand = Random.Range(1, 3);
            if (rand == 2)
                rand++;
            animator.SetInteger("Attack", rand);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
