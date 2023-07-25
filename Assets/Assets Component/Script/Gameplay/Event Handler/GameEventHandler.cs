using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    // Camera Shaker
    public delegate void CameraShake();
    public static event CameraShake OnCameraShake;
    
    // Game Over when Player Catch
    public delegate IEnumerator PlayerCatch();
    public static event PlayerCatch OnPlayerCatch;

    // Game Over when Timer Done
    public delegate IEnumerator TimerEnd();
    public static event TimerEnd OnTimerEnd;
    
    // Game Win 
    public delegate void GameWin();
    public static event GameWin OnGameWin;
    
    
    
    public static void CameraShakeEvent() => OnCameraShake?.Invoke();
    public  void PlayerCatchEvent() => StartCoroutine(OnPlayerCatch?.Invoke());
    public void TimerEndEvent() => StartCoroutine(OnTimerEnd?.Invoke());
    public static void GameWinEvent() => OnGameWin?.Invoke();
}