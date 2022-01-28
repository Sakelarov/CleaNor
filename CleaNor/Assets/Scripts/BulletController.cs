using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 direction;
    private float speed = 6;
    private bool isSet = false;

    void Update()
    {
        if(isSet) transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetupBullet(int bullet, Vector3 position)
    {
        transform.position = position;
        switch (bullet)
        {
            case 1: direction = new Vector3(1, 0.5f);
                break;
            case 2: direction = new Vector3(1, -0.5f);
                break;
            case 3: direction = new Vector3(0, -1f);
                break;
            case 4: direction = new Vector3(-1, -0.5f);
                break;
            case 5: direction = new Vector3(-1, 0.5f);
                break;
        }
        isSet = true;
    }
    
}
