﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour {
    public Image healthBar;
    float maxHealth = 100f;
    public float health;
    public GameObject gameOverPopUp;
    public string playerName;
    void OnEnable()
    {
        
        // if (GameObject.Find("HealthBar")!=null)
        // healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
        health = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collission with = "+ collision.gameObject.name);

        if (collision.gameObject.name.Contains("Bomb"))
        {
            if (health > 0)
            {
                 health = health - 40.0f;                
            }
            else
            {
                //TODO: Need to pass the server that Game is over
                Debug.Log("Game Over!!");
                GameManager.instance.isGameOver = true;
                gameOverPopUp.SetActive(true);
                gameOverPopUp.transform.GetChild(0).GetComponent<Text>().text = "Game Over\n " + playerName + " Wins!!!\n Press 'R' to Restart";
            }          
        }
        if(healthBar!=null)
        healthBar.fillAmount = health / maxHealth;
    }
}