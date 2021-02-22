using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SpawnSeeds : MonoBehaviour
{

    public AgaveObject[] childrenAgaveObjects;
    public List<AgaveObject> listPool = new List<AgaveObject>();
    //public List<AgaveObject> activeObject = new List<AgaveObject>();
    public int numOfSeedToActivate = 4;
  

    private void Start()
    {

        childrenAgaveObjects = GetComponentsInChildren<AgaveObject>(true);
        foreach (var agaveObjects in childrenAgaveObjects)
        {
            listPool.Add(agaveObjects);
        }




    }

    private int count = 0;
    private Vector3 seedSize;


    public void SpawnAgaveObjecNow()
    {
        for (int i = 0; i < numOfSeedToActivate; i++)
        {
            if (!listPool[i].gameObject.activeSelf)
            {
                listPool[i].gameObject.SetActive(true);
                listPool[i].transform.position = transform.position + Random.insideUnitSphere *.4f;
                //activeObject.Add(listPool[i]);
            }
        }
    }

    //private void OnParticleCollision(GameObject other)
    //{

    //    int numCollisionEvents = seedsParticles.GetCollisionEvents(other, collisionEvents);


    //    if ((other.CompareTag("Ground") || other.CompareTag("Player")) && count < numOfSeed)//other.CompareTag("Player") && count < numOfSeed)
    //    {


    //        var emptyGO = new GameObject();
    //        emptyGO.transform.parent = null;
    //        var seed = Instantiate(newSeed, collisionEvents[0].intersection, Quaternion.identity, transform);
    //        seed.transform.parent = emptyGO.transform;


    //        count++;



    //    }




    //}

   
}
