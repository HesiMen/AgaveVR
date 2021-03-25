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
    private bool spawnCoroutineRunning = false;

    public AgaveObject[] spawnableObjects;
    Coroutine spawnCoroutine;


    void Start()
    {
        // Begin spawning
        spawnCoroutine = StartCoroutine(Spawn(spawnInterval));
        spawnCoroutineRunning = true;
}

// Update is called once per frame
void Update()
    {
        // Check if we can spawn
        if ((spawnedObjects < spawnMax) && spawnCoroutineRunning == false)
        {
            spawnCoroutine = StartCoroutine(Spawn(spawnInterval));
            spawnCoroutineRunning = true;
            Debug.Log("Starting object spawns.");
        }

        // Check if we need to stop spawns
        if (spawnedObjects >= spawnMax && spawnCoroutineRunning == true)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutineRunning = false;
            Debug.Log("Stopping object spawns.");
        }
    }

    void UpdateSpawns()
    {
        Vector3 spawnPos = gameObject.transform.position;

        Vector2 radiusPos = Random.insideUnitCircle.normalized * spawnRadius;

        spawnPos.x += radiusPos.x;
        spawnPos.y += 0.5f;
        spawnPos.z += radiusPos.y;

        AgaveObject spawnedObject = Instantiate(spawnableObjects[(int)Random.Range(0, spawnableObjects.Length)], spawnPos, Quaternion.identity);
        spawnedObject.SetSourceSpawner(this);

        spawnedObjects++;
    }

    IEnumerator Spawn(float interval) 
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            UpdateSpawns();
        }
    }

    public void LowerSpawnCount()
    {
        spawnedObjects--;
    }

}
