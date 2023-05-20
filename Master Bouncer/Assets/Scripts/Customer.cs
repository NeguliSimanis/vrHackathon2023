using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        thisAudioSource = gameObject.GetComponent<AudioSource>();
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
