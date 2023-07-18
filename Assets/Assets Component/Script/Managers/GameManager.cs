using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    #region Variable

    [Header("Scene Component")]
    [SerializeField] private RectTransform sceneFader;

    [Header("Game Over Component")]
    [SerializeField] private GameObject gameOverPanel;
    
    [Header("Timer Component")]
    [SerializeField] private TextMeshProUGUI timerTextUI;


    #endregion

    #region MonoBehaviour Callbacks
    
    private void OnEnable()
    {
        GameEvent.OnTimerEnd += TimerEnd;
        GameEvent.OnPlayerCatch += PlayerCatch;
        GameEvent.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        GameEvent.OnTimerEnd -= TimerEnd;
        GameEvent.OnPlayerCatch -= PlayerCatch;
        GameEvent.OnGameOver -= GameOver;
    }
    
    private void Start()
    {
        StartFader();
    }

    #endregion

    #region Tsukuyomi Scene Controller Callbacks
    
    public void SceneMoveController(GameCondition gameCondition)
    {
        switch (gameCondition)
        {
            case GameCondition.GameOver:
                // Some Logic
                break;
            // Some Logic
            default:
                Debug.LogWarning("Game Conditionnya Gaada Kang");
                break;
        }
    }

    private void StartFader()
    {
        sceneFader.gameObject.SetActive (true);
        
        LeanTween.alpha (sceneFader, 1, 0);
        LeanTween.alpha (sceneFader, 0, 1f).setOnComplete (() => {
            sceneFader.gameObject.SetActive (false);
        });
    }
    
    public void OpenMenuScene () 
    {
        sceneFader.gameObject.SetActive (true);
        
        LeanTween.alpha (sceneFader, 0, 0);
        LeanTween.alpha (sceneFader, 1, 1f).setOnComplete (() => {
            SceneManager.LoadScene (0);
        });
    }
    
    private void OpenGameScene()
    {
        sceneFader.gameObject.SetActive (true);
        
        LeanTween.alpha (sceneFader, 0, 0);
        LeanTween.alpha (sceneFader, 1, 1f).setOnComplete (() => {
            // Example for little pause before laoding the next scene
            Invoke ("LoadGame", 0.5f);
        });
    }
    private void OpenNextLevelScene()
    {
        sceneFader.gameObject.SetActive (true);
        
        // ALPHA
        LeanTween.alpha (sceneFader, 0, 0);
        LeanTween.alpha (sceneFader, 1, 1f).setOnComplete (() => {
            // Example for little pause before laoding the next scene
            Invoke ("LoadNextLevel", 0.5f);
        });
    }
        
    private void LoadGame () => SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
    private void LoadNextLevel () => SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);

    #endregion

    #region Tsukuyomi Game Event Callbacks
    
    private void PlayerCatch() => Debug.Log("Game Over");
    private void TimerEnd() => Debug.Log("End Game");
    
    private IEnumerator GameOver()
    {
        // Kedap Kedip Nyala Logic
        yield return new WaitForSeconds(1f);
        
        // Kedap Kedip Mati Logic
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        Debug.Log("Game Over");
    }

    #endregion
    
    
}