using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgaveObject : MonoBehaviour
{
    public enum AgaveObjectsInteractables { Sticks, FireStick, SmallRock, BigRock, Seed, AgaveLeaf, None }

    public AgaveObjectsInteractables agaveObject = AgaveObjectsInteractables.None;

    public bool _isEdible = false;
}
