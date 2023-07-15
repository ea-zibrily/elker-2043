using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleNarratorSystem : MonoBehaviour, IObserver
{
    public ObserverSubjects observerSubjects;
    
    private void OnEnable()
    {
        observerSubjects.AddObserver(this);
    }

    private void OnDisable()
    {
        observerSubjects.RemoveObserver(this);
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
                
                break;
            case ActionEnum.Caught:
                // Some logic
                
                break;
            default:
                Debug.LogError("Action Enum not found");
                break;
        }
    }
}
