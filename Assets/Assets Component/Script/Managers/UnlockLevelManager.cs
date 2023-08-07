using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UnlockLevelManager : MonoBehaviour
{
    #region Variable
    
    [Header("Main Component")]
    private int numberOfUnlockedLevel;
    [field: SerializeField] public int TargetUnlockedLevel {get; private set;}
    public event Action OnLevelUnlocked;

    #endregion
    
    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        OnLevelUnlocked += UnlockLevel;
    }

    private void OnDisable()
    {
        OnLevelUnlocked -= UnlockLevel;
    }

    #endregion

    #region Tsukuyomi Callbacks
    
    public void LevelUnlockEvent() => OnLevelUnlocked?.Invoke();
    private void UnlockLevel()
    {
        numberOfUnlockedLevel = PlayerPrefs.GetInt(UnlockLevelController.LevelUnlockKey);
        if (numberOfUnlockedLevel < TargetUnlockedLevel)
        {
            numberOfUnlockedLevel++;
            PlayerPrefs.SetInt(UnlockLevelController.LevelUnlockKey, numberOfUnlockedLevel);
        }
    }

    #endregion
}