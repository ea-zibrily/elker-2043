using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    // Player Catch
    public delegate void PlayerCatch();
    public static event PlayerCatch OnPlayerCatch;
    
    // Timer
    public delegate void TimerEnd();
    public static event TimerEnd OnTimerEnd;

    // Game Over
    public delegate IEnumerator GameOver();
    public static event GameOver OnGameOver;
    
    
    public static void PlayerCatchEvent() => OnPlayerCatch?.Invoke();
    public static void TimerEndEvent() => OnTimerEnd?.Invoke();
    public void GameOverEvent() => StartCoroutine(OnGameOver?.Invoke());
}