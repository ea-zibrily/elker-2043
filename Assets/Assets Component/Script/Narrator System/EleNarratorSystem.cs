using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleNarratorSystem : MonoBehaviour, IObserver
{
    public ObserverSubjects[] observerSubjects;
    private GameEventHandler gameEventHandler;
    
    private void Awake()
    {
        gameEventHandler = GameObject.Find("GameEvent").GetComponent<GameEventHandler>();
    }

    private void OnEnable()
    {
        InitializeSubject();
    }

    private void OnDisable()
    {
        RemoveSubject();
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

    private void InitializeSubject()
    {
        for (int i = 0; i < observerSubjects.Length; i++)
        {
            observerSubjects[i].AddObserver(this);
        }
    }
    
    private void RemoveSubject()
    {
        for (int i = 0; i < observerSubjects.Length; i++)
        {
            observerSubjects[i].RemoveObserver(this);
        }
    }
}
