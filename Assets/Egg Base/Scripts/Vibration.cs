using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibration : MonoBehaviour
{
    public float TimeVibration;
    public bool isVibration = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isVibration == true) Handheld.Vibrate();
    }

    private IEnumerator VibrationTime(float time)
    {
     
        if (isVibration == true)
        {
            yield return new WaitForSecondsRealtime(time);
            isVibration = false;
        }
       
       

    }
}
