using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseController : MonoBehaviour
{
    [SerializeField] private GameObject stainPrefab;
    private Animator anim;

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
            DestroyVase();
            var stain = Instantiate(stainPrefab);
            stain.transform.position = transform.position;
            Destroy(other.gameObject);
        }
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
