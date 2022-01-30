using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private Animator anim;
    public GameObject trap;
    private float speed = 3f;
    private bool canMove = true;
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("trap"))
        {
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
