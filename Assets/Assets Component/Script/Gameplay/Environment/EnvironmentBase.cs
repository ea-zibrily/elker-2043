using TMPro;
using UnityEngine;

public class EnvironmentBase : MonoBehaviour
{
    #region Variable

    [Header("Base Component")]
    public string buttonAlertText;
    public TextMeshProUGUI buttonAlertTextUI;
    public bool IsPlayerInside { get; private set; }


    #endregion

    #region Tsukuyomi Callbacks

    public void EnvironmentInitialize()
    {
        IsPlayerInside = false;
        buttonAlertTextUI.text = buttonAlertText;
        buttonAlertTextUI.gameObject.SetActive(false);
    }

    #endregion
    
    #region Collider Callbacks

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            buttonAlertTextUI.gameObject.SetActive(true);
            IsPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            buttonAlertTextUI.gameObject.SetActive(false);
            IsPlayerInside = false;
        }
    }

    #endregion
}