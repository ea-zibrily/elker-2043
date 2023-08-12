using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class LockController : MonoBehaviour
{
    #region Variable

    [Header("Main Component")] 
    [SerializeField] private string lockKey;
    [SerializeField] private string tempKey;
    [SerializeField] private TextMeshProUGUI lockKeyTextUI;
    [SerializeField] private TextMeshProUGUI successCodeKeyTextUI;
    public bool IsLockOpen {get; private set;}

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        IsLockOpen = false;
    }

    private void Update()
    {
        lockKeyTextUI.text = tempKey;
        successCodeKeyTextUI.text = "-" + lockKey + "-"; 
        CheckLockKey();
    }

    #endregion

    private void CheckLockKey()
    {
        if (tempKey.Length > lockKey.Length - 1)
        {
            if (tempKey == lockKey)
            {
                StartCoroutine(LockCorrect());
            }
            else
            {
                StartCoroutine(LockWrong());
            }
        }
    }

    private IEnumerator LockCorrect()
    {
        lockKeyTextUI.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        
        IsLockOpen = true;
    }

    private IEnumerator LockWrong()
    {
        GameEventHandler.CameraShakeEvent();
        lockKeyTextUI.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        
        tempKey = "";
        lockKeyTextUI.color = Color.white;
        IsLockOpen = false;
    }
    
    public void InputLockKey(string key) => tempKey += key;
    
}