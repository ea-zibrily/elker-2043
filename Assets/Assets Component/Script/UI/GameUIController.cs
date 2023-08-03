using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameUIController : MonoBehaviour
{
    #region Variable

    [Header("Game Condition Component")] 
    [SerializeField] private float displayTextSpeed;
    [SerializeField] private float displayButtonSpeed;
    [SerializeField] private string gameOverText;
    [SerializeField] private string[] gameOverTextTemp;
    
    private bool isGameOverTextDone;
    private bool isAddingRichTextTag;

    [Space]
    [SerializeField] TextMeshProUGUI gameOverTextUI;
    [SerializeField] TextMeshProUGUI headerTextUI;
    [SerializeField] private GameObject buttonObject;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        gameOverText = gameOverTextUI.text;
        isGameOverTextDone = false;
        buttonObject.SetActive(false);

        StartCoroutine(DisplayLine());
    }

    private void Update()
    {
        if (isGameOverTextDone)
        {
            StartCoroutine(ActivateButton());
        }
    }
    
    #endregion

    #region Tsukuyomi Callbacks

    private IEnumerator DisplayLine()
    {
        var gameOverTextLetters = gameOverText.ToCharArray();
        isAddingRichTextTag = false;
        gameOverTextUI.text = "";

        yield return new WaitForSeconds(0.5f);
        foreach (var letter in gameOverTextLetters)
        {
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                gameOverTextUI.text += letter;
                FindObjectOfType<AudioManager>().Play(SoundEnum.SFX_Typing);
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                gameOverTextUI.text += letter;
                FindObjectOfType<AudioManager>().Play(SoundEnum.SFX_Typing);
                yield return new WaitForSeconds(displayTextSpeed);
            }
        }
        
        isGameOverTextDone = true;
    }

    private IEnumerator ActivateButton()
    {
        yield return new WaitForSeconds(displayButtonSpeed);
        buttonObject.SetActive(true);
    }
    
    public void SetTextGameWin()
    {
        gameOverTextUI.text = gameOverTextTemp[0];
        headerTextUI.text = "Game Win";
    }

    public void SetTextPlayerCaught()
    {
        gameOverTextUI.text = gameOverTextTemp[1];
        headerTextUI.text = "Game Over";
    }

    public void SetTextTimesUp()
    {
        gameOverTextUI.text = gameOverTextTemp[2];
        headerTextUI.text = "Game Over";
    }
    
    
    
    #endregion
    
}