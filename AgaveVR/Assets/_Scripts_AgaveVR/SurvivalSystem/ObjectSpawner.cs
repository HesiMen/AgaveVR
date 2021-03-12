using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public float spawnRadius = 10.0f;
    public float spawnInterval = 10.0f;
    public int spawnMax = 5;

    private int spawnedObjects = 0;
    private bool maxSpawned = false;

    public AgaveObject[] spawnableObjects;
    Coroutine spawnCoroutine;


    void Start()
    {
        // Begin spawning
        spawnCoroutine = StartCoroutine(Spawn(spawnInterval));

    }

    // Update is called once per frame
    void Update()
    {
        maxSpawned = spawnedObjects >= spawnMax;
    }

    IEnumerator Spawn(float interval) 
    {
        while (true)
        {
            if (!maxSpawned)
            {
                yield return new WaitForSeconds(interval);
                Vector3 spawnPos = gameObject.transform.position;

                Vector2 radiusPos = Random.insideUnitCircle.normalized * spawnRadius;

                spawnPos.x += radiusPos.x;
                spawnPos.z += radiusPos.y;
                
                Instantiate(spawnableObjects[Random.Range(0, spawnableObjects.Length)], spawnPos, Quaternion.identity);

                spawnedObjects++;
            }
        }
    }

    public void LowerSpawnCount()
    {
        spawnedObjects--;
    }

}
