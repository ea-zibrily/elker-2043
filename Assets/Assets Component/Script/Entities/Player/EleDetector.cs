using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EleDetector : MonoBehaviour
{
   public List<EnemyBase> enemyDetected {get; set;}
   private GameEventHandler gameEventHandler;

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

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Enemy"))
      {
         Debug.Log($"{other.gameObject.name} is detected");
         GameEventHandler.CameraShakeEvent();
         gameEventHandler.PlayerCatchEvent();
      }
   }

   public void StopEnemyDetected()
   {
      enemyDetected[0].StopEnemyMovement();
      enemyDetected.Clear();
   }
}
