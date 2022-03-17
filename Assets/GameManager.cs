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

    // Start is called before the first frame update
    void Start()
    {
        foreach(Organ o in organs)
        {
            GameObject temp = Instantiate(organTemplate);
            temp.GetComponent<Image>().sprite = o.sprite;
            temp.name = o.name;
            RectTransform r = temp.GetComponent<RectTransform>();
            r.SetParent(organParent.transform,false);
            r.anchoredPosition = new Vector3(Random.Range(-300,300),Random.Range(-50,25));
            r.localScale = Vector3.one;
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
    }
}
