using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
   //this is a script to let sound manager which hand just held an object

    public enum WhichHand { RightHand, LeftHand, None}
    public WhichHand whichHand = WhichHand.None;

}
