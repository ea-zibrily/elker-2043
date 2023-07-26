using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyPatrol : EnemyBase
{
    [Header("Patrol Component")] 
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    private Transform currentPoint;
    private bool isPointArea;

    #region MonoBehaviour Callbacks

    protected override void Start()
    {
        base.Start();
        
        gameObject.name = enemyDataSO.enemyName;
        currentPoint = pointA;
        isPointArea = false;
    }

    private void FixedUpdate()
    {
        if (isPointArea)
        {
            return;
        }

        StartCoroutine(PatrolMove());
    }

    private void Update()
    {
        EnemyAnimation();
    }

    #endregion
    
    #region Tsukuyomi Callbacks

    private IEnumerator PatrolMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, 
            currentPoint.position, tempMoveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, pointA.transform.position) <= 0.1f)
        {
            currentPoint = pointB;
            isPointArea = true;
            
            yield return new WaitForSeconds(3f);
            EnemyFlip();
            isPointArea = false;
        }

        if (Vector2.Distance(transform.position, pointB.transform.position) <= 0.1f)
        {
            currentPoint = pointA;
            isPointArea = true;
            
            yield return new WaitForSeconds(3f);
            EnemyFlip();
            isPointArea = false;
        }
    }

    #endregion
}
