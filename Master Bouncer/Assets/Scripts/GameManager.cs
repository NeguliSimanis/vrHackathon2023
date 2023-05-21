using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    bool isGameOver = false;

    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;
    int scoreIncrement = 10;

    float gameDuration = 120f;
    float gameStartTime = 0f;

   int displaySeconds;
   int displayMinutes;
    AudioManager audioManager;

    /// <summary>
    /// Customers currently within level
    /// </summary>
    public List<Customer> customers = new List<Customer>();
    public GameObject customerPrefab;
    public List<CustomerPosition> customerPositions = new List<CustomerPosition>();

    #region PLAYER TASK
    string taskString1;
    string taskString2;
    string fullTaskString;
    string allowedFeature;
    string forbiddenFeature;


    [SerializeField] TextMeshProUGUI taskText;
    #endregion



    void Start()
    {
        audioManager = gameObject.GetComponent<AudioManager>();
        GeneratePlayerTask();
    }

    //flamingo,
    //elf,
    //rabbit,
    //hasTattoos,
    //hasWings,
    //panda,
    //hasBeard,
    //hasShoes,
    //hasFur
    private void GeneratePlayerTask()
    {
        string[] customerFeatures = System.Enum.GetNames(typeof(CustomerFeatures));
        GenerateRandomFeaturePair();

        if (!IsFeaturePairPossible())
        {
            Debug.Log("not allowed this combo " + allowedFeature + " to enter club. Unless they " + forbiddenFeature);
            for (int i = 0; i < 15; i++)
            {
                GenerateRandomFeaturePair();
                if (IsFeaturePairPossible())
                    break;
            }
            if (!IsFeaturePairPossible())
            {
                allowedFeature = "hasFur";
                forbiddenFeature = "panda";
            }
        }
        Debug.Log(allowedFeature + " . forb " + forbiddenFeature);

        taskText.text = "Only allow " + allowedFeature + " to enter club. Unless they " + forbiddenFeature;
    }

    private bool IsFeaturePairPossible()
    {
        if (allowedFeature == "elf" && forbiddenFeature == "beard")
            return false;
        if (allowedFeature == "beard" && forbiddenFeature == "elf")
            return false;
        if (allowedFeature == "rabbit" && forbiddenFeature == "hasFur")
            return false;
        if (forbiddenFeature == "rabbit" && allowedFeature == "hasShoes")
            return false;
        if (forbiddenFeature == "rabbit" && allowedFeature == "hasTattoos")
            return false;
        if (allowedFeature == "panda" && forbiddenFeature == "hasFur")
            return false;
        if (allowedFeature == "flamingo" && forbiddenFeature == "hasWings")
            return false;
        if (allowedFeature == "hasWings" && forbiddenFeature == "flamingo")
            return false;
        return true;
    }


    private void GenerateRandomFeaturePair()
    {
        string[] customerFeatures = System.Enum.GetNames(typeof(CustomerFeatures));
        int randomRollAllowed;
        randomRollAllowed = Random.Range(0, customerFeatures.Length);
        allowedFeature = customerFeatures[randomRollAllowed];

        int randomRollForbidden = Random.Range(0, customerFeatures.Length);
        if (randomRollAllowed == randomRollForbidden)
        {

            for (int i = 0; i < 7; i++)
            {
                randomRollForbidden = Random.Range(0, customerFeatures.Length);
                if (randomRollForbidden != randomRollAllowed)
                    break;
            }
            if (randomRollAllowed == randomRollForbidden)
            {
                if (randomRollForbidden + 1 < customerFeatures.Length)
                    randomRollForbidden += 1;
                else
                    randomRollForbidden -= 1;
            }
        }

        forbiddenFeature = customerFeatures[randomRollForbidden];

        Debug.Log("allowed " + allowedFeature + ". Forbidden: " + forbiddenFeature);
    }

    private string GetFeatureDescription(string feature, bool isAllowed = true)
    {
        string description = "";
        return description;
    }

    public void ProcessCustomerLeftLevel(Customer customer)
    {
        RemoveCustomer(customer);
        SpawnNewCustomer();
    }

    private void RemoveCustomer (Customer customer)
    {
        customers.Remove(customer);
        customer.parentPosition.childCustomer = null;
    }

    private void SpawnNewCustomer()
    {
        CustomerPosition emptyPosition = customerPositions[3];
        //find which customer spot is free
        foreach (CustomerPosition customerPosition in customerPositions)
        {
            if (customerPosition.childCustomer == null)
            {
                emptyPosition = customerPosition;
                break;
            }
        }

        GameObject newCustomerObj = Instantiate(customerPrefab, customerPositions[3].transform);
        Vector3 parentPos = customerPositions[3].transform.position;
        newCustomerObj.transform.position = new Vector3(parentPos.x, parentPos.y+5, parentPos.z);
        float rotationY = newCustomerObj.transform.rotation.y + 180f;
        Vector3 newRotation = new Vector3(newCustomerObj.transform.rotation.x, rotationY, newCustomerObj.transform.rotation.z);
        newCustomerObj.transform.eulerAngles = newRotation;
        Customer newCustomer = newCustomerObj.GetComponent<Customer>();
        customers.Add(newCustomer);
        //newCustomer.RandomizeCustomer();
        emptyPosition.childCustomer = newCustomer;
        newCustomer.parentPosition = emptyPosition;
        newCustomer.StartMovingToParent();
    }

    private void CalculateRemainingTime()
    {
        int seconds = ((int)gameStartTime + (int)gameDuration - (int)Time.time);
        displaySeconds = seconds % 60;
        displayMinutes = (seconds / 60) % 60;
    }

    public void ProcessCustomerEntry(Customer customer, bool isGoodCustomer = true)
    {
        isGoodCustomer = false;
        bool isForbidden = false;
        List<CustomerFeatures> customerFeatures = new List<CustomerFeatures>();
        foreach(CustomerFeatures feature in customer.myVisuals.customerFeatures)
        {
            if (feature.ToString() == forbiddenFeature)
            {
                isForbidden = true;
            }
            if (feature.ToString() == allowedFeature)
                isGoodCustomer = true;
        }

        if (isForbidden)
        {
            isGoodCustomer = false;
        }

        if (isGoodCustomer)
        {
            if (!isGameOver)
                score += scoreIncrement;
            audioManager.PlayYaySFX();
        }
        else
        {
            audioManager.PlayWrongSFX();
            score -= scoreIncrement;
        }

       

    }

    private void Update()
    {
        if (Time.time > gameStartTime + gameDuration & !isGameOver)
        {
            isGameOver = true;
            EndGame();
            return;
        }
        if (isGameOver)
            return;
        CalculateRemainingTime();
        string secondText = displaySeconds.ToString();
        if (displaySeconds < 10)
            secondText = "0"+ displaySeconds.ToString();
        scoreText.text = "Time: 0" + displayMinutes.ToString() + ":" + displaySeconds.ToString() + "\nScore: " + score.ToString() ;

    }

    private void EndGame()
    {
        audioManager.PlayGameOverSFX();
        scoreText.text = "GAME OVER " + "\nYou earned : " + score.ToString() + " points";
    }
}
