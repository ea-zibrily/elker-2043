using System;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    #region Variable

    [SerializeField] private float maxTime;
    private float currentTime;
    [SerializeField] private bool isTimerStart;
    [SerializeField] private TextMeshProUGUI timerTextUI;
    
    #endregion

    #region MonoBehaviour Callback

    private void Start()
    {
        currentTime = 0f;
        isTimerStart = true;
    }

    private void Update()
    {
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
        
        isTimerStart = false;
        if (currentTime <= maxTime)
        {
            currentTime -= Time.deltaTime;
            SetTimerTextUI(currentTime);
        }
        else
        {
            Debug.Log("Time's Up!");
            GameEvent.TimerEndEvent();
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
    }

    #endregion
    
}