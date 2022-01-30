using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private GameObject mouse;
    void Start()
    {
        Invoke("SpawnMouse", 3);
    }

    private void SpawnMouse()
    {
        var m = Instantiate(mouse);
        m.GetComponent<MouseController>().trap = this.gameObject;
    }
}
