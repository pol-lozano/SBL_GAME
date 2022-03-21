using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    [SerializeField]
    private int sceneToLoad;

    [SerializeField]
    private Image fadeImage;

    private float alpha;

    private void Start()
    {
        //sceneToload = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(FadeIn());
    }
    private IEnumerator FadeIn()
    {
        alpha = 1;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0);
        }
        fadeImage.gameObject.SetActive(false);
    }
    public void loadLevel()
    {
        fadeImage.gameObject.SetActive(true);
            StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        alpha = 0;

        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0);
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
