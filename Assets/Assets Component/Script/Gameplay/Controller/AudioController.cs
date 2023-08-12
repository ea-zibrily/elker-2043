using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    #region Variable

    [Header("Audio Controller Component")]
    public SoundEnum playSoundEnum;
    public SoundEnum stopSoundEnum;
    [SerializeField] private float mainMenuVolume;
    [SerializeField] private float inGameVolume;
    
    [Header("Reference")]
    private AudioManager audioManager;
    
    #endregion
    
    #region MonoBehaviour Callbacks

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        PlayBGM(playSoundEnum, stopSoundEnum);
        SetupVolume(playSoundEnum);
    }
    
    #endregion
    
    #region Tsukuyomi Callbacks
    
    private void PlayBGM(SoundEnum playEnum, SoundEnum stopEnum)
    {
        audioManager.StopAudio(stopSoundEnum);
        audioManager.PlayAudio(playSoundEnum);
    }
    
    private void SetupVolume(SoundEnum playEnum)
    {
        switch (playEnum)
        {
            case SoundEnum.BGM_MainMenu:
                audioManager.SetVolume(playEnum, mainMenuVolume);
                break;
            case SoundEnum.BGM_Ingame:
                audioManager.SetVolume(playEnum, inGameVolume);
                break;
            default:
                Debug.Log("No BGM is playing");
                break;
        }
    }



    #endregion
}
