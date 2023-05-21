using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerFeatures
{
    flamingo,
    elf,
    rabbit,
    hasTattoos,
    hasWings,
    panda,
    hasBeard,
    hasShoes,
    hasFur
}

public class Customer : MonoBehaviour
{
    public bool exitedLevel = false;
    public bool enteredClub = false;
    public GameObject ragDoll;
    public GameObject defaultObject;

    AudioSource thisAudioSource;
    public AudioClip yaySFX;
    public AudioClip grabSFX;
    public AudioClip throwSFX;

    public float startMoveTime;


    /// <summary>
    /// POSITION
    /// </summary>
    public bool isOnWayToParentObject = false;
    public CustomerPosition parentPosition;

    #region RANDOM customer GENERATOR
    public CustomerVisuals[] customerRoster;
    public CustomerVisuals myVisuals;
    #endregion


    private void Start()
    {
        thisAudioSource = gameObject.GetComponent<AudioSource>();
        RandomizeCustomer();
    }

    public void ActivateRagdoll()
    {
      //  ragDoll.SetActive(true);
     //   defaultObject.SetActive(false);
    }

    public void PlayYaySFX()
    {
        thisAudioSource.PlayOneShot(yaySFX);
    }

    public void PlayGrabSFX()
    {
        thisAudioSource.PlayOneShot(grabSFX);
    }

    public void StartMovingToParent()
    {
        Debug.Log("start moving to parent: " + parentPosition.gameObject.name);
        isOnWayToParentObject = true;
        startMoveTime = Time.time;
    }

    public void RandomizeCustomer()
    {
        int randomRoll = Random.Range(0, customerRoster.Length);
        for (int i = 0; i < customerRoster.Length; i++)
        {
            if (i == randomRoll)
            {
                customerRoster[i].customerModel.SetActive(true);
            }
            else
                customerRoster[i].customerModel.SetActive(false);
        }
        myVisuals = customerRoster[randomRoll].GetComponent<CustomerVisuals>();
    }

    private void FixedUpdate()
    {
        if (isOnWayToParentObject && Time.time < startMoveTime + 4f)
        {
            float zPos = Mathf.Lerp(transform.position.z, parentPosition.transform.position.z, Time.deltaTime);
            if (zPos <= 0.01f)
            {
                isOnWayToParentObject = false;
            }    
            transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
        }    
    }
}
