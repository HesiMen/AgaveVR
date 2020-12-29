using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SpawnSeeds : MonoBehaviour
{

    [FMODUnity.EventRef] 
    [SerializeField] private string soundClip;

    FMOD.Studio.EventInstance soundState;

    public ParticleSystem seedsParticles;

    public List<ParticleCollisionEvent> collisionEvents;


    public float numOfSeed = 4f;
    public GameObject newSeed;


    



    private void Start()
    {

        soundState = FMODUnity.RuntimeManager.CreateInstance(soundClip);    //call sound  via event ref

        seedsParticles = GetComponent<ParticleSystem>();

        collisionEvents = new List<ParticleCollisionEvent>();


    }

    private int count = 0;
    private Vector3 seedSize;
    private void OnParticleCollision(GameObject other)
    {

        int numCollisionEvents = seedsParticles.GetCollisionEvents(other, collisionEvents);


        if ((other.CompareTag("Ground") || other.CompareTag("Player")) && count < numOfSeed)//other.CompareTag("Player") && count < numOfSeed)
        {


            FMODUnity.RuntimeManager.PlayOneShot(soundClip);

            var emptyGO = new GameObject();
            emptyGO.transform.parent = null;
            var seed = Instantiate(newSeed, collisionEvents[0].intersection, Quaternion.identity, transform);
            seed.transform.parent = emptyGO.transform;

            
            count++;



        }




    }

    private void Update()
    {
        
    }

}
