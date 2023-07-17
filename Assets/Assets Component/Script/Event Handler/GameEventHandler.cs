using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    [SerializeField] private EleDetector eleDetector;

    private void OnEnable()
    {
        eleDetector.OnCaught += Awowo;
    }

    private void OnDisable()
    {
        eleDetector.OnCaught -= Awowo;
    }

    public void Awowo() => Debug.Log("Handler");
}
