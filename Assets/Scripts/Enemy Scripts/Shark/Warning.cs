using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        Physics2D.IgnoreCollision(gameObject.GetComponent<Rigidbody2D>().GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Floor").GetComponent<BoxCollider2D>(), true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
