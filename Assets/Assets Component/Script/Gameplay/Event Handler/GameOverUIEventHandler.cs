using System;
using UnityEngine;

public class GameOverUIEventHandler : MonoBehaviour
{
    public static event Action OnGameOverButtonActive;
    
    public void GameOverButtonActive() => OnGameOverButtonActive?.Invoke();
}