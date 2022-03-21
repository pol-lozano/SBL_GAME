using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text text;
    private float time;

    [SerializeField] private GameObject organParent;
    [SerializeField] private Organ[] organs;
    [SerializeField] private GameObject organTemplate;
    [SerializeField] private GameObject quizPanel;

    [SerializeField] private Organ currentOrgan;

    private int questionsCompleted;
    private int questionsLeft;

    public Text questionsLeftText;
    public Text questionText;
    public Text option1Text;
    public Text option2Text;
    public Text option3Text;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Organ o in organs)
        {
            GameObject temp = Instantiate(organTemplate);
            temp.GetComponent<Image>().sprite = o.sprite;
            temp.name = o.name;
            RectTransform r = temp.GetComponent<RectTransform>();
            r.SetParent(organParent.transform, false);
            r.anchoredPosition = new Vector3(Random.Range(-300,300),Random.Range(-50,25));
            r.localScale = Vector3.one;
            temp.GetComponent<OrganHolder>().organData = o;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        UpdateUI();
    }

    void UpdateUI()
    {
        float minutes = Mathf.FloorToInt(time/60);
        float seconds = Mathf.FloorToInt(time % 60);

        text.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        questionsLeftText.text = string.Format("Time: {0:00}:{1:00}, Questions Left: {2}/{3}", minutes, seconds, questionsCompleted, questionsLeft);
    }

    public GameObject analyzer;
    public void ScanOrgan()
    {
        currentOrgan = analyzer.GetComponent<ItemSlot>().currentOrgan;
        if (currentOrgan != null && currentOrgan.analyzed != true)
        {
            questionsLeft = currentOrgan.questions.Length;
            questionsCompleted = 0;

            SetQuestions(currentOrgan, 0);
        }
    }

    int correctAnswer = -1;
    private void SetQuestions(Organ o, int index)
    {
        //End quiz when out of questions
        if(index++ >= o.questions.Length)
        {
            currentOrgan.analyzed = true;
            EndQuiz();
            return;
        }

        var currentQuestion = o.questions[index];
        questionText.text = currentQuestion.question;

        correctAnswer = 1 + Mathf.FloorToInt(Random.value * 3);
        switch (correctAnswer)
        {
            case 1:
                option1Text.text = currentQuestion.answer;
                option2Text.text = currentQuestion.wrongAnswers[0];
                option3Text.text = currentQuestion.wrongAnswers[1];
                break;
            case 2:
                option1Text.text = currentQuestion.wrongAnswers[1];
                option2Text.text = currentQuestion.answer;
                option3Text.text = currentQuestion.wrongAnswers[0];
                break;
            case 3:
                option1Text.text = currentQuestion.wrongAnswers[0];
                option2Text.text = currentQuestion.wrongAnswers[1];
                option3Text.text = currentQuestion.answer;
                break;
        }

        quizPanel.SetActive(true);
    }

    public void CheckAnswer(int i)
    {
        if(i == correctAnswer)
        {
            //Blink Green
            Debug.Log("Correct Answer");
            questionsCompleted++;
        }
        else
        {
            //Blink Red
            Debug.Log("Wrong Answer");
        }
    }

    private void EndQuiz()
    {
        throw new System.NotImplementedException();
    }
}
