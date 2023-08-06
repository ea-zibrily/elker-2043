using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoSingleton<SceneController>
{
    #region Variable

    [SerializeField] private RectTransform sceneFader;
    [SerializeField] private int sceneNum;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        StartFader();
    }
    
    #endregion

    #region Tsukuyomi Callbacks
    
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
            case 3:
                OpenLevelScene();
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
            SceneManager.LoadScene (0);
            Time.timeScale = 1;
        });
    }
    
    private void OpenGameScene()
    {
        sceneFader.gameObject.SetActive (true);
        
        LeanTween.alpha (sceneFader, 0, 0);
        LeanTween.alpha (sceneFader, 1, 1f).setOnComplete (() => {
            // Example for little pause before laoding the next scene
            Invoke ("LoadGame", 0.5f);
            Time.timeScale = 1;
        });
    }
    
    private void OpenNextLevelScene()
    {
        sceneFader.gameObject.SetActive (true);
        
        LeanTween.alpha (sceneFader, 0, 0);
        LeanTween.alpha (sceneFader, 1, 1f).setOnComplete (() => {
            // Example for little pause before laoding the next scene
            Invoke ("LoadNextLevel", 0.5f);
            Time.timeScale = 1;
        });
    }
    
    private void OpenLevelScene()
    {
        sceneFader.gameObject.SetActive (true);
        
        LeanTween.alpha (sceneFader, 0, 0);
        LeanTween.alpha (sceneFader, 1, 1f).setOnComplete (() => {
            // Example for little pause before laoding the next scene
            Invoke ("LoadLevelScene", 0.5f);
            Time.timeScale = 1;
        });
    }
        
    private void LoadGame () => SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
    private void LoadNextLevel () => SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
    private void LoadLevelScene () => SceneManager.LoadScene (sceneNum);
    public void SetSceneNum(int num) => sceneNum = num;


    #endregion
    
}
