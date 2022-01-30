using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GarbageController : MonoBehaviour
{
    private RectTransform spawnArea;
    [SerializeField] private GameObject stain;
    [SerializeField] private GameObject landingSpot;
    private GameObject landingObj;

    private Vector2 landingArea;
    private Vector2 startingPosition;

    private float distance;
    private float distanceFlown;

    private float speed = 3f;
    private bool isLandingAreaChosen = false;
    private LayerMask layer;

    private CircleCollider2D coll;
    void Start()
    {
        spawnArea = GameObject.Find("SpawnArea").GetComponent<RectTransform>();
        layer = LayerMask.NameToLayer("Garbage");
        coll = GetComponent<CircleCollider2D>();
        coll.enabled = false;

        ChooseLandingArea();
    }

    private void OnDestroy()
    {
        Destroy(landingObj);
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
        landingObj = Instantiate(landingSpot);
        landingObj.transform.position = landingArea;

        distance = Vector2.Distance(transform.position, landingArea);
        startingPosition = transform.position;
        isLandingAreaChosen = true;
    }

    private void Fly()
    {
        transform.position = Vector3.MoveTowards(transform.position, landingArea, Time.deltaTime * speed);
        distanceFlown = Vector2.Distance(transform.position, startingPosition);

        if (distanceFlown < distance / 2)
        {
            transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime, transform.localScale.y + Time.deltaTime, 1);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime, transform.localScale.y - Time.deltaTime, 1);
            coll.enabled = true;
        }
    }

    private void Splash()
    {
        if (transform.localScale.x < 0.49f)
        {
            if (GameObject.Find("UIManagerGO").GetComponent<UIManager>().score >= 2)
            {
                GameObject.Find("UIManagerGO").GetComponent<UIManager>().score -= 2;
            }

            SoundManager.Instance.Play("garbage");
            var stainObj = Instantiate(stain);
            stainObj.transform.position = landingArea;
            StainSpawner.Instance.AddStainToCollection(stainObj);
            Destroy(this.gameObject);
        }
    }
}
