using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameOverUIController : MonoBehaviour
{
    #region Variable
    
    [Header("Main Component")] 
    [SerializeField] private float displayTextSpeed;
    [SerializeField] private float displayButtonSpeed;
    private string gameOverText;
    [SerializeField] private TextMeshProUGUI gameOverTextUI;
    
    private bool isGameOverTextDone;
    private bool isAddingRichTextTag;

    [Space]
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

    #endregion
    
}