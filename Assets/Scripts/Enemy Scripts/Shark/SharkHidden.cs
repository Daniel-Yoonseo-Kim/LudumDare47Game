using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkHidden : StateMachineBehaviour
{
    public Renderer rend;
    private float waitTime;
    private float startTime;
    Rigidbody2D rb;
    Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        rend = animator.GetComponent<Renderer>();
        rend.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        waitTime = Random.value + Random.Range(1, 4);
        Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), true);
        startTime = Time.time;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Time.time - startTime > waitTime)
        {
            animator.SetInteger("Attack", Random.Range(1, 3));
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
