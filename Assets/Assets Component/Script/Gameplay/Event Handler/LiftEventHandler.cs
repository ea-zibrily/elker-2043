using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftEventHandler : MonoBehaviour
{
    public event Action OnLiftOpen;
    public event Action OnLiftClose;
    
    public void LiftOpenEvent() => OnLiftOpen?.Invoke();
    public void LiftCloseEvent() => OnLiftClose?.Invoke();
}
