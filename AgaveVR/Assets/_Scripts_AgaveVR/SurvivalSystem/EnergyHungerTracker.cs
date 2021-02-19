using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergyHungerTracker : MonoBehaviour
{
    public EatingScript eatingEvent;
    //Energy Display and Decrease
    public TextMeshPro energyDisplay;
    public float energy = 100;
    private float fullEnergy;
    private float timer = 0;
    public float intervalToReduceEnergyInSeconds = 60f;
    public float intervalToSurvive = 8f;


    private void OnEnable()
    {
        if(eatingEvent != null)
        {
            eatingEvent.ateEvent += AteSomething;
        }
        
    }

    private void OnDisable()
    {
        if (eatingEvent != null)
        {
            eatingEvent.ateEvent -= AteSomething;
        }
    }

    private void AteSomething(float energyReceived)
    {
        EnergyAdded(energyReceived);


    }
    private void Start()
    {
        fullEnergy = energy;
        energyDisplay.text = ((int)energy).ToString();
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if ((int)timer % intervalToReduceEnergyInSeconds == 0) // every minute
        {

            EnergySpent(intervalToSurvive / fullEnergy);
           
        }
    }


    public void EnergySpent(float energySpent)
    {
        energy -= energySpent;//intervalToSurvive / fullEnergy;
        energyDisplay.text = ((int)energy).ToString();
    }

    public void EnergyAdded(float energyAdded)
    {

        energy += energyAdded;
        energyDisplay.text = ((int)energy).ToString();  
    }
}
