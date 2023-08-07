using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ComputerHackController : EnvironmentBase
{
    #region Variable

    [Header("Hold Component")]
    [SerializeField] private Image fillBarUI;
    [SerializeField] private float fillBarSpeed;
    [field: SerializeField] public bool IsComplete {get; private set;}
    private float maxFillBar;
    private float minFillBar;
    
    [Header("Reference")]
    private Animator holdPanelAnim;
    private GameEventHandler gameEventHandler;
    private UnlockLevelManager unlockLevelManager;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        holdPanelAnim = GetComponentInChildren<Animator>();
        gameEventHandler = GameObject.Find("GameEvent").GetComponent<GameEventHandler>();
        unlockLevelManager = GameObject.Find("UnlockLevelManager").GetComponent<UnlockLevelManager>();
    }

    private void Start()
    {
        EnvironmentInitialize();
        
        maxFillBar = 1f;
        minFillBar = 0f;
        
        fillBarUI.fillAmount = minFillBar;
        IsComplete = false;
    }

    private void Update()
    {
        HoldObject();
    }

    #endregion

    #region Tsukuyomi Callbacks

    private void HoldObject()
    {
        if (!IsPlayerInside)
        {
           return;
        }
        
        if (IsComplete)
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
            GameEventHandler.CameraShakeEvent();
            
            fillBarUI.fillAmount = maxFillBar;
            holdPanelAnim.SetBool("IsHolding", false);
            IsComplete = true;
            
            unlockLevelManager.LevelUnlockEvent();
            gameEventHandler.GameWinEvent();
        }
    }
    
    private IEnumerator ResetBar()
    {
        IsComplete = false;
        holdPanelAnim.SetBool("IsHolding", false);
        yield return new WaitForSeconds(0.25f);
        
        fillBarUI.fillAmount = minFillBar;
    }

    #endregion
    
}