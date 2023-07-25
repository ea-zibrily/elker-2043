using System;
using UnityEngine;

public class GameOverButtonEventHandler : MonoBehaviour
{
    public static event Action OnGameOverButtonActive;
    
    public void GameOverButtonActive() => OnGameOverButtonActive?.Invoke();
}