using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class QuizController : MonoBehaviour
{
    #region Variable
    
    [Header("Quiz Component")]
    [SerializeField] private List<QuestionAndAnswer> questionAndAnswers;
    [SerializeField] private GameObject[] optionsPanel;
    private int currentQuestion;
    private int quizScore;
    private int questionTotal;
    private int tempAnswerInput;
    
    private Coroutine checkAnswer;

    [SerializeField] private TextMeshProUGUI questionTextUI;
    [SerializeField] private TextMeshProUGUI scoreUI;
    
   [Header("Reference")]
   private QuizzEventHandler quizzEventHandler;
   
    #endregion
    
    #region MonoBehaviour Callbacks

    private void Awake()
    {
        quizzEventHandler = GetComponent<QuizzEventHandler>();
    }

    private void Start()
    {
        questionTotal = questionAndAnswers.Count;
        GenerateQuestion();
    }
    
    private void Update()
    {
        scoreUI.text = quizScore.ToString() + "/" + questionTotal;
    }

    #endregion


    #region Tsukuyomi Callbacks

    private void GenerateQuestion()
    {
        if(questionAndAnswers.Count > 0)
        {
            currentQuestion = Random.Range(0, questionAndAnswers.Count);
            questionTextUI.text = questionAndAnswers[currentQuestion].question;
            SetAnswer();
        }
        else
        {
            if (quizScore < questionTotal)
            {
                quizzEventHandler.HackFailedEvent();
                Debug.Log("Hack Failed");
            }
            else
            {
                quizzEventHandler.HackSuccessEvent();
                Debug.Log("Hack Success");
            }
        }
    }

    private void SetAnswer()
    {
        for (int i = 0; i < optionsPanel.Length; i++)
        {
            optionsPanel[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text 
                = questionAndAnswers[currentQuestion].answer[i];
        }
    }

    public void InputAnswer(int answer)
    {
        tempAnswerInput = answer - 1;
        
        checkAnswer = tempAnswerInput == questionAndAnswers[currentQuestion].correctAnswer ? 
            StartCoroutine(CorrectAnswer()) : StartCoroutine(WrongAnswer());
    }

    private IEnumerator CorrectAnswer()
    {
        quizScore++;
        // SetTextButtonGreen();
        SetTextButtonColor();
        
        yield return new WaitForSeconds(0.5f);
        questionAndAnswers.RemoveAt(currentQuestion);
        GenerateQuestion();
    }

    private IEnumerator WrongAnswer()
    {
        // SetTextButtonRed();
        SetTextButtonColor();
        
        yield return new WaitForSeconds(0.5f);
        questionAndAnswers.RemoveAt(currentQuestion);
        GenerateQuestion();
    }

    private void SetTextButtonColor()
    {
        for (int i = 0; i < optionsPanel.Length; i++)
        {
            var buttonTextUI = optionsPanel[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            
            buttonTextUI.color = i == questionAndAnswers[currentQuestion].correctAnswer ? 
                new Color(60.0f, 207.0f, 78.0f, 1f) : Color.red;
        }
    }
    
    private void SetTextButtonGreen()
    {
        for (int i = 0; i < optionsPanel.Length; i++)
        {
            var buttonTextUI = optionsPanel[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            if (i == questionAndAnswers[currentQuestion].correctAnswer)
            {
                buttonTextUI.color = new Color(60.0f, 207.0f, 78.0f, 1f);
            }
        }
    }

    private void SetTextButtonRed()
    {
        for (int i = 0; i < optionsPanel.Length; i++)
        {
            var buttonTextUI = optionsPanel[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            if (i == tempAnswerInput)
            {
                buttonTextUI.color = Color.red;
            }
        }
    }

    #endregion

}
