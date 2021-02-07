using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureForSeeds : GestureBase
{
    public AgaveObject.WhichSeed whichSeed;
    public AgaveObject.AgaveObjectsInteractables wutToGive;

    public bool hasGivenSeed = false;
    public Transform seedSpawnPos;

    


    
    // public ParticleSystem[] seedPrefab = new ParticleSystem[4];

    public void SeedHasBeenGiven()
    {
        Debug.Log(whichSeed);
        switch (whichSeed)
        {
            case AgaveObject.WhichSeed.Nopal:
                GlobalParticles.i.PlayParticleOnPlace(seedSpawnPos, GlobalParticles.SeedParticles.Nopal);

                //Instantiate(seedPrefab[0], seeSpawnPos.position, seeSpawnPos.rotation, transform); // Instantiate will make the game lag. 
                break;

            case AgaveObject.WhichSeed.Agave:
                GlobalParticles.i.PlayParticleOnPlace(seedSpawnPos, GlobalParticles.SeedParticles.Agave);
                //Instantiate(seedPrefab[1], seeSpawnPos.position, seeSpawnPos.rotation, transform);

                break;

            case AgaveObject.WhichSeed.Sunflower:
                GlobalParticles.i.PlayParticleOnPlace(seedSpawnPos, GlobalParticles.SeedParticles.Sunflower);
                break;

            case AgaveObject.WhichSeed.Papalo:
                GlobalParticles.i.PlayParticleOnPlace(seedSpawnPos, GlobalParticles.SeedParticles.Papalo);
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
