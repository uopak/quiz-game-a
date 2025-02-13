using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct QuizData
{
    public string question;
    public string description;
    public int type;
    public int answers;
    public string[] examples;
    public string[] options;
}

public class QuizCardController : MonoBehaviour
{
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Button[] buttons;
    
    
    public delegate void QuizCardDelegate(int cardIndex);
    private event QuizCardDelegate onCompleted;

    public void SetQuiz(QuizData data, QuizCardDelegate onCompleted)
    {
        // 1. 퀴즈 
        // 2. 설명
        // 3. 타입 (O: OX퀴즈, 1: 보기 3개 객관식
        // 4. 정답
        // 5. 보기 (1, 2, 3)
        
        questionText.text = 
        
        for (int i)
    }
}
