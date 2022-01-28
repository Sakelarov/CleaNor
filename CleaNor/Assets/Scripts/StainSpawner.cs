using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] stains;
    [SerializeField] private GameObject shoes;
    [SerializeField] private GameObject trap;

    [SerializeField] private RectTransform spawnArea;

    private List<GameObject> currentTraps = new List<GameObject>();
    void Start()
    {
        StartCoroutine("SpawnStains");
        StartCoroutine("SpawnShoes");
        StartCoroutine("SpawnTraps");
    }

    private IEnumerator SpawnStains()
    {
        for (int i = 0; i < 15; i++)
        {
            float seconds = Random.Range(5, 7);
            yield return new WaitForSeconds(seconds);
            int randomStain = Random.Range(0, stains.Length);
            Instantiate(stains[randomStain], this.transform).transform.position = GetPosition();
        }
    }

    public StainController SpawnStainInPosition(Vector3 position)
    {
        int randomStain = Random.Range(0, stains.Length);
        var stain = Instantiate(stains[randomStain], this.transform);
        stain.transform.position = position;
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
        if (currentTraps.Count < 5)
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
}

    
