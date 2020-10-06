using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sample : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool invulnerable;
    private float timer;
    private bool collideOff;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        invulnerable = false;
        collideOff = false;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log(timer);
        Debug.Log("Lelouch");
        Debug.Log(Time.time - timer);
        if (Time.time - timer > .5 && collideOff == true)
        {

            Transform boss = GameObject.FindGameObjectWithTag("Boss").transform;
            Debug.Log("C2");
            Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), boss.GetComponent<BoxCollider2D>(), true);
        }*/
    }

    public void Damage(float d, Rigidbody2D boss)
    {
        //Debug.Log(d);
        //Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), boss.GetComponent<BoxCollider2D>(), true);
        //timer = Time.time;
        if (boss.position.x > rb.position.x)
        {
            rb.AddForce(new Vector2(-5f, 3f), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(5f, 3f), ForceMode2D.Impulse);
        }
    }
}
