using System.Collections;
using UnityEngine;

public class QuizzEventHandler : MonoBehaviour
{
    // Hack success event
    public delegate IEnumerator HackSuccess();
    public event HackSuccess OnHackSuccess;
    
    // Hack failed event
    public delegate IEnumerator HackFailed();
    public event HackFailed OnHackFailed;
    
    public void HackSuccessEvent()
    {
        StartCoroutine(OnHackSuccess?.Invoke());
    }
    
    public void HackFailedEvent()
    {
        StartCoroutine(OnHackFailed?.Invoke());
    }
}