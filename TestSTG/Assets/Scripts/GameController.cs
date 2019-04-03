using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int countHazard;
    public float spawnWait, startWait, waitWave;

    public Text scoreText, gameOverText;
    private bool gameOver = false;
    private int score = 0, hazardID, shipCounter;
    [SerializeField]
    private GameObject pauseMenu;
    
    void Start()
    {
        Time.timeScale = 1f;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        gameOverText.text = "";
        shipCounter = 0;
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startWait);
        while (!gameOver)
        {
            shipCounter = 0;

            for (int i = 0; i < countHazard; i++)
            {
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
