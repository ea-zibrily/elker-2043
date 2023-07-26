﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoSingleton<GameManager>
{
    #region Variable

    [Header("Scene Component")]
    [SerializeField] private RectTransform sceneFader;
    
    [Header("Game Over Component")]
    [SerializeField] private GameObject timePanel;
    [SerializeField] private GameObject catchPanel;
    [SerializeField] private GameObject alertLampPanel;

    [Header("Reference")]
    // [SerializeField] private EnemyBase[] enemyBase;
    private EleController eleController;

    #endregion

    #region MonoBehaviour Callbacks

    protected override void Awake()
    {
        base.Awake();
        
        eleController = GameObject.FindGameObjectWithTag("Player").GetComponent<EleController>();
    }

    private void OnEnable()
    {
        GameEventHandler.OnTimerEnd += TimerEnd;
        GameOverButtonEventHandler.OnGameOverButtonActive += StopGameActivities;
        GameEventHandler.OnPlayerCatch += PlayerCatch;
        GameEventHandler.OnGameWin += PlayerWin;
    }

    private void OnDisable()
    {
        GameEventHandler.OnTimerEnd -= TimerEnd;
        GameOverButtonEventHandler.OnGameOverButtonActive -= StopGameActivities;
        GameEventHandler.OnPlayerCatch -= PlayerCatch;
        GameEventHandler.OnGameWin -= PlayerWin;
    }
    
    private void Start()
    {
        StartFader();
        
        timePanel.SetActive(false);
        catchPanel.SetActive(false);
        alertLampPanel.SetActive(false);
    }
    
    #endregion
    
    #region Tsukuyomi Scene Controller Callbacks
    
    public void SceneMoveController(int gameCondition)
    {
        switch (gameCondition)
        {
            case 0:
                OpenMenuScene();
                break;
            case 1:
                OpenGameScene();
                break;
            // Etc Scene w Some Logic
            
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
        
        timePanel.SetActive(true);
    }
    
    private void StopGameActivities() => Time.timeScale = 0f;
    
    private IEnumerator PlayerCatch()
    {
        eleController.StopEleMovement();
        alertLampPanel.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        
        alertLampPanel.SetActive(false);
        catchPanel.SetActive(true);
    }

    public void PlayerWin() => Debug.Log("Win lur");

    #endregion


}