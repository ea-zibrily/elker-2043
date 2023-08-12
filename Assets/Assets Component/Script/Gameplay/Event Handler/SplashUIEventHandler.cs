using System;
using UnityEngine;

public class SplashUIEventHandler : MonoBehaviour
{
    public static event Action OnSplashUIDoneEvent;
    public void SplashUIDone() => OnSplashUIDoneEvent?.Invoke();
}