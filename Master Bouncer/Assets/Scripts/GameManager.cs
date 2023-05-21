using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;
    int scoreIncrement = 10;

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

        // rule 1 + what is needed

        for (int i = 0; i < customerFeatures.Length; i++)
        {
            //Debug.Log(customerFeatures[i]);
        }

        // rule 2 + what is not needed
    }

    private bool IsFeaturePairPossible()
    {
        if (allowedFeature == "elf")
            return false;
        if (allowedFeature == "beard")
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
        Customer newCustomer = newCustomerObj.GetComponent<Customer>();
        customers.Add(newCustomer);
        //newCustomer.RandomizeCustomer();
        emptyPosition.childCustomer = newCustomer;
        newCustomer.parentPosition = emptyPosition;
        newCustomer.StartMovingToParent();
    }

    

    public void ProcessCustomerEntry(bool isGoodCustomer = true)
    {
        if (isGoodCustomer)
        {
            score += scoreIncrement;
            audioManager.PlayYaySFX();
        }
        else
        {
            score -= scoreIncrement;
        }

        scoreText.text = "Score: " + score.ToString();

    }
}
