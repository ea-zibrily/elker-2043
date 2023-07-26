using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EleDetector : MonoBehaviour
{
   #region Variable

   public List<EnemyBase> enemyDetected;
   private GameEventHandler gameEventHandler;

   #endregion

   #region MonoBehaviour Callbacks

   private void Awake()
   {
      gameEventHandler = GameObject.Find("GameEvent").GetComponent<GameEventHandler>();
   }

   private void Start()
   {
      var isTrigger = gameObject.GetComponent<BoxCollider2D>().isTrigger;
      
      if (!isTrigger)
      {
         gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
      }
   }

   #endregion

   #region Tsukuyomi Callbacks

   private void HandleEnemyComponent(GameObject enemyObject)
   {
      Debug.Log($"{enemyObject.name} is detected");
      
      var enemyObj = enemyObject.GetComponent<EnemyBase>();
      if (enemyObj != null)
      {
         enemyDetected.Add(enemyObj);
      }
      
      GameEventHandler.CameraShakeEvent();
      StopEnemyDetectedMovement();
      gameEventHandler.PlayerCatchEvent();
   }

   public void StopEnemyDetectedMovement()
   {
      enemyDetected[0].StopEnemyMovement();
      enemyDetected.Clear();
   }

   #endregion


   #region Collider2D Callbacks

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("EnemyBody"))
      {
         HandleEnemyComponent(other.gameObject);
      }
      else if (other.CompareTag("EnemyLighting"))
      {
         var enemyLighting = other.gameObject.GetComponentInParent<EnemyBase>();
         if (enemyLighting != null)
         {
            HandleEnemyComponent(enemyLighting.gameObject);
         }
      }
   }

   #endregion

}
