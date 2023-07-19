using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleNarratorSystem : MonoBehaviour, IObserver
{
    public ObserverSubjects[] observerSubjects;
    // private GameEvent gameEvent;
    //
    // private void Awake()
    // {
    //     gameEvent = GameObject.Find("GameEvent").GetComponent<GameEvent>();
    // }

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
                Debug.Log("Caught");
                
                // Camera Shake Logic
                // Audio Logic
                
                observerSubjects[0].gameObject.GetComponent<EleController>().isCaught = true;
                observerSubjects[1].gameObject.GetComponent<EleDetector>().enemyDetected[0].isCaughted = true;
                // gameEvent.GameOverEvent();
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
