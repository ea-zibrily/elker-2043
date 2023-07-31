using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    #region Variable

    [Header("Scene Component")]
    [SerializeField] private RectTransform sceneFader;
    
    [Header("Game Over Component")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject alarmLampPanel;

    [Header("Reference")]
    private EleController eleController;

    #endregion

    #region MonoBehaviour Callbacks
    
    private void OnEnable()
    {
        GameEventHandler.OnTimerEnd += TimerEnd;
        GameOverUIEventHandler.OnGameOverButtonActive += StopGameActivities;
        GameEventHandler.OnPlayerCatch += PlayerCatch;
        GameEventHandler.OnGameWin += PlayerWin;
    }

    private void OnDisable()
    {
        GameEventHandler.OnTimerEnd -= TimerEnd;
        GameOverUIEventHandler.OnGameOverButtonActive -= StopGameActivities;
        GameEventHandler.OnPlayerCatch -= PlayerCatch;
        GameEventHandler.OnGameWin -= PlayerWin;
    }
    
    private void Start()
    {
        StartFader();
        
        gameOverPanel.SetActive(false);
        alarmLampPanel.SetActive(false);
    }
    
    #endregion
    
    #region Tsukuyomi Scene Controller Callbacks
    
    public void SceneMoveController(int gameCondition)
    {
        switch (gameCondition)
        {
            case 0:
                OpenGameScene();
                break;
            case 1:
                OpenMenuScene();
                break;
            case 2:
                OpenNextLevelScene();
                break;
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
    
    private void OpenMenuScene () 
    {
        sceneFader.gameObject.SetActive (true);
        
        LeanTween.alpha (sceneFader, 0, 0);
        LeanTween.alpha (sceneFader, 1, 1f).setOnComplete (() => {
            SceneManager.LoadScene (1);
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

    private IEnumerator TimerEnd()
    {
        eleController.StopEleMovement();
        yield return new WaitForSeconds(0.3f);
        
        gameOverPanel.GetComponent<GameOverUIController>().SetTextTimesUp();
        gameOverPanel.SetActive(true);
    }
    
    private IEnumerator PlayerCatch()
    {
        eleController.StopEleMovement();
        alarmLampPanel.SetActive(true);
        FindObjectOfType<AudioManager>().Play(SoundEnum.SFX_Alarm);
        yield return new WaitForSeconds(3.5f);
        
        FindObjectOfType<AudioManager>().Stop(SoundEnum.SFX_Alarm);
        alarmLampPanel.SetActive(false);
        gameOverPanel.GetComponent<GameOverUIController>().SetTextPlayerCaught();
        gameOverPanel.SetActive(true);
    }

    public void PlayerWin() => Debug.Log("Win lur");
    private void StopGameActivities() => Time.timeScale = 0f;

    #endregion


}