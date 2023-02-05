using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timerScript : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    //I could also make timeleft variable a property, but current version works okay.
    public float timeLeft = 120f;

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timeLeft);
        }
        else
        {
            timerText.text = "Time's Up!";
        }
    }
}
