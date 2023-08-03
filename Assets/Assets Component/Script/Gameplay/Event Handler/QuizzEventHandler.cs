using System.Collections;
using UnityEngine;

public class QuizzEventHandler : MonoBehaviour
{
    // Hack success event
    public delegate void HackSuccess();
    public event HackSuccess OnHackSuccess;
    
    // Hack failed event
    public delegate void HackFailed();
    public event HackFailed OnHackFailed;
    
    public void HackSuccessEvent() => OnHackSuccess?.Invoke();
    public void HackFailedEvent() => OnHackFailed?.Invoke();
}