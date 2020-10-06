using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerprojectile : MonoBehaviour
{
    public float throwSpeed = 10f, speed;
    private Rigidbody2D rb;
    private GameObject bottom;
    public Transform player, enemy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GameObject.FindGameObjectWithTag("Boss").transform;
        Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Boss").GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(rb.GetComponent<BoxCollider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>(), true);
            if (enemy.position.x > player.position.x) {
                speed = throwSpeed * transform.localScale.x;
            } else {
                speed = -(throwSpeed * transform.localScale.x);
            }
    }

    // Update is called once per frame
    private void Update()
    {
            rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D other) {
        bottom = GameObject.FindWithTag("Bottom");
        if (other.gameObject == bottom) {
            Destroy(gameObject);
            player.position = new Vector2(rb.position.x, rb.position.y);
            player.GetComponent<Renderer>().enabled = true;
        }

    }
}

