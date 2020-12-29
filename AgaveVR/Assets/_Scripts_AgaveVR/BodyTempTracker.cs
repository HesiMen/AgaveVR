using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BodyTempTracker : MonoBehaviour
{
    /*
    The body temperature tracker will use essentially the same base as the energy tracker,
    but will operate differently because instead of a 0-100 energy tracker that depletes over time and is refilled,
    the body temperature tracker looks to stabilize around a standard temperature, let's say 50 on the 0-100 scale.

    The other difference is in the events that modify this number. Things are rudimentary right now but will 
    depend heavily on the weather system. Based on the weather state and intensity, the body temperature will
    periodically increase or decrease.

    Eating food will increase body temperature by a small amount, and we have to check if we are in the radius
    of certain temperature affecting objects (such as fire, which will provide a periodic increase that could
    balance out the periodic change of the outside weather)

    These two examples are simple enough, the more interesting one will be dealing with hot and sunny weather,
    in terms of how we implement shade. If shaded areas are explicitly labeled, it'll be as simple as a collision
    check there as well.
    */

    public BodyTempScript bodyTempEvent;
    //Temp Display and Increase/Decrease
    public TextMeshPro tempDisplay;
    public float temp = 100;
    private float normalTemp;
    private float timer = 0;
    public float intervalToChangeTempinSeconds = 60f;
    public float intervalToSurvive = 8f;

    private void OnEnable()
    {
        if(bodyTempEvent != null)
        {
            bodyTempEvent.tempEvent += TempChange;
        }
        
    }

    private void OnDisable()
    {
        if (bodyTempEvent != null)
        {
            bodyTempEvent.tempEvent -= TempChange;
        }
    }

    private void TempChange(float degrees)
    {
        temp += degrees;
        tempDisplay.text = ((int)temp).ToString();
    }

    private void Start()
    {
        normalTemp = temp;
        tempDisplay.text = ((int)temp).ToString();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if ((int)timer % intervalToChangeTempinSeconds == 0) // every minute
        {
            // modify based on conditions
            TempChange(intervalToSurvive / normalTemp);
           
        }
    }

}
