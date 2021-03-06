using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                var inst = FindObjectOfType<UIManager>();
                instance = inst;
            }

            return instance;
        }
    }

    public Text pointsText;
    public Button pauseButton;
    public Button mainMenuButton;
    public AudioSource pauseSound;
    public AudioSource playMusic;
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
            //pauseButton.GetComponentInChildren<Text>().text = "II";
            pauseSound.Play();

            //if (playMusic.isPlaying)
            //{
            //    playMusic.mute = true;
            //}
        }
        else if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            //pauseButton.GetComponentInChildren<Text>().text = "Pause";
            pauseSound.Play();

            //if (playMusic.isPlaying)
            //{
            //    playMusic.mute = false;
            //}
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Start is called before the first frame update
    void Start()
    {
        //if (GameObject.Find("MusicGO"))
        //{
        //    playMusic = GameObject.Find("MusicGO").GetComponent<AudioSource>();
        //}

        //pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        //mainMenuButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
        pauseSound = GameObject.Find("PauseSoundGO").GetComponent<AudioSource>();

        //pauseButton.onClick.AddListener(Pause);
        mainMenuButton.onClick.AddListener(MainMenu);
        pointsText.text = "Score: 0";
        StartCoroutine(UpdatePoints());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
