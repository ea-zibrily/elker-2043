using System;
using UnityEngine;

public class EleMainMenuEventHandler : MonoBehaviour
{
    public void FootstepMainMenuSound() => FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Ele);
}