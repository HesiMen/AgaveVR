using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgaveObject : MonoBehaviour
{

    public Rigidbody rb;// get this one 
    public Collider col;//Get all colliders
    public enum AgaveObjectsInteractables { Sticks, FireStick, SmallRock, BigRock, Seed, AgaveLeaf, None }
    public enum WhichSeed { Nopal, Agave, Sunflower, Papalo,NotASeed}


    public AgaveObjectsInteractables agaveObject = AgaveObjectsInteractables.None;
    public WhichSeed whichSeed = WhichSeed.NotASeed;

    public bool _isEdible = false;


    private void Start()
    {
      //  rb = GetComponent<Rigidbody>();
    }

}
