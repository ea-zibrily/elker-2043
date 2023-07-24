using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HackComputerController : EnvironmentBase
{
    #region Variable

    [Header("Hold Component")]
    [SerializeField] private Image fillBarUI;
    [SerializeField] private float fillBarSpeed;
    [SerializeField] private bool isComplete;
    private float maxFillBar;
    private float minFillBar;
    
    [Header("Reference")]
    private Animator holdPanelAnim;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        holdPanelAnim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        EnvironmentInitialize();
        
        maxFillBar = 1f;
        minFillBar = 0f;
        
        fillBarUI.fillAmount = minFillBar;
        isComplete = false;
    }

    private void Update()
    {
        HoldObject();
        
        if (isComplete)
        {
            GameEventHandler.GameWinEvent();
        }
    }

    #endregion

    #region Tsukuyomi Callbacks

    private void HoldObject()
    {
        if (!IsPlayerInside)
        {
           return;
        }
        
        if (isComplete)
        {
            return;
        }
        
        if (Input.GetKey(KeyCode.C))
        {
            StartCoroutine(IncreaseBar());
        }
        else
        {
            StartCoroutine(ResetBar());
        }
    }

    private IEnumerator IncreaseBar()
    {
        buttonAlertTextUI.gameObject.SetActive(false);
        holdPanelAnim.SetBool("IsHolding", true);
        yield return new WaitForSeconds(0.23f);
        
        if (fillBarUI.fillAmount < maxFillBar)
        {
            fillBarUI.fillAmount += fillBarSpeed * Time.deltaTime;
        }
        else
        {
            fillBarUI.fillAmount = maxFillBar;
            holdPanelAnim.SetBool("IsHolding", false);
            isComplete = true;
        }
    }

    private IEnumerator ResetBar()
    {
        isComplete = false;
        holdPanelAnim.SetBool("IsHolding", false);
        yield return new WaitForSeconds(0.25f);
        
        fillBarUI.fillAmount = minFillBar;
    }

    #endregion
    
}