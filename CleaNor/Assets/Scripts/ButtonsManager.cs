using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public int soundOff;
    public Button playButton;
    public Button soundButton;
    public Button howToPlayButton;
    private AudioSource _audioSource;


    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SoundOff()
    {
        if (soundOff == 0)
        {
            PlayerPrefs.SetInt("SoundOff", 1);
            soundOff = PlayerPrefs.GetInt("SoundOff");
        }
        else if (soundOff == 1)
        {
            PlayerPrefs.SetInt("SoundOff", 0);
            soundOff = PlayerPrefs.GetInt("SoundOff");
        }
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    // Start is called before the first frame update
    void Start()
    {
        howToPlayButton = GameObject.Find("HowToPlayButton").GetComponent<Button>();
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        soundButton = GameObject.Find("SoundButton").GetComponent<Button>();
        howToPlayButton.onClick.AddListener(HowToPlay);
        playButton.onClick.AddListener(PlayGame);
        soundButton.onClick.AddListener(SoundOff);
        soundOff = PlayerPrefs.GetInt("SoundOff");
    }

    
}
