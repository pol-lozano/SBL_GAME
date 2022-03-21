using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Organ", order = 1)]
public class Organ : ScriptableObject
{
    public string name;
    public int id;
    public bool analyzed = false; //Are all questions for it answered or not

    public Sprite sprite;
    public QuizQuestion[] questions;
}

[System.Serializable]
public class QuizQuestion
{
    public string question;
    public string answer;
    public string[] wrongAnswers;
}
