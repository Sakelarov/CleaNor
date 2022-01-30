using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private Animator anim;
    public GameObject trap;
    private float speed = 5f;
    private bool canMove = true;
    private bool rotationStarted = false;

    private Vector3 deathMovement;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (canMove)
        {
            Vector3 moveDir = (trap.transform.position - transform.position).normalized;
            transform.position += moveDir * speed * Time.deltaTime;
        }
        else
        {
            Vector3 moveDir = (deathMovement - transform.position).normalized;
            transform.position += moveDir * 3*speed * Time.deltaTime;

            StartCoroutine("Rotate");
        }

    }

    private IEnumerator Rotate()
    {
        for (int i = 1; i <= 10; i++)
        {
            yield return new WaitForSeconds(0.05f);
            //transform.rotation = Quaternion.Euler(0, 0, -18 * i);
            transform.Rotate(0, 0, -0.06f);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("trap"))
        {
            deathMovement = new Vector3(transform.position.x + 0.1f, transform.position.y + 0.2f, transform.position.z);
            canMove = false;
            other.transform.SetParent(this.transform);
            other.GetComponent<CircleCollider2D>().enabled = false;
            anim.SetBool("isDead", true);
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
