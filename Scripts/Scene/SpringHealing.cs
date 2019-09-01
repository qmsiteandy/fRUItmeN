﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringHealing : MonoBehaviour {

    public float healingTotalTime = 3f;
    public int TotalAmount = 300;
    public ParticleSystem healingFX;

    public int leftAmount; //剩餘治癒量
    private float healingCycle = 1f;
    public int amountPerTime;

    private bool isHealing = false;
    public float timer = 0f;

    private PlayerControl playerControl;
    private Vector3 playerPos;
    
	// Use this for initialization
	void Start ()
    {
        leftAmount = TotalAmount;
        amountPerTime = TotalAmount / (int)(healingTotalTime / healingCycle); 

        ParticleSystem.MainModule main = healingFX.main;
        main.duration = healingTotalTime;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isHealing)
        {
            if(timer >= healingCycle)
            {
                timer = 0f;

                playerControl.TakeHeal(amountPerTime, amountPerTime);
                leftAmount -= amountPerTime;
                if (leftAmount <= 0) HealingOver();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }        
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !isHealing)
        {
            playerControl = collider.GetComponent<PlayerControl>();
            playerPos = collider.transform.position;

            isHealing = true;

            healingFX.transform.position = playerPos;
            healingFX.Play();
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && isHealing)
        {
            playerControl = collider.GetComponent<PlayerControl>();

            playerPos = collider.transform.position;
            healingFX.transform.position = playerPos;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && isHealing)
        {
            playerControl = null;

            isHealing = false;

            healingFX.Pause();
        }
    }

    void HealingOver()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
