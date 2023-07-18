using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EleDetector : ObserverSubjects
{
   public List<EnemyBase> enemyDetected {get; set;}

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
         enemyDetected.Add(other.gameObject.GetComponent<EnemyBase>());
         NotifyObservers(ActionEnum.Caught);
      }
   }
}
