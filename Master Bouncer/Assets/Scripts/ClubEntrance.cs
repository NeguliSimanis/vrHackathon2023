using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubEntrance : MonoBehaviour
{

    [SerializeField]
    GameManager gameManager;
    
    private void OnTriggerEnter(Collider other)
    {
        Customer newCustomer = other.gameObject.GetComponent<Customer>();
        if (newCustomer == null)
        {
            return;
        }
            Debug.Log("entered");

        if (!newCustomer.enteredClub)
        {
            gameManager.ProcessCustomerEntry();
            //newCustomer.PlayYaySFX();
            newCustomer.enteredClub = true;
        }
    }
}
