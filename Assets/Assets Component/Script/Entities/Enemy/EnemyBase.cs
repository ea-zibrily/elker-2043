using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyBase : MonoBehaviour
{
    #region Variable

    [Header("Enemy Component")]
    public EnemyData enemyDataSO;
    public bool isRight;
    [HideInInspector] public float tempMoveSpeed;
    [HideInInspector] public bool isCaughted;
    [HideInInspector] public Animator myAnim;

    #endregion

    #region Struct

    [Serializable]
    public struct WallDetector
    {
        public Transform wallChecker;
        public float wallCheckerRadius;
        public LayerMask wallLayer;
    }

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        myAnim = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        tempMoveSpeed = enemyDataSO.EnemyMoveSpeed;
    }

    #endregion
    
    #region Tsukuyomi Callbacks
    
    public void EnemyAnimation()
    {
        if (tempMoveSpeed != 0)
        {
            myAnim.SetBool("IsMove", true);
        }
        else
        {
            myAnim.SetBool("IsMove", false);
        }
    }
    
    public void EnemyFlip()
    {
        isRight = !isRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void StopEnemyMovement()
    {
        isCaughted = true;
        tempMoveSpeed = 0f;
    }

    #endregion
}
