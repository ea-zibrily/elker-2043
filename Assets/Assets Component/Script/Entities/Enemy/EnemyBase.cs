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

    #region Tsukuyomi Callbacks

   

    #endregion
}
