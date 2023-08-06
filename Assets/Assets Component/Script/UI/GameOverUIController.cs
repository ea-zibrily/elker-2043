using System;
using TMPro;
using UnityEngine;

public class GameOverUIController : GameWinUIController
{
    #region Variable

    [Header("Game Over Component")]
    [SerializeField] private string[] tempGameOverText;
    
    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        gameOverText = gameOverTextUI.text;
        InitializeResultUI();
        StartCoroutine(DisplayLine());
    }

    private void Update()
    {
        if (!IsGameOverTextDone)
        {
            return;
        }
        
        StartCoroutine(ActivateButton());
    }

    #endregion
    
    #region Tsukuyomi Callbacks

    public void SetTextPlayerCaught() => gameOverTextUI.text = tempGameOverText[0];
    public void SetTextTimesUp() => gameOverTextUI.text = tempGameOverText[1];


    #endregion
}