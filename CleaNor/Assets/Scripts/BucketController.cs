using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BucketController : MonoBehaviour
{
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    [SerializeField] private Image loader;

    private float loadspeed = 0.1f;
    private Color faded = new Color(1, 1, 1, 0.6f);

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void Update()
    {
        if (loader.gameObject.activeSelf)
        {
            loader.fillAmount += Time.deltaTime * loadspeed;
            if (loader.fillAmount >= 1)
            {
                loader.gameObject.SetActive(false);
                sprite.color = Color.white;
                coll.isTrigger = true;
            }
        }
    }

    public void ResetBucket()
    {
        loader.fillAmount = 0;
        loader.gameObject.SetActive(true);
        sprite.color = faded;
        coll.isTrigger = false;
    }
}
