using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed= 5;
    public float cleaningSpeed = 1;

    private float horizontalInput;
    private float verticalInput;

    private GameObject trap;

    void Start()
    {
        
    }

    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector2.up * verticalInput * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "garbage":
                if (other.gameObject.transform.localScale.x < 0.6f)
                {
                    Destroy(other.gameObject);
                }
                break;
            case "shoes": StartCoroutine("IncreaseSpeed");
                Destroy(other.gameObject);
                break;
            case "bucket": StartCoroutine("IncreaseCleaningSpeed");
                other.gameObject.GetComponent<BucketController>().ResetBucket();
                break;
            case "trap": StartCoroutine("Trap");
                trap = other.gameObject;
                break;
        }
    }

    private IEnumerator IncreaseSpeed()
    {
        speed = 8;
        yield return new WaitForSeconds(7);
        speed = 7;
        yield return new WaitForSeconds(1);
        speed = 6;
        yield return new WaitForSeconds(1);
        speed = 5;
    }

    private IEnumerator Trap()
    {
        speed = 0;
        yield return new WaitForSeconds(4);
        speed = 5;
        Destroy(trap);
    }

    private IEnumerator IncreaseCleaningSpeed()
    {
        cleaningSpeed = 2;
        yield return new WaitForSeconds(8);
        cleaningSpeed = 1;
    }

}
