using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedWasPlantedChoose : MonoBehaviour
{
    public enum Plants { Agave, SunFlower, Papalo, PricklyCactus,None }
    
    public GameObject[] plants = new GameObject[4];
    public GameObject lump;

    public void StartGrowingthisPlant(AgaveObject seed)
    {
        lump.SetActive(true);
        
        switch (seed.whichSeed)
        {
            case AgaveObject.WhichSeed.Agave:
                Instantiate(plants[0],transform.position, Quaternion.identity, transform);
               // plants[0].StartGrowig();
                break;
        }
    }

}
