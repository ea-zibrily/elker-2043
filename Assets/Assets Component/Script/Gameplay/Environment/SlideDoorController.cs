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
    [field: SerializeField] public bool IsLocked { get; set; }
    
    [Header("Reference")]
    private SlideDoorEventHandler slideDoorEventHandler;
    private Animator myAnim;
    [SerializeField] private BoxCollider2D physicsCollider;

    #endregion
    
    #region MonoBehaviour Callbacks

    private void Awake()
    {
        slideDoorEventHandler = GetComponentInChildren<SlideDoorEventHandler>();
        myAnim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        slideDoorEventHandler.OnOpen += DeactivateCollider;
        slideDoorEventHandler.OnClose += ActivateCollider;
    }

    private void OnDisable()
    {
        slideDoorEventHandler.OnOpen -= DeactivateCollider;
        slideDoorEventHandler.OnClose -= ActivateCollider;
    }

    #endregion

    #region Tsukuyomi Callbacks

    private IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(closeTime);
        myAnim.SetBool("IsOpen", false);
    }
    
    private void DeactivateCollider() => physicsCollider.enabled = false;
    private void ActivateCollider() => physicsCollider.enabled = true;
    
    #endregion

    #region Collider Callbacks
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !IsLocked)
        {
            myAnim.SetBool("IsOpen", true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !IsLocked)
        {
            StartCoroutine(CloseDoor());
        }
    }

    #endregion
   
}
