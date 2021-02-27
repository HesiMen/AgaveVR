using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateObjects : MonoBehaviour
{

    public Transform feet;
    public Transform rightHand;
    public Transform leftHand;
    public Transform head;

    public List<AgaveObject> agaveObject;

    public bool _HasSeed = false;
    public bool _HasTool = false;

    public static PlayerStateObjects i;

    public List<AgaveObject> agabeObjectsHeld = new List<AgaveObject>();
    private void Awake()
    {
        i = this;
    }

    public void AgaveObjectToAdd(AgaveObject agaveObject, Hand hand)
    {
        agabeObjectsHeld.Add(agaveObject);
        PlaySoundInContext(agaveObject.agaveObject, hand.transform);
    }

    public void AgaveObjectToRemove(AgaveObject agaveObject, Hand hand)
    {
        if (agabeObjectsHeld.Contains(agaveObject))
        {
            agabeObjectsHeld.Remove(agaveObject);
        }
    }

    public void PlaySoundInContext(AgaveObject.AgaveObjectsInteractables agaveobjInteract, Transform trans)
    {
        switch (agaveobjInteract)
        {
            case AgaveObject.AgaveObjectsInteractables.Seed:
                PlayerSoundManager.i.PlaySoundSimple(PlayerSoundManager.i.grabSeedsString, trans.position);
                break;

            case AgaveObject.AgaveObjectsInteractables.Sticks:
                //PlayerSoundManager.i.PlaySoundSimple(PlayerSoundManager.i.grabSeedsString, trans.position);
                break;

            case AgaveObject.AgaveObjectsInteractables.FireStick:
                break;

            case AgaveObject.AgaveObjectsInteractables.SmallRock:

                break;
        }

    }

    public void PlaySoundOnHead(string soundString)
    {
        PlayerSoundManager.i.PlaySoundSimple(soundString, head.position);
    }

}
