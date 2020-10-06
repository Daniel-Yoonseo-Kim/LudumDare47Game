using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
	public Transform player;
	public bool isFlipped;
	public Rigidbody2D rb;
	public Header.Bosses type;

	public int health;
	public int damage;
	private float timeBtwDamage;

	public Animator camAnim;
	private Animator anim;
	public bool isDead;
	public bool warning;
	public bool lunging;
	public bool right;

	//if its touching the floor or walls
	public bool floor;
	public bool wall;
	public bool bottom;

	private float invulnerabilityTimer;
	private float invulnerabilityLength = 1f;
	private bool deathAnim;
	public bool playerDead;


	// Start is called before the first frame update
	void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), true);
		isFlipped = false;
		if (type == Header.Bosses.DOG)
			isFlipped = true;
		floor = false;
		wall = false;
		warning = false;
		lunging = false;
		right = true;
		bottom = false;
		invulnerabilityTimer = Time.time;
		deathAnim = false;
		playerDead = false;
		GameController.instance.currentBoss = type;
		
	}

	// Update is called once per frame
	void Update()
    {
		if (player.GetComponent<PlayerStatus>().health <= 0)
		{
			gameObject.GetComponent<Animator>().SetTrigger("PlayerDeath");
			playerDead = true;
		}
		else
		{
			Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), player.GetComponent<Collider2D>(), true);
			if (!gameObject.GetComponent<EnemyStats>().isAlive && !deathAnim)
			{
				//Debug.Log("kmk");
				gameObject.GetComponent<Animator>().SetBool("Die", true);
				GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<BossKillButton>().Appear();
				deathAnim = true;
			}
			if (player.GetComponent<PlayerStatus>().invulnerable)
			{

				if (Time.time - invulnerabilityTimer > 1.5)
				{
					player.GetComponent<PlayerStatus>().invulnerable = false;
					Debug.Log("q");
				}
				else if (Time.time - invulnerabilityTimer > .3f)
					player.GetComponent<PlayerMovement>().Unlock(player.gameObject);
			}
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


	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player" && warning == false)
		{
			//deal with player invulerability
			if (Time.time - invulnerabilityTimer > .8)
			{
				player.GetComponent<PlayerStatus>().TakeDamage(damage);
				invulnerabilityTimer = Time.time;
			}
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player" && warning == false)
		{
			player = collision.transform;
			if (!player.GetComponent<PlayerStatus>().invulnerable)
			{
				player.GetComponent<PlayerStatus>().TakeDamage(damage);
				invulnerabilityTimer = Time.time;
			}
		}
		else if(collision.gameObject.tag == "Floor")
		{
			floor = true;
		}
		else if(collision.gameObject.tag == "Wall")
		{
			wall = true;
		}
		else if (collision.gameObject.tag == "Bottom")
		{
			bottom = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Floor")
		{
			floor = false;
		}
		else if (other.gameObject.tag == "Wall")
		{
			wall = false;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Floor")
		{
			floor = false;
		}
		else if (collision.gameObject.tag == "Wall")
		{
			wall = false;
		}
	}


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Floor")
		{
			floor = true;
		}
		else if (collision.gameObject.tag == "Wall")
		{
			wall = true;
		}
	}

	/*public void Damage(int damage)
	{
		if (Time.time - timeBtwDamage >= .25f)
		{
			health -= damage;
			timeBtwDamage = Time.time;
		}

	}*/
}
