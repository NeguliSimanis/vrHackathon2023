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
