using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Playables;

public class MainMenuController : MonoBehaviour
{
    #region Variable
    
    [Header("Main Component")]
    [SerializeField] private string notificationText;
    [SerializeField] private float idleMaxTime;
    
    [Space]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject levelSelectionPanel;
    [SerializeField] private GameObject creditPanel;
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private TextMeshProUGUI notificationTextUI;
    
    [Space]
    [SerializeField] private PlayableDirector elePlayableDirector;
    
    private float idleTime;
    private bool isIdle;
    private bool isMainMenuOpen;
    
    [Header("BGM Component")]
    [SerializeField] private float decreaseVolumeSpeed;
    private float currentVolume;
    private bool isDecreaseVolume;
    
    [Header("Reference")]
    private Animator mainMenuAnimator;
    private Animator levelSelectionAnimator;
    
    #endregion

    #region MonoBehaviour Callbacks
    
    private void Awake()
    {
        mainMenuAnimator = mainMenuPanel.GetComponent<Animator>();
        levelSelectionAnimator = levelSelectionPanel.GetComponent<Animator>();
    }
    
    private void Start()
    {
        isMainMenuOpen = true;
        idleTime = 0f;
        isIdle = false;

        notificationTextUI.text = notificationText;
        notificationTextUI.gameObject.SetActive(false);
        
        creditPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        
        currentVolume = FindObjectOfType<AudioManager>().GetVolume(SoundEnum.BGM_MainMenu);
        isDecreaseVolume = false;
    }
    
    private void Update()
    {
        if (isDecreaseVolume)
        {
            DecreaseMusicVolume();
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && !isMainMenuOpen)
        {
            StartCoroutine(OpenMainMenuPanel());
        }

        if (!isMainMenuOpen)
        {
            IdleStateTimer();
        }

    }
    
    #endregion
    
    #region Tsukuyomi Callbacks
    
    private IEnumerator OpenLevelSelectionPanel()
    {
        FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Click);
        yield return new WaitForSeconds(0.3f);
        
        mainMenuAnimator.SetBool("IsOpen", false);
        yield return new WaitForSeconds(2.3f);

        if (levelSelectionPanel.activeSelf)
        {
            levelSelectionAnimator.SetBool("IsOpen", true);
        }
        else
        {
            levelSelectionPanel.SetActive(true);
        }
        isMainMenuOpen = false;
    }
    
    private IEnumerator OpenMainMenuPanel()
    {
        FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Click);
        yield return new WaitForSeconds(0.3f);
        
        levelSelectionAnimator.SetBool("IsOpen", false);
        yield return new WaitForSeconds(2.3f);
        
        mainMenuAnimator.SetBool("IsOpen", true);
        isMainMenuOpen = true;
    }
    
    private IEnumerator OpenLevelScene(int levelNum)
    {
        FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Click);
        isDecreaseVolume = true;
        MainMenuSceneManager.Instance.SetSceneNum(levelNum);
        levelSelectionAnimator.SetBool("IsOpen", false);
        yield return new WaitForSeconds(2f);
        
        elePlayableDirector.Play();
    }

    private void IdleStateTimer()
    {
        if (Input.anyKey || Input.GetAxis("Mouse X") != 0f || Input.GetAxis("Mouse Y") != 0f)
        {
            ResetIdleState();
            notificationTextUI.gameObject.SetActive(false);
        }
        else
        {
            idleTime += Time.deltaTime;
            if (idleTime >= idleMaxTime && !isIdle)
            {
                isIdle = true;
                notificationTextUI.gameObject.SetActive(true);
            }
        }
    }
    
    private void ResetIdleState()
    {
        idleTime = 0f;
        isIdle = false;
    }

    public void OpenLevelSelection() => StartCoroutine(OpenLevelSelectionPanel());
    public void OpenGameLevel(int levelNum) => StartCoroutine(OpenLevelScene(levelNum));

    public void OpenCredit()
    {
        FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Click);
        creditPanel.SetActive(true);
    }

    public void CloseCredit()
    {
        FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Click);
        creditPanel.SetActive(false);
    }
     
    public void OpenHowToPlay()
    {
        FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Click);
        howToPlayPanel.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        FindObjectOfType<AudioManager>().PlayAudio(SoundEnum.SFX_Click);
        howToPlayPanel.SetActive(false);
    }

    private void DecreaseMusicVolume()
    {
        if (currentVolume > 0.01f)
        {
            currentVolume -= decreaseVolumeSpeed * Time.deltaTime;
            FindObjectOfType<AudioManager>().SetVolume(SoundEnum.BGM_MainMenu, currentVolume);
        }
        else
        {
            isDecreaseVolume = false;
        }
    }
    
    #endregion
}