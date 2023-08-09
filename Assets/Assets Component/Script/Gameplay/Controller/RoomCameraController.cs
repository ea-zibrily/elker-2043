using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomCameraController : MonoBehaviour
{
    #region Variable

    private Animator cameraAnimator;

    #endregion

    #region MonoBehaviour Callback

    private void Awake()
    {
        cameraAnimator = GetComponentInChildren<Animator>();
    }

    #endregion

    #region Collider Callbacks
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraAnimator.SetBool("IsRoomTwo", true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraAnimator.SetBool("IsRoomTwo", false);
        }
    }

    #endregion
    
}
