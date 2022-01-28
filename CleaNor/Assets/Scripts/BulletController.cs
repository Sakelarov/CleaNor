using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private int rotation;
    private float speed = 6;
    private bool isDirectionSet = false;
    private bool isSet = false;

    void Update()
    {
        if(isSet)Shoot();
    }


    private void Shoot()
    {
        if (!isDirectionSet)
        {
            transform.Rotate(new Vector3(0, 0, rotation));
            isDirectionSet = true;
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void SetupBullet(int bullet, Vector3 position)
    {
        transform.position = position;
        switch (bullet)
        {
            case 1:rotation = -45;
                break;
            case 2:rotation = -90;
                break;
            case 3:rotation = 45;
                break;
            case 4:rotation = 90;
                break;
            case 5:rotation = 135;
                break;
            case 6:rotation = 180;
                break;
            case 7:rotation = 225;
                break;
        }
        isSet = true;
    }
    
}
