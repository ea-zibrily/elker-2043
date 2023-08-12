using System;
using UnityEngine;

public class EleEventHandler : MonoBehaviour
{
    public event Action OnFootStepSound;
    public void FootstepSound() => OnFootStepSound?.Invoke();
}