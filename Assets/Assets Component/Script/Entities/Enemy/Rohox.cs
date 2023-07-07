using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rohox : EnemyBase
{
    [Header("Rohox Component")]
    [SerializeField] private bool isRight;
    public WallDetector wallDetector;

    #region MonoBehaviour Callbacks

    private void Start()
    {
        gameObject.name = enemyDataSO.enemyName;
    }
    
    private void FixedUpdate()
    {
        RohoxMove();
    }

    #endregion
    
    #region Tsukuyomi Callbacks

    private void RohoxMove()
    {
        transform.Translate(Vector2.left * (enemyDataSO.enemyMoveSpeed * Time.deltaTime));
        
        if (IsWall())
        {
            RohoxFlip();
        }
    }

    private void RohoxFlip()
    {
        isRight = !isRight;
        transform.Rotate(0f, 180f, 0f);
    }
    
    private bool IsWall()
    {
        return Physics2D.OverlapCircle(wallDetector.wallChecker.position, wallDetector.wallCheckerRadius,
            wallDetector.wallLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallDetector.wallChecker.position, wallDetector.wallCheckerRadius);
    }

    #endregion
   
}
