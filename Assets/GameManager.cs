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
        var organ = analyzer.GetComponent<ItemSlot>().currentOrgan;
        if (organ != null)
        {
            questionsLeft = organ.questions.Length;
            questionsCompleted = 0;

            SetQuestions(organ, 0);
        }
    }

    private void SetQuestions(Organ o, int index)
    {
        //End quiz when out of questions
        if(index++ >= o.questions.Length)
        {
            EndQuiz();
            return;
        }

        var currentQuestion = o.questions[Random.Range(0, o.questions.Length-1)];

        questionText.text = currentQuestion.question;
        option1Text.text = currentQuestion.answer;
        option2Text.text = currentQuestion.wrongAnswers[0];
        option3Text.text = currentQuestion.wrongAnswers[1];

        quizPanel.SetActive(true);
        questionsCompleted++;
    }

    private void EndQuiz()
    {
        throw new System.NotImplementedException();
    }
}
