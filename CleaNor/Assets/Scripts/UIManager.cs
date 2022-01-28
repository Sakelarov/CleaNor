using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Text pointsText;
    public int score;

    IEnumerator UpdatePoints()
    {
        while (true)
        {
            pointsText.text = "Score: " + score.ToString();
            yield return new WaitForSeconds(0.00000001f);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        pointsText.text = "Score: 0";
        StartCoroutine(UpdatePoints());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
