﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Town()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().toTown();
    }
}
