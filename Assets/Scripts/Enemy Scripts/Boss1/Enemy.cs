using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Transform player, throwPoint;

	public GameObject playerObject;
	
	public bool isFlipped = false;
	public Rigidbody2D rb;
	public Header.Bosses type;

	public int health;
	public int damage;
	private float timeBtwDamage;

	public Animator camAnim;
	public Animator anim;
	public bool isDead, isGrabbed;
	public GameObject wall, playerprojectile;
	public bool deathAnim;
	public bool playerDead;

	// Start is called before the first frame update
	void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
		playerObject = GameController.instance.playerObject;
		isFlipped = false;
		deathAnim = false;
		GameController.instance.currentBoss = type;
		playerDead = false;
	}

	// Update is called once per frame
	void Update()
    {
		/*if (health <= 0)
		{
			anim.SetTrigger("deathRight");
		}*/
		if (player.GetComponent<PlayerStatus>().health <= 0)
		{
			gameObject.GetComponent<Animator>().SetTrigger("PlayerDeath");
			playerDead = true;
		}
		if (!gameObject.GetComponent<EnemyStats>().isAlive && !deathAnim)
		{
			//Debug.Log("kmk");
			gameObject.GetComponent<Animator>().SetBool("deathRight", true);
			GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<BossKillButton>().Appear();
			deathAnim = true;
		}
	}

	public void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
             Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        wall = GameObject.FindWithTag("Wall");
        if (other.gameObject == wall) {
            anim.SetTrigger("idle");
        }
    }


	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}
}
