﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion, playerExplosion;
    public int customScore = 5;
    private GameController gameController;

    private void Start()
    {
        GameObject gameControllerObj = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObj != null)
        {
            gameController = gameControllerObj.GetComponent<GameController>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "EnemyShip")
        { return; }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (other.CompareTag("Shield"))
        {           
            Destroy(this.gameObject);
            return;
        }

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, transform.position, transform.rotation);
            gameController.GameOver();
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
        gameController.AddScore(customScore);
        gameController.CheckBestScore();
    }
}
