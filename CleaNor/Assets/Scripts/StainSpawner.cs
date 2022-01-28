using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] stains;
    void Start()
    {
        StartCoroutine("SpawnStains");
    }

    private IEnumerator SpawnStains()
    {
        for (int i = 0; i < 15; i++)
        {
            float seconds = Random.Range(3, 5);
            yield return new WaitForSeconds(seconds);
            int randomStain = Random.Range(0, stains.Length);
            var stain = Instantiate(stains[randomStain], this.transform);

            var x = Random.Range(-8, 8);
            var y = Random.Range(-4, 4);
            stain.transform.position = new Vector2(x, y);
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
}

    
