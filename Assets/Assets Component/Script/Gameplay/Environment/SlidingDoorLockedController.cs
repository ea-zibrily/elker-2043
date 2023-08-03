using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlidingDoorLockedController : EnvironmentBase
{
    #region Variable

    [Header("Sliding Door Lock Component")] 
    [SerializeField] private GameObject lockPanelUI;
    private bool isLockPanelOpen;
    private bool isLockPanelDone;
    
    [Header("Reference")]
    private SlideDoorController slideDoorController;
    private LockController lockController;
    private EleController eleController;
    
    #endregion

    #region MonoBehaviour Callbacks
    
    private void Awake()
    {
        slideDoorController = GetComponent<SlideDoorController>();
        lockController = GameObject.Find("LockController").GetComponent<LockController>();
        eleController = GameObject.FindGameObjectWithTag("Player").GetComponent<EleController>();
    }
    
    private void Start()
    {
        EnvironmentInitialize();
        
        isLockPanelOpen = false;
        isLockPanelDone = false;
    }
    
    private void Update()
    {
        if (!IsPlayerInside)
        {
            return;
        }
        
        if (isLockPanelDone)
        {
            buttonAlertTextUI.text = "";
        }
        else
        {
            if (!Input.GetKeyDown(KeyCode.X))
            {
                return;
            }
            
            if(!isLockPanelOpen)
            {
                OpenLockPanel();
            }
            else
            {
                CloseLockPanel();
            }
        }

        SetActiveSlideDoor();
    }

    #endregion

    #region Tsukuyomi Callbacks

    private void OpenLockPanel()
    {
        eleController.StopEleMovement();
        buttonAlertTextUI.gameObject.SetActive(false);
        isLockPanelOpen = true;
        lockPanelUI.SetActive(true);
    }
    
    private void CloseLockPanel()
    {
        eleController.ResumeEleMovement();
        buttonAlertTextUI.gameObject.SetActive(true);
        isLockPanelOpen = false;
        lockPanelUI.SetActive(false);
    }
    
    private void SetActiveSlideDoor()
    {
        if (lockController.IsLockOpen)
        {
            eleController.ResumeEleMovement();
            lockPanelUI.SetActive(false);
            isLockPanelDone = true;
            slideDoorController.IsLocked = false;
        }
    }
    
    #endregion
}
