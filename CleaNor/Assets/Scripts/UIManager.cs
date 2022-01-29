using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Text pointsText;
    public Button pauseButton;
    public Button mainMenuButton;
    public bool isPaused = false;
    public int score;

    IEnumerator UpdatePoints()
    {
        while (true)
        {
            pointsText.text = "Score: " + score.ToString();
            yield return new WaitForSeconds(0.00000001f);
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseButton.image.color = Color.yellow;
            pauseButton.GetComponentInChildren<Text>().text = "II";
        }
        else if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseButton.image.color = Color.green;
            pauseButton.GetComponentInChildren<Text>().text = "Pause";
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        mainMenuButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
        pauseButton.image.color = Color.green;
        pauseButton.onClick.AddListener(Pause);
        mainMenuButton.onClick.AddListener(MainMenu);
        pointsText.text = "Score: 0";
        StartCoroutine(UpdatePoints());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
