using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // This is a testing script to instantiate crafting components to be deleted later
        var stickPrefab = Resources.Load("Crafting/Stick");
        GameObject stickObject = Instantiate(stickPrefab, new Vector3(3.5f, -0.5f, 0f), Quaternion.identity) as GameObject;

        var stonePrefab = Resources.Load("Crafting/Stone");
        GameObject stoneObject = Instantiate(stonePrefab, new Vector3(2.5f, -0.5f, 0f), Quaternion.identity) as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
