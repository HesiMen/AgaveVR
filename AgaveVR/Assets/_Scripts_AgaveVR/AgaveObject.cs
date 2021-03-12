using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class AgaveObject : MonoBehaviour
{

    public Rigidbody rb;// get this one 
    public Collider[] col;//Get all colliders

    private XROffsetGrabInteractable interactable;
    public enum AgaveObjectsInteractables { Sticks, FireStick, SmallRock, BigRock, Seed, AgaveLeaf, None }
    public enum WhichSeed { Nopal, Agave, Sunflower, Papalo, NotASeed }


    public AgaveObjectsInteractables agaveObject = AgaveObjectsInteractables.None;
    public WhichSeed whichSeed = WhichSeed.NotASeed;
    private ObjectSpawner sourceSpawner;

    public bool _isEdible = false;
    public bool _isHeld = false;

    [SerializeField] public Consumable consumable;

    private void Start()
    {

        if (_isEdible && consumable == null)
        {
            Debug.Log("You need a consumable Scriptable Object");



        }

        col = GetComponentsInChildren<Collider>();

        if (GetComponent<XROffsetGrabInteractable>() != null)
        {
            interactable = GetComponent<XROffsetGrabInteractable>();
        }
    }
    public void ObjectHeld(bool held, Hand whichHand)
    {
       
        _isHeld = held;
        if (held)
        {
            PlayerStateObjects.i.AgaveObjectToAdd(this, whichHand);// adding to list of objects bing held
            //PlayerSoundManager.i.PlaySoundSimple(PlayerSoundManager.i.grabSeedsString)
            Debug.Log(this.gameObject.name + "--- Has been grabbed");
        }
        else
        {
            PlayerStateObjects.i.AgaveObjectToRemove(this, whichHand);
            Debug.Log(this.gameObject.name + "--- Has been dropped");
        }
        if (interactable != null)
        {
           // Debug.Log("interactable ignores cols" + held);
            interactable.IgnorePlayerCollision(held);
        }

    }

    public void SetSourceSpawner(ObjectSpawner spawner)
    {
        sourceSpawner = spawner;
    }


}
