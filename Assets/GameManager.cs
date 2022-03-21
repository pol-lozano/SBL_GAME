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
    [SerializeField] private GameObject endPanel;

    [SerializeField] private Organ currentOrgan;

    private int questionsCompleted;
    private int questionsLeft;

    private float bloodLeft = 100;

    static public Dictionary<Organ, bool> organAnalyzed = new Dictionary<Organ, bool>();
    static public int organsFixed = 0;

    public Text questionsLeftText;
    public Text questionText;
    public Text option1Text;
    public Text option2Text;
    public Text option3Text;

    public Text operationCompleteText;

    public AudioClip rightAnswer;
    public AudioClip wrongAnswer;


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

            organAnalyzed.Add(o, false);
        }
    }

    private bool gameEnded = false;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        bloodLeft -= Time.deltaTime/20;
        if (bloodLeft < 0) bloodLeft = 0;
        UpdateUI();
        if(bloodLeft <= 0 || organsFixed >= 11)
        {
            if (gameEnded == false)
            {
                gameEnded = true;
                EndGame();
            }
        }
    }

    void UpdateUI()
    {
        float minutes = Mathf.FloorToInt(time/60);
        float seconds = Mathf.FloorToInt(time%60);

        text.text = string.Format("Time: {0:00}:{1:00} Blood: {2:0.00}%", minutes, seconds, bloodLeft);
        questionsLeftText.text = string.Format("Time: {0:00}:{1:00} Questions Completed: {2}/{3} Blood: {4:0.00}%", minutes, seconds, questionsCompleted, questionsLeft, bloodLeft);
    }

    public GameObject analyzer;
    public void ScanOrgan()
    {
        currentOrgan = analyzer.GetComponent<ItemSlot>().currentOrgan;
        if (currentOrgan != null && organAnalyzed[currentOrgan] == false)
        {
            questionsLeft = currentOrgan.questions.Length;
            questionsCompleted = 0;
            index = 0;

            SetQuestions();
        }
    }

    int correctAnswer = -1;
    int index = 0;

    private void SetQuestions()
    {
        //End quiz when out of questions
        if(index >= currentOrgan.questions.Length)
        {
            EndQuiz();
            return;
        }

        var currentQuestion = currentOrgan.questions[index];
        questionText.text = currentQuestion.question;

        correctAnswer = Random.Range(1, 3);
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

        organParent.SetActive(false);
        quizPanel.SetActive(true);
    }

    public void CheckAnswer(int i)
    {
        if(i == correctAnswer)
        {
            AudioSource.PlayClipAtPoint(rightAnswer, Vector3.zero);
            Debug.Log("Correct Answer");
            questionsCompleted++;
            index++;
            SetQuestions();
        }
        else
        {
            AudioSource.PlayClipAtPoint(wrongAnswer, Vector3.zero);
            Debug.Log("Wrong Answer");
            bloodLeft -= 2.5f;
        }
    }

    private void EndQuiz()
    {
        Debug.Log("Quiz Ended");
        organAnalyzed[currentOrgan] = true;
        organParent.SetActive(true);
        quizPanel.SetActive(false);
    }

    private void EndGame()
    {
        float finalTime = time;
        float finalBlood = bloodLeft;
        operationCompleteText.text = "(Organs Fixed: " + organsFixed + " * Cash Per Organ: " + 10000 + " - Time penalty: (-10 per sec) " + 10 + finalTime % 60 + ") * Blood Percentage: " + finalBlood + " = " + (organsFixed * 10000 - 10 * (finalTime % 60)) * finalBlood;
        endPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        Application.LoadLevel(1);
    }

    public void ExitToMainMenu()
    {
        Application.LoadLevel(0);
    }

}
