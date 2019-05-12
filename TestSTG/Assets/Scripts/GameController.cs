using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] hazards;
    [SerializeField]
    private GameObject[] boosts;
    public Vector3 spawnValues;
    public int countHazard;
    public float spawnWait, startWait, waitWave;

    public Text scoreText, gameOverText;
    private bool gameOver = false;
    private int score = 0, hazardID, shipCounter, boostID, maxShips;
    [SerializeField]
    private GameObject pauseMenu;

    private int boostCounter = 1, boostTempCounter = 0;
    private static int boostActive = 1;
    
    void Start()
    {

        switch (GameValues.Difficulty)
        {
            case GameValues.Difficulties.Easy:
                maxShips = 2;
                countHazard = 3;
                spawnWait = 1;
                break;
            case GameValues.Difficulties.Hard:
                maxShips = 3;
                countHazard = 6;
                spawnWait = 0.5f;
                break;
        }
        Time.timeScale = 1f;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        gameOverText.text = "";
        shipCounter = 0;
        boostCounter = 1;
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startWait);
        while (!gameOver)
        {
            shipCounter = 0;
            boostTempCounter = 0;

            if (countHazard > 8 + boostCounter) boostCounter++;

            for (int i = 0; i < countHazard; i++)
            {
                if (boostTempCounter < boostCounter)
                {
                    if (Random.Range(1, 5) == boostActive)
                    {
                        callBoost();
                        yield return new WaitForSeconds(spawnWait);
                        continue;
                    }
                }

                hazardID = Random.Range(0, hazards.Length);

                if (hazardID == 5 && shipCounter>= maxShips)
                {
                    hazardID = Random.Range(0, hazards.Length-1);
                }

                if (hazardID == 5)
                {
                    shipCounter++;
                }

                GameObject hazard = hazards[hazardID];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                if (hazard.CompareTag("Enemy"))
                {
                    PlayerBolt spd = hazard.GetComponent<PlayerBolt>();

                    switch (GameValues.Difficulty)
                    {
                        case GameValues.Difficulties.Easy:
                            if (spd != null)
                            {
                                spd.speed = -5;
                            }
                            break;
                        case GameValues.Difficulties.Hard:
                            if (spd != null)
                            {
                                spd.speed = -10;
                            }
                            break;
                    }

                } else if (hazard.CompareTag("EnemyShip"))
                {
                    WeaponController wc = hazard.GetComponent<WeaponController>();

                    switch (GameValues.Difficulty)
                    {
                        case GameValues.Difficulties.Easy:
                            if (wc != null)
                            {                           
                                wc.fireRate = 1.5f;
                            }
                            break;
                        case GameValues.Difficulties.Hard:
                            if (wc != null)
                            {
                                wc.fireRate = 0.5f;
                            }
                            break;
                    }
                }
               
                Instantiate(hazard, spawnPosition, spawnRotation); 
                yield return new WaitForSeconds(spawnWait);
            }
            countHazard++;
            yield return new WaitForSeconds(waitWave);
        }

        pauseMenu.SetActive(true);
        gameOver = false;
    }

    private void callBoost()
    {
        boostID = Random.Range(0, boosts.Length);
        GameObject boost = boosts[boostID];
        Vector3 boostPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion boostRotation = Quaternion.identity;
        Instantiate(boost, boostPosition, boostRotation);
        boostTempCounter++;
    }

    public void AddScore(int fixedScore)
    {
        score += fixedScore;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "YOU DIED";
        gameOver = true;
    }

    public void CheckBestScore()
    {
        if (score > PlayerPrefs.GetInt("BestScore", 0)) {
            PlayerPrefs.SetInt("BestScore", score);
        }
    }
}
