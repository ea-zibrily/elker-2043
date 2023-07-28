using System;
using System.Collections;
using UnityEngine;

public class QuizzManager : MonoBehaviour
{
    #region Variable

    [Header("Computer UI Component")]
    [SerializeField] private GameObject[] descriptionPanelUI;
    
    [Header("Reference")]
    private QuizzEventHandler quizController;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        quizController = GameObject.Find("QuizController").GetComponent<QuizzEventHandler>();
    }

    private void OnEnable()
    {
        quizController.OnHackSuccess += HackSuccess;
        quizController.OnHackFailed += HackFailed;
    }

    private void OnDisable()
    {
        quizController.OnHackSuccess -= HackSuccess;
        quizController.OnHackFailed -= HackFailed;
    }

    #endregion

    #region Tsukuyomi Callbacks

    private IEnumerator HackSuccess()
    {
        // Some logic here
        yield return new WaitForSeconds(1f);
    }
    
    private IEnumerator HackFailed()
    {
        // Some logic here
        yield return new WaitForSeconds(1f);
    }

    #endregion
    
}