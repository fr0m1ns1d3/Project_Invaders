using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kastyl : MonoBehaviour
{
    private bool active;

    void Start()
    {
        active = PauseMenu.GameIsPaused;
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (active)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }
}
