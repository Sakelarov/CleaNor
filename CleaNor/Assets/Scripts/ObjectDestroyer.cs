using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("stain"))
        {
            StainSpawner.Instance.RemoveStainFromCollection(other.GetComponent<SpriteRenderer>());
        }
        else if (other.CompareTag("shoes"))
        {
            StainSpawner.Instance.currentShoes = null;
        }
        else if (other.CompareTag("trap"))
        {
            StainSpawner.Instance.currentTraps.Remove(other.gameObject);
        }
        else if (other.CompareTag("dontDestroy"))
        {
            return;
        }
        Destroy(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("dontDestroy"))
        {
            return;
        }
        Destroy(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("stain"))
        {
            StainSpawner.Instance.RemoveStainFromCollection(other.GetComponent<SpriteRenderer>());
        }
        else if (other.CompareTag("shoes"))
        {
            StainSpawner.Instance.currentShoes = null;
        }
        else if (other.CompareTag("trap"))
        {
            StainSpawner.Instance.currentTraps.Remove(other.gameObject);
        }
        else if (other.CompareTag("dontDestroy"))
        {
            return;
        }
        Destroy(other.gameObject);
    }
}
