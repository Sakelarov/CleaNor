using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StainSpawner : MonoBehaviour
{
    private static StainSpawner instance;

    public static StainSpawner Instance
    {
        get
        {
            if (instance == null)
            {
                var inst = FindObjectOfType<StainSpawner>();
                instance = inst;
            }

            return instance;
        }
    }

    private List<SpriteRenderer> stainCollection = new List<SpriteRenderer>();
    [SerializeField] private Slider progressBar;
    [SerializeField] private Text progressValue;
    [SerializeField] private GameObject gameoverText;
    [SerializeField] private GameObject levelCompletedText;

    [SerializeField] private GameObject topBorder;
    [SerializeField] private GameObject bottomBorder;
    [SerializeField] private GameObject spawnarea2;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject levelChangeTrigger;

    [SerializeField] private GameObject[] stains;
    [SerializeField] private GameObject shoes;
    [SerializeField] private GameObject trap;

    [SerializeField] private RectTransform spawnArea;

    private bool isCompleted = false;
    public bool isGameRunning = true;

    private IEnumerator spawnStains;
    private IEnumerator spawnShoes;
    private IEnumerator spawnTraps;
    private int level = 1;

    private List<GameObject> currentTraps = new List<GameObject>();
    
    void Start()
    {
        spawnStains = SpawnStains();
        spawnShoes = SpawnShoes();
        spawnTraps = SpawnTraps();

        StartCoroutine(spawnStains);
        StartCoroutine(spawnShoes);
        StartCoroutine(spawnTraps);
    }

    public void Update()
    {
        CalculateProgress();
    }

    private IEnumerator SpawnStains()
    {
        for (int i = 0; i < level + 2; i++)
        {
            int randomStain = Random.Range(0, stains.Length);
            var stain = Instantiate(stains[randomStain], this.transform);
            stain.transform.position = GetPosition();
            stainCollection.Add(stain.GetComponent<SpriteRenderer>());
        }
        for (int i = 0; i < 10 + level; i++)
        {
            float seconds = Random.Range(8, 13);
            yield return new WaitForSeconds(seconds);
            if (isGameRunning)
            {
                int randomStain = Random.Range(0, stains.Length);
                var stain = Instantiate(stains[randomStain], this.transform);
                stain.transform.position = GetPosition();
                stainCollection.Add(stain.GetComponent<SpriteRenderer>());
            }
        }
    }

    public StainController SpawnStainInPosition(Vector3 position)
    {
        int randomStain = Random.Range(0, stains.Length);
        var stain = Instantiate(stains[randomStain], this.transform);
        stain.transform.position = position;

        stainCollection.Add(stain.GetComponent<SpriteRenderer>());

        var stainController = stain.GetComponent<StainController>();
        stainController.alpha = 0;

        return stainController;
    }

    private IEnumerator SpawnShoes()
    {
        yield return new WaitForSeconds(8);
        var shoesBuff = Instantiate(shoes);
        shoesBuff.transform.position = GetPosition();
        yield return new WaitUntil(() => shoesBuff == null);
        yield return SpawnShoes();
    }

    private IEnumerator SpawnTraps()
    {
        yield return new WaitForSeconds(8);
        var t = Instantiate(trap);
        t.transform.position = GetPosition();
        currentTraps.Add(t);
        if (currentTraps.Count < 3)
        {
            yield return SpawnTraps();
        }
    }

    public void SpawnTrap()
    {
        StartCoroutine("SpawnTraps");
    }

    private Vector2 GetPosition()
    {
        var x = Random.Range(spawnArea.position.x - spawnArea.rect.width / 2, spawnArea.position.x + spawnArea.rect.width / 2);
        var y = Random.Range(spawnArea.position.y - spawnArea.rect.height / 2, spawnArea.position.y + spawnArea.rect.height / 2);
        return new Vector2(x, y);
    }

    public void AddStainToCollection(GameObject stain)
    {
        stainCollection.Add(stain.GetComponent<SpriteRenderer>());
    }

    public void RemoveStainFromCollection(SpriteRenderer stain)
    {
        stainCollection.Remove(stain);
    }

    private void CalculateProgress()
    {
        float progress = 0;
        foreach (var stain in stainCollection)
        {
            progress += stain.color.a;
        }

        progressBar.value = progress;
        float value = progress * 100 / 20;
        progressValue.text = $"{value:f1}%";

        if (progress <= 0.1f)
        {
            CompleteLevel();
        }
        else if (progress >= 20)
        {
            // you are fired
        }
    }

    private void CompleteLevel()
    {
        if (!isCompleted)
        {
            progressValue.text = "0%";
            levelCompletedText.SetActive(true);
            isGameRunning = false;
            arrow.SetActive(true);
            topBorder.SetActive(false);
            levelChangeTrigger.SetActive(true);
            foreach (var garbageController in GameObject.FindObjectsOfType<GarbageController>())
            {
                Destroy(garbageController.gameObject);
            }
            foreach (var stain in GameObject.FindObjectsOfType<StainController>())
            {
                Destroy(stain.gameObject);
                stainCollection.Remove(stain.GetComponent<SpriteRenderer>());
            }
            StopCoroutine(spawnStains);
            StopCoroutine(spawnShoes);
            StopCoroutine(spawnTraps);

            isCompleted = true;
        }
    }

    public void StartNextLevel()
    {
        topBorder.SetActive(true);
        levelCompletedText.SetActive(false);
        arrow.SetActive(false);
        isGameRunning = true;
        level++;

        if (level == 2)
        {
            bottomBorder.transform.position = new Vector3(bottomBorder.transform.position.x,
                bottomBorder.transform.position.y - 2, bottomBorder.transform.position.z);
            spawnArea.transform.position = spawnarea2.transform.position;
        }

        StartCoroutine(spawnStains);
        StartCoroutine(spawnShoes);
        StartCoroutine(spawnTraps);
    }

    public void HideText() // called from animation of levelCompletedText
    {
        topBorder.SetActive(false);
        levelChangeTrigger.SetActive(true);
        arrow.SetActive(true);
    }

    private void GameOver()
    {
        gameoverText.SetActive(true);
        isGameRunning = false;
    }
}

    
