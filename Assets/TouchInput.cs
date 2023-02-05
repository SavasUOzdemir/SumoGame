using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    float screenWidth = Screen.width;
    //I'm really not sure how this would work, as I cannot test at the moment.
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch finger = Input.GetTouch(0);
            if (finger.phase == TouchPhase.Began)
            {
                if (finger.position.x>screenWidth/2)
                {
                    Input.GetAxis("Horizontal");
                }
                else
                {
                    Input.GetAxis("Horizontal");
                }
            }
        }   
    }
}
