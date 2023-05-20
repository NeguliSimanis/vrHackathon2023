using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubLight : MonoBehaviour
{
    Light thisLight;
    float maxIntensity = 6;
    float minIntensity = 3;
    bool isLightGettingBrighter = true;

    bool isChangingR = false;
    bool isIncreasingR = false;
    bool isChangingB = true;
    bool isIncreasingB = true;

    // Start is called before the first frame update
    void Start()
    {
        thisLight = gameObject.GetComponent<Light>();
    }




    private void FixedUpdate()
    {
        if (isLightGettingBrighter)
        {
            thisLight.intensity += 0.1f;
            if (thisLight.intensity >= maxIntensity)
            {
                isLightGettingBrighter = false;
            }
        }
        else
        {
            thisLight.intensity -= 0.1f;
            if (thisLight.intensity <= minIntensity)
            {
                isLightGettingBrighter = true;
            }
        }

        if (isChangingB == true)
        {

        }
        else if (isChangingR == true)
        {

        }
    }
}
