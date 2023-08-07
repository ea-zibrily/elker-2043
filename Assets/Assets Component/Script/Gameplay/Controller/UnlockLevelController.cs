using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UnlockLevelController : MonoBehaviour
{
    #region Variable

    [Header("Main Component")]
    [SerializeField] private Button[] levelButton;
    [SerializeField] private TextMeshProUGUI[] levelTimeWinTextUI;
    private int numberOfUnlockedLevel;
    public static string LevelUnlockKey {get; private set;}
    public static string[] LevelTimeWinKey {get; private set;}

    #endregion
    
    #region MonoBehaviour Callbacks

    private void Start()
    {
        LevelUnlockKey = "LevelUnlock";
        LevelTimeWinKey = new string[]
        {
            "Level1TimeWin",
            "Level2TimeWin",
            "Level3TimeWin",
        };
        
        InitializeUnlockPrefs();
        InitializeTimeWinPrefs();
        DeactivateLevel();
    }
    
    private void Update()
    {
        numberOfUnlockedLevel = PlayerPrefs.GetInt(LevelUnlockKey);
        ActivateLevel();
    }
    
    #endregion

    #region Tsukuyomi Callbacks

    private void InitializeUnlockPrefs()
    {
        if (!PlayerPrefs.HasKey(LevelUnlockKey))
        {
            PlayerPrefs.SetInt(LevelUnlockKey, 1);
        }
    }
    
    private void InitializeTimeWinPrefs()
    {
        for (int i = 0; i < LevelTimeWinKey.Length; i++)
        {
            if (!PlayerPrefs.HasKey(LevelTimeWinKey[i]))
            {
                PlayerPrefs.SetFloat(LevelTimeWinKey[i], 0f);
            }
        }
    }
    
    private void ActivateLevel()
    {
        for (int i = 0; i < numberOfUnlockedLevel; i++)
        {
            levelButton[i].interactable = true;
            levelTimeWinTextUI[i].text = PlayerPrefs.GetFloat(LevelTimeWinKey[i]).ToString("00.0") + " sec";
            levelTimeWinTextUI[i].alpha = 1f;
        }
    }
    
    private void DeactivateLevel()
    {
        for (int i = 0; i < numberOfUnlockedLevel; i++)
        {
            levelButton[i].interactable = false;
            levelTimeWinTextUI[i].text = "00.0 sec";
            levelTimeWinTextUI[i].alpha = 0.5f;
        }
    }

    #endregion
    
}
