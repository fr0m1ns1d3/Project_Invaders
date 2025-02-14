﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Text bestScore;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public GameObject DifficultyToggles;
    private void Start()
    {
        bestScore.text = "Best Score: " + PlayerPrefs.GetInt("BestScore", 0).ToString();
        DifficultyToggles.transform.GetChild((int)GameValues.Difficulty).GetComponent<Toggle>().isOn = true;
    }

    #region Difficulty
    public void SetEasyDifficulty(bool isOn)
    {
        if (isOn)
            GameValues.Difficulty = GameValues.Difficulties.Easy;

    }
    public void SetHardDifficulty(bool isOn)
    {
       if (isOn)
            GameValues.Difficulty = GameValues.Difficulties.Hard;
    }
    #endregion
}
