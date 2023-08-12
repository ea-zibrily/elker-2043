using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameWinUIController : MonoBehaviour
{
    #region Variable

    [Header("Base Component")]
    public string gameOverText;
    [field: SerializeField] public float DisplayTextSpeed { get; private set; }
    [field: SerializeField] public float DisplayButtonSpeed { get; private set; }
    public bool IsGameOverTextDone { get; set; }
    public bool IsAddingRichTextTag { get; set; }
    
    [Space]
    public TextMeshProUGUI gameOverTextUI;
    public GameObject buttonObject;
    
    #endregion

    #region MonoBehaviour Callbacks
    
    private void Start()
    {
        gameOverTextUI.text = "";
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

    protected void InitializeResultUI()
    {
        IsGameOverTextDone = false;
        buttonObject.SetActive(false);
    }
    
    protected IEnumerator DisplayLine()
    {
        var gameOverTextLetters = gameOverText.ToCharArray();
        IsAddingRichTextTag = false;
        gameOverTextUI.text = "";
        
        yield return new WaitForSeconds(0.5f);
        foreach (var letter in gameOverTextLetters)
        {
            if (letter == '<' || IsAddingRichTextTag)
            {
                IsAddingRichTextTag = true;
                gameOverTextUI.text += letter;
                if (letter == '>')
                {
                    IsAddingRichTextTag = false;
                }
            }
            else
            {
                gameOverTextUI.text += letter;
                FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Typing);
                yield return new WaitForSeconds(DisplayTextSpeed);
            }
        }
        
        IsGameOverTextDone = true;
    }
    
    protected IEnumerator ActivateButton()
    {
        yield return new WaitForSeconds(DisplayButtonSpeed);
        buttonObject.SetActive(true);
    }

    #endregion
}