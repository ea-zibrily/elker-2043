using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameOverUIController : MonoBehaviour
{
    #region Variable
    
    [Header("Text Component")] 
    [SerializeField] private float displayTextSpeed;
    [SerializeField] private float displayButtonSpeed;
    private string gameOverText;
    [SerializeField] private TextMeshProUGUI gameOverTextUI;
    
    private bool isGameOverTextDone;
    private bool isAddingRichTextTag;
    
    [Space]
    [SerializeField] private GameObject[] buttonObject;
    
    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        DeactivateButton();
        
        gameOverText = gameOverTextUI.text;
        isGameOverTextDone = false;
        gameOverTextUI.text = "";
    }

    private void Update()
    {
        if (!isGameOverTextDone)
        {
            StartCoroutine(DisplayLine());
        }
        else
        {
            StartCoroutine(ActivateButton());
        }
    }
    
    #endregion

    #region Tsukuyomi Callbacks

    private IEnumerator DisplayLine()
    {
        isAddingRichTextTag = false;

        var gameOverTextLetters = gameOverText.ToCharArray();
        foreach (var letter in gameOverTextLetters)
        {
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                gameOverTextUI.text += letter;
                // Sfx Logic
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                gameOverTextUI.text += letter;
                // Sfx Logic
                yield return new WaitForSeconds(displayTextSpeed);
            }
        }
        
        isGameOverTextDone = true;
    }

    private IEnumerator ActivateButton()
    {
        for (int i = 0; i < buttonObject.Length; i++)
        {
            buttonObject[i].SetActive(true);
            yield return new WaitForSeconds(displayButtonSpeed);
        }
    }
    
    private void DeactivateButton()
    {
        for (int i = 0; i < buttonObject.Length; i++)
        {
            buttonObject[i].SetActive(false);
        }
    }
    
    
    #endregion
    
}