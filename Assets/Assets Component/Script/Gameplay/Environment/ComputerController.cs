using System;
using UnityEngine;

public class ComputerController : EnvironmentBase
{
    #region Variable

    [Header("Computer Component")] 
    [SerializeField] private GameObject computerPanelUI;
    [SerializeField] private GameObject promptPanelUI;

    private bool isComputerOpen;

    [Header("Reference")]
    private EleController eleController;
    private GameObject eleDetectorObject;
    
    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        eleController = GameObject.FindGameObjectWithTag("Player").GetComponent<EleController>();
        eleDetectorObject = GameObject.Find("EleDetector");
    }

    private void Start()
    {
        EnvironmentInitialize();
        isComputerOpen = false;
        
        Debug.Log($"eleDetectorObject name: {eleDetectorObject}");
    }

    private void Update()
    {
        if (!IsPlayerInside)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.X))
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

    #endregion
    
    #region Tsukuyomi Callbacks

    private void OpenComputer()
    {
        computerPanelUI.SetActive(true);
        eleController.StopEleMovement();
        eleDetectorObject.SetActive(false);
        isComputerOpen = true;
    }

    private void CloseComputer()
    {
        computerPanelUI.SetActive(false);
        eleController.ResumeEleMovement();
        eleDetectorObject.SetActive(true);
        isComputerOpen = false;
    }
    
    public void OpenPromptPanel() => promptPanelUI.SetActive(true);

    #endregion
}