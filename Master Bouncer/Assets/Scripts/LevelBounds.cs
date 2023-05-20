using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBounds : MonoBehaviour
{
    public GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Customer>()!= null)
        {
            Customer exitCustomer = other.gameObject.GetComponent<Customer>();
            if (!exitCustomer.exitedLevel)
            {
                exitCustomer.exitedLevel = true;
                gameManager.ProcessCustomerLeftLevel(exitCustomer);
            }
        }
    }
}
