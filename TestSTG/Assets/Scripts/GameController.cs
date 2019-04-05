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
    private int score = 0, hazardID, shipCounter, boostID;
    [SerializeField]
    private GameObject pauseMenu;

    private int boostCounter = 1, boostTempCounter = 0;
    private static int boostActive = 1;
    
    void Start()
    {
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
                    }
                }

                hazardID = Random.Range(0, hazards.Length);

                if (hazardID == 5 && shipCounter>=2)
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
}
