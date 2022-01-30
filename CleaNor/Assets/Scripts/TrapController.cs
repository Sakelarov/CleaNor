using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private GameObject mouse;
    private Vector3 spawnPos;

    public GameObject currentMouse = null;
    void Start()
    {
        spawnPos = GameObject.Find("MouseSpawnPosition").transform.position;
        Invoke("SpawnMouse", 5);
    }

    private void SpawnMouse()
    {
        currentMouse = Instantiate(mouse);
        currentMouse.transform.position = spawnPos;
        currentMouse.GetComponent<MouseController>().trap = this.gameObject;
    }
}
