using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainController : MonoBehaviour
{
    private SpriteRenderer rend;
    private PlayerController player;
    public float alpha = 1;

    private AudioSource sound;

    public int point = 1;

    private IEnumerator cleaning;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        sound = SoundManager.Instance.Play("clean");
        sound.mute = true;
    }

    
    void Update()
    {
        
    }

    private IEnumerator GetCleaned()
    {
        while (alpha > 0)
        {
            yield return new WaitForSeconds(0.2f);
            alpha -= player.cleaningSpeed * 0.1f;
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, alpha);
            player.transform.localScale = new Vector3(-player.transform.localScale.x, player.transform.localScale.y,
                player.transform.localScale.z);
            var back = player.transform.GetChild(0);
            back.localScale = new Vector3(-back.localScale.x, back.localScale.y, back.localScale.z);

            if (alpha < 0)
            {
                GameObject.Find("UIManagerGO").GetComponent<UIManager>().score += point;
                StainSpawner.Instance.RemoveStainFromCollection(rend);
                Destroy(this.gameObject);
            }
        }
        
    }

    private IEnumerator GetDirtier()
    {
        while (alpha < 1)
        {
            yield return new WaitForSeconds(0.2f);
            alpha += 0.1f;
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, alpha);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("cleaner"))
        {
            cleaning = GetCleaned();
            StartCoroutine(cleaning);
            sound = SoundManager.Instance.Play("clean");
            sound.mute = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("cleaner"))
        {
            StopCoroutine(cleaning);
            sound.mute = true;
        }
    }

    public void GetDirty(float value)
    {
        alpha += value;
        if (alpha > 1)
        {
            alpha = 1;
        }
        if (alpha <= 1)
        {
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, alpha);
        }
    }
}
