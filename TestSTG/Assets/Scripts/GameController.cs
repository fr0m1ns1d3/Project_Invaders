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
    private int score = 0;
    
    void Start()
    {
        Time.timeScale = 1f;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        gameOverText.text = "";
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startWait);
        while (!gameOver)
        {
            for (int i = 0; i < countHazard; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waitWave);
        }
    }

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetKey("return"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
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
