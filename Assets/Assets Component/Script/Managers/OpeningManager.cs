using System;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class OpeningManager : MonoBehaviour
{
    #region Variable
    
    [Header("Cutscene Video Component")] 
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Animator splashPanelAnimator;

    [Header("Timer Component")] 
    [SerializeField] private float targetTime;
    [SerializeField] private string skipNotificationText;
    [SerializeField] private TextMeshProUGUI skipNotificationTextUI;
    private float currentTime;
    private bool isTimerStart;

    #endregion
    
    #region MonoBehaviour Callbacks

    private void OnEnable()
    {
        SplashUIEventHandler.OnSplashUIDoneEvent += PlayCutsceneVideo;
        videoPlayer.loopPointReached += MoveScene;
    }
    
    private void OnDisable()
    {
        SplashUIEventHandler.OnSplashUIDoneEvent -= PlayCutsceneVideo;
        videoPlayer.loopPointReached -= MoveScene;
    }
    
    private void Start()
    {
        currentTime = 0;
        isTimerStart = false;
        
        skipNotificationTextUI.text = skipNotificationText;
        skipNotificationTextUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        VideoTimer();
        
        if (Input.GetKeyDown(KeyCode.Space) && skipNotificationTextUI.IsActive())
        {
            MainMenuSceneManager.Instance.SceneMoveController(2);
        }
    }

    #endregion
    
    #region Tsukuyomi Callbacks

    private void VideoTimer()
    {
        if (!isTimerStart)
        {
            return;
        }
        
        if (currentTime < 60.0f)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            isTimerStart = false;
            currentTime = targetTime;
            skipNotificationTextUI.gameObject.SetActive(true);
        }
    }
    
    private void PlayCutsceneVideo()
    {
        isTimerStart = true;
        videoPlayer.Play();
    }
    
    private void MoveScene(VideoPlayer newVideoPlayer) => MainMenuSceneManager.Instance.SceneMoveController(2);
    
    #endregion
}