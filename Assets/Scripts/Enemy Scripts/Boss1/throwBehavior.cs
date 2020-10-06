using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwBehavior : StateMachineBehaviour
{
    public Transform throwPoint;
    public float throwForceX = 5;
    public float throwForceY = 5;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //rend = player.GetComponent<Renderer>();
        //rend.enabled = false;
        throwPoint = GameObject.FindGameObjectWithTag("Boss").GetComponent<Enemy>().throwPoint;

        // disable the player object
        GameController.instance.playerObject.GetComponent<Animator>().SetTrigger("OAA");
        GameController.instance.playerObject.SetActive(false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //rend.enabled = true;
        // enable the player obejct, put it to the right position and apply force
        GameController.instance.playerObject.SetActive(true);
        GameController.instance.playerObject.transform.position = throwPoint.position;
        int direction = (throwPoint.position.x - GameObject.FindGameObjectWithTag("Boss").transform.position.x > 0) ? 1 : -1;
        GameController.instance.playerObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * throwForceX, throwForceY), ForceMode2D.Impulse);

        GameController.instance.playerObject.GetComponent<PlayerStatus>().thrownLock = true;
    }
}
