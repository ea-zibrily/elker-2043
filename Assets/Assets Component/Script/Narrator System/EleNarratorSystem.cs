using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleNarratorSystem : MonoBehaviour, IObserver
{
    public ObserverSubjects observerSubjects;

    [Header("Reference")]
    private EleController eleController;

    private void OnEnable()
    {
        observerSubjects.AddObserver(this);
    }

    private void OnDisable()
    {
        observerSubjects.RemoveObserver(this);
    }

    private void Awake()
    {
        eleController = GameObject.FindGameObjectWithTag("Player").GetComponent<EleController>();
    }

    public void AddNotify(ActionEnum actionEnum)
    {
        switch (actionEnum)
        {
            case ActionEnum.Idle:
                // Some logic
                
                break;
            case ActionEnum.Walk:
                // Some logic
                
                break;
            case ActionEnum.Jump:
                // Some logic
                Debug.Log("Jump");
                break;
            case ActionEnum.Caught:
                // Some logic
                
                break;
            default:
                Debug.LogError("ActionEnum not found");
                break;
        }
    }
}
