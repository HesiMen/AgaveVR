using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


[Serializable]
public class StickInTriggerEvent : UnityEvent { }
public class FireStickTrigger : MonoBehaviour
{
    private int turnCounter = 0;
    public StickInTriggerEvent FireStickReady;
    public StickInTriggerEvent MakeFire;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if(other.GetComponentInParent<AgaveObject>() != null && other.GetComponentInParent<AgaveObject>().agaveObject == AgaveObject.AgaveObjectsInteractables.FireStick)
        {
            FireStickReady.Invoke();
            Destroy(other.GetComponentInParent<XROffsetGrabInteractable>());
            other.GetComponentInParent<Rigidbody>().isKinematic = true;
            other.GetComponentInParent<Collider>().enabled = false;
            GameObject stick = other.GetComponentInParent<Transform>().gameObject;

            Debug.Log("FireStickInTrigger");

            MoveStickTween(stick);
            

        }
    }

    private GameObject fireStickUD;
    private void MoveStickTween(GameObject stickToMove)
    {
        fireStickUD = stickToMove;
        Sequence movingStickSequence = DOTween.Sequence();

        movingStickSequence.Append(stickToMove.transform.DOMove(this.transform.position, 2f, false).OnComplete(MoveUpDown));

        movingStickSequence.Insert(0,stickToMove.transform.DORotate(transform.rotation.eulerAngles, 2f));


    }

    private void MoveUpDown()
    {

        fireStickUD.transform.DOMoveY(transform.position.y + .5f, 1f, false).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }


    public void TurnRight()
    {

        if (turnCounter > 3)
        {
            MakeFire.Invoke();
        }
        Vector3 rightRotation = new Vector3(0f,-45,0);
        fireStickUD.transform.DORotate(rightRotation, .5f);
        turnCounter += 1;
    }

    public void TurnLeft()
    {

        Vector3 leftRotation = new Vector3(0f, 220f, 0);
        fireStickUD.transform.DORotate(leftRotation, .5f);
    }
}
