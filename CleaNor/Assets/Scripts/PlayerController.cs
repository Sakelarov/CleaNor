using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed= 10;

    private float horizontalInput;
    private float verticalInput;

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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("garbage"))
        {
            if (other.gameObject.transform.localScale.x < 0.6f)
            {
                Destroy(other.gameObject);
            }
        }
    }

}
