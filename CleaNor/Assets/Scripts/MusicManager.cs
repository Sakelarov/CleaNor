using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private GameObject[] other;
    private bool notFirst = false;
    int musicState;

    public void PlayMusic()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    private void Awake()
    {
        
            other = GameObject.FindGameObjectsWithTag("Music");

        foreach (GameObject oneOther in other)
        {
            if (oneOther.scene.buildIndex != 1)
            {
                notFirst = true;
            }
        }

        if (notFirst == true)
        {
            Destroy(gameObject);
        }
        else if (notFirst == false)
        {
            DontDestroyOnLoad(gameObject);
        }

        musicState = PlayerPrefs.GetInt("SoundOff");

        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayMusicRoutine());
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator PlayMusicRoutine()
    {
        while (true)
        {
            musicState = PlayerPrefs.GetInt("SoundOff");

            if (musicState == 0)
            {
                PlayMusic();
            }
            else if (musicState == 1)
            {
                StopMusic();
            }

            yield return new WaitForSeconds(0.000001f);
        }



    }




}
