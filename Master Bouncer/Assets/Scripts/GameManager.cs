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

    void Start()
    {
        audioManager = gameObject.GetComponent<AudioManager>();
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
