using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubLight : MonoBehaviour
{
    Light thisLight;
    float maxIntensity = 6;
    float minIntensity = 3;
    float lightChangeSpeed = 0.1f;
    float minLightChangeSpeed = 0.03f;
    float maxLightChangeSpeed = 0.18f;
    bool isLightChangeSpeedIncreasing = true;
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


    private void ChangeLightChangeSpeed()
    {
        if (isLightChangeSpeedIncreasing)
        {
            lightChangeSpeed += 0.001f;
            if (lightChangeSpeed > maxLightChangeSpeed)
                isLightChangeSpeedIncreasing = false;
        }
        else
        {
            lightChangeSpeed -= 0.001f;
            if (lightChangeSpeed < minLightChangeSpeed)
                isLightChangeSpeedIncreasing = true;
        }

    }


    private void FixedUpdate()
    {
        ChangeLightChangeSpeed();
        if (isLightGettingBrighter)
        {
            thisLight.intensity += lightChangeSpeed;
            if (thisLight.intensity >= maxIntensity)
            {
                isLightGettingBrighter = false;
            }
        }
        else
        {
            thisLight.intensity -= lightChangeSpeed;
            if (thisLight.intensity <= minIntensity)
            {
                isLightGettingBrighter = true;
            }
        }
        Color currColor = thisLight.color;
        if (isChangingB == true)
        {
            if (isIncreasingB)
            {

                thisLight.color = new Color(currColor.r, currColor.g, currColor.b + 0.01f);
                if (thisLight.color.b >= 0.99)
                    isIncreasingB = false;
            }
            else
            {
                thisLight.color = new Color(currColor.r, currColor.g, currColor.b - 0.01f);
                if (thisLight.color.b <= 0.01)
                    isIncreasingB = true;
            }

        }
        else if (isChangingR == true)
        {
            if (isIncreasingR)
            {

            }
            else
            {

            }
        }
    }
}
