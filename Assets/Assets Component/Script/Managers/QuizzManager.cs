using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class QuizzManager : MonoBehaviour
{
    #region Variable

    [Header("Computer UI Component")]
    [SerializeField] private GameObject[] descriptionPanelUI;

    [Header("Reference")]
    private QuizzEventHandler quizEventHandler;
    private ComputerNormalController computerNormalController;
    private EleController eleController;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        quizEventHandler = GameObject.Find("QuizController").GetComponent<QuizzEventHandler>();
        computerNormalController = GameObject.FindGameObjectWithTag("ComputerNormal").
            GetComponent<ComputerNormalController>();
        eleController = GameObject.FindGameObjectWithTag("Player").GetComponent<EleController>();
    }

    private void OnEnable()
    {
        quizEventHandler.OnHackSuccess += HackSuccess;
        quizEventHandler.OnHackFailed += HackFailed;
    }
    
    private void OnDisable()
    {
        quizEventHandler.OnHackSuccess -= HackSuccess;
        quizEventHandler.OnHackFailed -= HackFailed;
    }

    #endregion

    #region Tsukuyomi Callbacks
    
    private void HackSuccess()
    {
        descriptionPanelUI[1].SetActive(true);
        descriptionPanelUI[2].SetActive(true);
        computerNormalController.IsHackDone = true;
    }
    
    private void HackFailed()
    {
        descriptionPanelUI[1].SetActive(true);
        descriptionPanelUI[3].SetActive(true);
        computerNormalController.IsHackDone = false;
    }

    private void CloseHackPanel()
    {
        for (int i = 0; i < descriptionPanelUI.Length; i++)
        {
            descriptionPanelUI[i].SetActive(false);
        }
    }
    
    public void CloseComputerPanel()
    {
        CloseHackPanel();
        computerNormalController.ClosePromptPanel();
        eleController.ResumeEleMovement();
    }

    #endregion
    
}