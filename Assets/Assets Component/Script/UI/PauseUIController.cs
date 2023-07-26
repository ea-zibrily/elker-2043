using System;
using UnityEngine;

public class PauseUIController : MonoBehaviour
{
    #region Variable

    [SerializeField] private GameObject pausePanel;
    private bool isGamePause;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        pausePanel.SetActive(false);
        isGamePause = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    #endregion

    #region Tsukuyomi Callbacks

    private void PauseGame()
    {
        pausePanel.SetActive(true);
        isGamePause = true;
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        pausePanel.SetActive(false);
        isGamePause = false;
        Time.timeScale = 1f;
    }

    #endregion
}