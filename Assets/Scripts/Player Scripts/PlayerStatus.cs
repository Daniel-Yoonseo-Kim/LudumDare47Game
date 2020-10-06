using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // parameters ////////////////////////////////////////////////////
    public float health = 100f;
    public float shockDamage = 10;
    public GameObject deadPlayer = null;

    // effects ///////////////////////////////////////////////////////
    public List<GameObject> effectsNextFrame = new List<GameObject>(); // will execute in lateUpdate
    public GameObject effectTakingOver = null; // will execute after effectsNextFrame, and is a singleton
    public List<GameObject> effectsBeforeHurt = new List<GameObject>();
    public List<GameObject> effectsAfterHurt = new List<GameObject>();

    // temperory numbers for each damage /////////////////////////////
    public float upcomingDamage = 0;

    // added to allow knockback to work //////////////////////////////
    public bool invulnerable = false;
    private float invulnerabilityTimer;
    private float invulnerabilityLength = 1f;

    // total lock ///////////////////////////////////////////////////
    public bool thrownLock = false;
    private int thrownTimer = 0;


    // utils /////////////////////////////////////////////////////////
    IEnumerator ShockTimer() {
        while (!gameObject.GetComponent<PlayerMovement>().Touches()) {
            yield return null;
        }

        gameObject.GetComponent<PlayerMovement>().Unlock(gameObject);
        gameObject.GetComponent<PlayerAttack>().Unlock(gameObject);
        gameObject.GetComponent<PlayerSkills>().Unlock(gameObject);

        if (thrownLock) {
            gameObject.GetComponent<Animator>().SetBool("Flying", false);
            thrownLock = false;
            TakeDamage(shockDamage);
        }
    }

    IEnumerator GPTimer(int frames, string coroutine) {
        int i = frames;
        while (i>0) {
            i--;
            yield return null;
        }

        StartCoroutine(coroutine);
    }


    // interfaces ////////////////////////////////////////////////////
    public void TakeDamage(float rawDamage) {
        if (gameObject.GetComponent<PlayerMovement>().isDashing)
            return;
        upcomingDamage = rawDamage;

        foreach (GameObject effect in effectsBeforeHurt) {
            if (effect)
                effect.GetComponent<SkillTemplate>().BeforeHurt();
        }

        Rigidbody2D boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Rigidbody2D>();
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (upcomingDamage > 0) {
            gameObject.GetComponent<PlayerMovement>().Lock(gameObject);
            gameObject.GetComponent<PlayerMovement>().Lock(gameObject);
            gameObject.GetComponent<PlayerMovement>().Lock(gameObject);
            if (boss.position.x > rb.position.x) {
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(-4f, 3f), ForceMode2D.Impulse);
            }
            else {
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(4f, 3f), ForceMode2D.Impulse);
            }
            StartCoroutine(GPTimer(2, "ShockTimer"));
        }

        health -= upcomingDamage;
        if (health <= 0) {
            health = 0;
            Die();
        }

        foreach (GameObject effect in effectsAfterHurt) {
            if (effect)
                effect.GetComponent<SkillTemplate>().AfterHurt();
        }

    }

    public void GetHeal(float rawHealing) {
        // add effects here if needed
        health += rawHealing;
    }

    public void Die() {
        Debug.Log("player dies");
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerAttack>().enabled = false;
        gameObject.GetComponent<PlayerSkills>().enabled = false;

        GameObject death = Instantiate(deadPlayer);
        death.transform.position = transform.position;
        death.transform.localScale = transform.localScale;
        death.GetComponent<Animator>().SetBool("RightFacing", gameObject.GetComponent<PlayerMovement>().rightFacing);
        gameObject.SetActive(false);
    }


    // system message ////////////////////////////////////////////////
    private void Start() {
        GameController.instance.playerObject = gameObject;
        invulnerabilityTimer = Time.time;
    }

    private void Update() {
        if (thrownLock) {
            gameObject.GetComponent<PlayerAttack>().Lock(gameObject);
            gameObject.GetComponent<PlayerMovement>().Lock(gameObject);
            gameObject.GetComponent<PlayerSkills>().Lock(gameObject);
            gameObject.GetComponent<Animator>().SetBool("Flying", true);

            StartCoroutine("ShockTimer");
        }
    }

    private void LateUpdate() {

        foreach (GameObject effect in effectsNextFrame) {
            if (effect) {
                effect.GetComponent<SkillTemplate>().NextFrame();
            }
        }

        // sanitize immediate effects
        List<GameObject> temp = new List<GameObject>();
        foreach (GameObject effect in effectsNextFrame) {
            if (effect) {
                temp.Add(effect);
            }
        }
        effectsNextFrame = temp;

        if (effectTakingOver) {
            effectTakingOver.GetComponent<SkillTemplate>().NextFrame();
        }
        else {
            effectTakingOver = null;
        }
    }
}
