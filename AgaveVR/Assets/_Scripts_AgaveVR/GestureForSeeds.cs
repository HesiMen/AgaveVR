using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureForSeeds : GestureBase
{
    public AgaveObject.WhichSeed whichSeed;
    public bool hasGivenSeed = false;
    public Transform seeSpawnPos;
    public GameObject[] seedPrefab = new GameObject[4];

    public void SeedHasBeenGiven()
    {
        Debug.Log(whichSeed);
        switch (whichSeed)
        {
            case AgaveObject.WhichSeed.Nopal:
                Instantiate(seedPrefab[0], seeSpawnPos.position, seeSpawnPos.rotation, transform);
                break;

            case AgaveObject.WhichSeed.Agave:
                Instantiate(seedPrefab[1], seeSpawnPos.position, seeSpawnPos.rotation, transform);

                break;

            case AgaveObject.WhichSeed.Sunflower:

                break;

            case AgaveObject.WhichSeed.Papalo:
                break;
        }


        hasGivenSeed = true;
    }
    public override void ShowMesh()
    {
        if (!hasGivenSeed)
            base.ShowMesh();
    }

    public override void StopShowing()
    {
        if (!hasGivenSeed)
            base.StopShowing();
    }

}
