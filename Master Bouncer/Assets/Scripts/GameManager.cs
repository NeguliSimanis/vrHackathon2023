using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;
    int scoreIncrement = 10;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessCustomerEntry(bool isGoodCustomer = true)
    {
        if (isGoodCustomer)
        {
            score += scoreIncrement;
        }
        else
        {
            score -= scoreIncrement;
        }

        scoreText.text = "Score: " + score.ToString();

    }
}
