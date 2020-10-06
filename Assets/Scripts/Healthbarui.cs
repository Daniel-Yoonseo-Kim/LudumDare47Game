using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbarui : MonoBehaviour
{
    private float health;
    public int numOfHearts;
    public GameObject player;
    public Image[] hearts;
    public Sprite fullheart;
    public Sprite emptyheart;


    // Update is called once per frame
    void Update()
    {
        health = player.GetComponent<PlayerStatus>().health / 10;
        for (int i = 0;  i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullheart;
            }
            else
            {
                hearts[i].sprite = emptyheart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
