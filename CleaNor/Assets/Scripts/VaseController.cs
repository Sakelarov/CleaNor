using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseController : MonoBehaviour
{
    [SerializeField] private GameObject stainPrefab;
    private Animator anim;


    public int point = 5;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void DestroyVase()
    {
        anim.SetBool("isDestroyed", true);
        Invoke("Destroy", 2);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            if (GameObject.Find("UIManagerGO").GetComponent<UIManager>().score >= 5)
            {
                GameObject.Find("UIManagerGO").GetComponent<UIManager>().score -= point;
            }
            DestroyVase();
            var stain = Instantiate(stainPrefab);
            stain.transform.position = transform.position;
            StainSpawner.Instance.AddStainToCollection(stain);
            other.GetComponent<BulletController>().DestructBullet();
        }
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
