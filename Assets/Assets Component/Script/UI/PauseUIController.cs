using System;
using UnityEngine;

public class PauseUIController : MonoBehaviour
{
    #region Variable

    [SerializeField] private GameObject pausePanel;
    private bool isGamePause;
    private bool isGameOver;

    #endregion

    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        GameEventHandler.OnDisableGamePause += DisablePause;
    }

    private void OnDisable()
    {
        GameEventHandler.OnDisableGamePause -= DisablePause;
    }

    private void Start()
    {
        pausePanel.SetActive(false);
        isGamePause = false;
        isGameOver = false;
    }

    private void Update()
    {
        if (isGameOver)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Click);
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

    private void DisablePause() => isGameOver = true;
    
    #endregion
}