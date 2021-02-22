using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalParticles : MonoBehaviour
{
    public static GlobalParticles i;

    public enum SeedParticles { Agave, Nopal, Sunflower, Papalo,Sticks }

    public SpawnSeeds[] seedsPrefabs = new SpawnSeeds[5];
    private void Awake()
    {
        i = this;
    }


    public void PlayParticleOnPlace(Transform place, SeedParticles seed)
    {
        Debug.Log("PlayParticleOnplace");
        seedsPrefabs[(int)seed].transform.position = place.position;
        seedsPrefabs[(int)seed].transform.rotation = place.rotation;
        seedsPrefabs[(int)seed].SpawnAgaveObjecNow();
    }

}
