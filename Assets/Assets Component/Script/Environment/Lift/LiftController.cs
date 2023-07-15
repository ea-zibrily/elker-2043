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

    [Header("Reference")]
    private Animator myAnim;
    private Transform eleTransform;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        eleTransform = GameObject.FindGameObjectWithTag("Player").transform;
        myAnim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        EnvironmentInitialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && IsPlayerInside)
        {
            LiftTeleport();
        }
    }

    #endregion

    #region Tsukuyomi Callbacks

    private void LiftTeleport() => eleTransform.position = liftPoint.position;

    #endregion

}
