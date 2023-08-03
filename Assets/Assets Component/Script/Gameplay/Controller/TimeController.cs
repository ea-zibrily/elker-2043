using System;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    #region Variable
    
    [Header("Timer Component")]
    [SerializeField] private float currentTime;
    [SerializeField] private bool isTimerStart;
    [SerializeField] private TextMeshProUGUI timerTextUI;
    
    [Header("Reference")]
    private EleDetector eleDetector;
    private GameEventHandler gameEventHandler;

    #endregion

    #region MonoBehaviour Callback

    private void Awake()
    {
        gameEventHandler = GameObject.Find("GameEvent").GetComponent<GameEventHandler>();
        eleDetector = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<EleDetector>();
    }

    private void Start()
    {
        isTimerStart = true;
    }

    private void Update()
    {
        if (eleDetector.IsPlayerCatch)
        {
            return;
        }
        
        TimerController();
    }

    #endregion

    #region Tsukuyomi Callbacks

    private void TimerController()
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

    #endregion
    
}