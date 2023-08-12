using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LiftController : EnvironmentBase
{
    #region Variable

    [Header("Lift Component")] 
    [SerializeField] private Transform liftPoint;
    private Transform tempTransform;
    [SerializeField] private bool isLiftOpen;

    [Header("Reference")]
    private Animator myAnim;
    private Transform eleTransform;
    private EleController eleController;
    private LiftEventHandler liftEventHandler;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        eleTransform = GameObject.FindGameObjectWithTag("Player").transform;
        eleController = eleTransform.GetComponent<EleController>();
        myAnim = GetComponentInChildren<Animator>();
        liftEventHandler = GetComponentInChildren<LiftEventHandler>();
    }

    private void OnEnable()
    {
        liftEventHandler.OnLiftOpen += SetLiftOpen;
        liftEventHandler.OnLiftClose += SetLiftClose;
    }

    private void OnDisable()
    {
        liftEventHandler.OnLiftOpen -= SetLiftOpen;
        liftEventHandler.OnLiftClose -= SetLiftClose;
    }

    private void Start()
    {
        EnvironmentInitialize();
        isLiftOpen = false;
    }

    private void Update()
    {
        LiftAnimation();
        
        if (Input.GetKeyDown(KeyCode.Z) && IsPlayerInside && isLiftOpen)
        {
            LiftTeleport();
        }
    }

    #endregion

    #region Tsukuyomi Callbacks

    private void LiftAnimation()
    {
        if (IsPlayerInside && eleController.IsCaught)
        {
            myAnim.SetBool("IsActive", true);
        }
        else
        {
            myAnim.SetBool("IsActive", false);
        }
    }

    private void LiftTeleport()
    {
        eleTransform.position = liftPoint.position;
        FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Lift);
    }
    private void SetLiftOpen() => isLiftOpen = true;
    private void SetLiftClose() => isLiftOpen = false;

    #endregion

}
