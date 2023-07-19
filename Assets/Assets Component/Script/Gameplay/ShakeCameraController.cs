using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class ShakeCameraController : MonoBehaviour
{
    #region Variable

    [Header("Shake Component")] 
    [SerializeField] private float hitAmplitudeGain;
    [SerializeField] private float hitFrequencyGain;
    private float shakeTimeElapse;
    [SerializeField] private float shakeDuration;
    [SerializeField] private bool isShaking;
    
    [Header("Reference")]
    private CinemachineVirtualCamera myVirtualCamera;
    private CinemachineBasicMultiChannelPerlin myVirtualCameraNoise;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        myVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        myVirtualCameraNoise = myVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
        StopShake();
    }

    private void Update()
    {
        if (!isShaking)
        {
            return;
        }
        
        shakeTimeElapse += Time.deltaTime;
        if (shakeTimeElapse >= shakeDuration)
        {
            Debug.Log("Stop Shake");
            StopShake();
        }
    }

    #endregion

    #region Tsukuyomi Callbacks

    private void StartShake()
    {
        Debug.Log("Ayo Shake");
        
        myVirtualCameraNoise.m_AmplitudeGain = hitAmplitudeGain;
        myVirtualCameraNoise.m_FrequencyGain = hitFrequencyGain;
        shakeTimeElapse = 0f;
        isShaking = true;
    }

    private void StopShake()
    {
        myVirtualCameraNoise.m_AmplitudeGain = 0f;
        myVirtualCameraNoise.m_FrequencyGain = 0f;
        isShaking = false;
    }

    #endregion
}
