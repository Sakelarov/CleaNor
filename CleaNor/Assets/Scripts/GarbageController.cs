using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GarbageController : MonoBehaviour
{
    private RectTransform spawnArea;

    private Vector2 landingArea;
    private Vector2 startingPosition;

    private float distance;
    private float distanceFlown;

    private float speed = 5f;
    private bool isLandingAreaChosen = false;
    void Start()
    {
        spawnArea = GameObject.Find("SpawnArea").GetComponent<RectTransform>();
        ChooseLandingArea();
    }


    void Update()
    {
        if (isLandingAreaChosen) Fly();
        Splash();
    }

    private void ChooseLandingArea()
    {
        var x = Random.Range(spawnArea.position.x - spawnArea.rect.width / 2, spawnArea.position.x + spawnArea.rect.width / 2);
        var y = Random.Range(spawnArea.position.y - spawnArea.rect.height / 2, spawnArea.position.y + spawnArea.rect.height / 2);
        landingArea = new Vector2(x, y);

        distance = Vector2.Distance(transform.position, landingArea);
        startingPosition = transform.position;
        isLandingAreaChosen = true;
    }

    private void Fly()
    {
        transform.position = Vector3.MoveTowards(transform.position, landingArea, Time.deltaTime * speed);
        distanceFlown = Vector2.Distance(transform.position, startingPosition);

        if (distanceFlown < distance / 2) transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime, transform.localScale.y + Time.deltaTime, 1);
        else transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime, transform.localScale.y - Time.deltaTime, 1);
    }

    private void Splash()
    {
        //Physics.CheckSphere(landingArea, 0.1f,) //add layermask
    }
}
