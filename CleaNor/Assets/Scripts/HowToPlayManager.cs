using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class HowToPlayManager : MonoBehaviour
{
    public Button backToMain;



    // Start is called before the first frame update
    void Start()
    {
        backToMain = GameObject.Find("BackToMainButton").GetComponent<Button>();
        backToMain.onClick.AddListener(GoToMain);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void GoToMain()
    {
        SoundManager.Instance.VolumeZero("music");
        SceneManager.LoadScene("MainMenu");
    }


















}
