using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoorEventHandler : MonoBehaviour
{
    public event Action OnOpen;
    public event Action OnClose;

    public void OnDoorOpen() => OnOpen?.Invoke();
    public void OnDoorClose() => OnClose?.Invoke();
}
