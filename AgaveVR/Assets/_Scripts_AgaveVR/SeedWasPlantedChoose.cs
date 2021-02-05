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
                Instantiate(plants[(int)Plants.Agave],transform.position, Quaternion.identity, transform);
               // plants[0].StartGrowig();
                break;

            case AgaveObject.WhichSeed.Nopal:
                Instantiate(plants[(int)Plants.PricklyCactus], transform.position, Quaternion.identity, transform);
                // plants[0].StartGrowig();
                break;
            case AgaveObject.WhichSeed.Papalo:
                Instantiate(plants[(int)Plants.Papalo], transform.position, Quaternion.identity, transform);
                // plants[0].StartGrowig();
                break;
            case AgaveObject.WhichSeed.Sunflower:
                Instantiate(plants[(int)Plants.SunFlower], transform.position, Quaternion.identity, transform);
                // plants[0].StartGrowig();
                break;
        }
    }

}
