using System;
using UnityEngine;

public class ComputerNormalController : EnvironmentBase
{
    #region Variable

    [Header("Computer Component")] 
    [SerializeField] private GameObject computerPanelUI;
    [SerializeField] private GameObject promptPanelUI;

    public bool IsHackDone { get; set; }
    private bool isComputerOpen;
    private bool isPromptOpen;

    [Header("Reference")]
    private EleController eleController;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        eleController = GameObject.FindGameObjectWithTag("Player").GetComponent<EleController>();
    }

    private void Start()
    {
        EnvironmentInitialize();
        isComputerOpen = false;
        IsHackDone = false;
        isPromptOpen = false;
    }

    private void Update()
    {
        if (!IsPlayerInside)
        {
            return;
        }
        
        if (IsHackDone)
        {
            buttonAlertTextUI.text = "";
        }
        else
        {
            if (isPromptOpen)
            {
                return;
            }
        
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!isComputerOpen)
                {
                    OpenComputer();
                }
                else
                {
                    CloseComputer();
                }
            }
        }
    }

    #endregion
    
    #region Tsukuyomi Callbacks

    private void OpenComputer()
    {
        computerPanelUI.SetActive(true);
        eleController.StopEleMovement();
        isComputerOpen = true;
    }

    private void CloseComputer()
    {
        computerPanelUI.SetActive(false);
        eleController.ResumeEleMovement();
        isComputerOpen = false;
    }

    public void OpenPromptPanel()
    {
        promptPanelUI.SetActive(true);
        isPromptOpen = true;
    }

    public void ClosePromptPanel()
    {
        isComputerOpen = false;
        isPromptOpen = false;
        promptPanelUI.SetActive(false);
    }
    
    #endregion
}