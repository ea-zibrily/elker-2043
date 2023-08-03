using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    #region Variable

    [Header("Audio Controller Component")]
    [SerializeField] private bool isChangeBGM;
    public SoundEnum playSoundEnum;
    public SoundEnum stopSoundEnum;
    
    #endregion
    
    #region MonoBehaviour Callbacks
    
    private void Start()
    {
        if (isChangeBGM)
        {
            PlayAnotherBGM(playSoundEnum, stopSoundEnum);
        }
        else
        {
            PlayOneBGM(playSoundEnum);
        }
    }

    #endregion


    #region Tsukuyomi Callbacks

    private void PlayOneBGM(SoundEnum playEnum) => FindObjectOfType<AudioManager>().Play(playEnum);

    private void PlayAnotherBGM(SoundEnum playEnum, SoundEnum stopEnum)
    {
        FindObjectOfType<AudioManager>().Stop(stopEnum);
        FindObjectOfType<AudioManager>().Play(playEnum);
    }



    #endregion
}
