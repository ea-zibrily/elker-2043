using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider2D))]
public class SlideDoorController : MonoBehaviour
{
    #region Variable

    [FormerlySerializedAs("doorCloseTime")]
    [Header("Slide Door Component")]
    [SerializeField] private float closeTime;
    
    [Header("Reference")]
    private SlideDoorEventHandler slideDoorEventHandler;
    private Animator myAnim;

    #endregion
    
    #region MonoBehaviour Callbacks

    private void Awake()
    {
        slideDoorEventHandler = GetComponentInChildren<SlideDoorEventHandler>();
        myAnim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        slideDoorEventHandler.OnOpen += DeactivateDoor;
        slideDoorEventHandler.OnClose += ActivateDoor;
    }

    private void OnDisable()
    {
        slideDoorEventHandler.OnOpen -= DeactivateDoor;
        slideDoorEventHandler.OnClose -= ActivateDoor;
    }

    #endregion

    #region Tsukuyomi Callbacks

    private void ActivateDoor()
    {
        gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
    }
    
    private void DeactivateDoor()
    {
        gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
    }
    
    private IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(closeTime);
        myAnim.SetBool("IsOpen", false);
    }


    #endregion

    #region Collider Callbacks

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            myAnim.SetBool("IsOpen", true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(CloseDoor());
        }
    }

    #endregion
   
}
