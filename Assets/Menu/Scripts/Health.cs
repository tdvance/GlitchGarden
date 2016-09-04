﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    private Attacker attacker;

    public float health = 10f;

    // Use this for initialization
    void Start () {
        attacker = GetComponent<Attacker>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //optionally trigger an animation
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}