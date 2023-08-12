using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    #region Variable
    
    [Header("Timer Component")]
    [SerializeField] private float currentTime;
    [SerializeField] private bool isTimerStart;
    [SerializeField] private TextMeshProUGUI timerTextUI;

    private float maxTime;
    private float endTime;
    
    [Header("Reference")]
    private EleDetector eleDetector;
    private ComputerHackController computerHackController;
    private GameEventHandler gameEventHandler;

    #endregion

    #region MonoBehaviour Callback

    private void Awake()
    {
        gameEventHandler = GameObject.Find("GameEvent").GetComponent<GameEventHandler>();
        eleDetector = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<EleDetector>();
        computerHackController = 
            GameObject.FindGameObjectWithTag("ComputerHack").GetComponent<ComputerHackController>();
    }

    private void Start()
    {
        isTimerStart = true;
        maxTime = currentTime;
    }

    private void Update()
    {
        if (eleDetector.IsPlayerCatch || computerHackController.IsComplete)
        {
            return;
        }
        
        StartTimer();
        SetWinningTime();
    }

    #endregion

    #region Tsukuyomi Callbacks
    
    private void StartTimer()
    {
        if (!isTimerStart)
        {
            return;
        }
        
        if (currentTime > 1.0f)
        {
            currentTime -= Time.deltaTime;
            SetTimerTextUI(currentTime);
        }
        else
        {
            GameEventHandler.CameraShakeEvent();
            gameEventHandler.TimerEndEvent();

            isTimerStart = false;
            currentTime = 0f;
        }
    }
    
    private void SetTimerTextUI(float time)
    {
        float minutes;
        float seconds;
        
        minutes = Mathf.FloorToInt(time / 60);
        seconds = Mathf.FloorToInt(time % 60);
        
        timerTextUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        if (time <= 10.0f)
        {
            timerTextUI.color = Color.red;
        }
    }
    
    private void SetWinningTime()
    {
        var currentLevel = PlayerPrefs.GetInt(UnlockLevelController.LevelUnlockKey) - 1;
        endTime = maxTime - currentTime;
        
        if (eleDetector.IsPlayerCatch)
        {
            PlayerPrefs.SetFloat(UnlockLevelController.LevelTimeWinKey[currentLevel], 0.00f);
        }
        else if (computerHackController.IsComplete)
        {
            PlayerPrefs.SetFloat(UnlockLevelController.LevelTimeWinKey[currentLevel], endTime);
        }
    }
    
    #endregion
    
}