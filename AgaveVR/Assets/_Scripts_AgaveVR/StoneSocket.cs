using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;
using System;
using UnityEngine.Events;

[Serializable] public class RewardStoneEvent : UnityEvent { }
public class StoneSocket : MonoBehaviour
{

    public RewardStoneEvent ReardInStone;
    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log(other.name);

        if (other.GetComponentInParent<AgaveObject>() != null && other.GetComponentInParent<AgaveObject>().agaveObject == AgaveObject.AgaveObjectsInteractables.SmallRock)
        {
            AgaveObject reward = other.GetComponentInParent<AgaveObject>();
            Debug.Log(reward.name);
            Destroy(reward.GetComponent<XROffsetGrabInteractable>());
            reward.rb.isKinematic = true;
            foreach (var item in reward.col)
            {
                item.enabled = false;
            }
           
           
            

            MoveStickTween(reward);
           

        }
    }

    private Rigidbody rb;
    private void MoveStickTween(AgaveObject _reward)
    {
        //_reward.GetComponentInParent<Collider>().enabled = false;
        //_reward.GetComponentInParent<Rigidbody>().isKinematic = true;
        rb = _reward.rb;
        _reward.transform.parent = transform;
        Sequence movingStickSequence = DOTween.Sequence();

        movingStickSequence.Append(_reward.transform.DOMove(this.transform.position, 1f, false).OnComplete(DisableFreeFall));

        movingStickSequence.Insert(0, _reward.transform.DORotate(transform.rotation.eulerAngles, 1f));


     

    }

  
    private void DisableFreeFall()
    {
        rb.useGravity = false;
        rb.isKinematic = true;

        ReardInStone.Invoke();
    }
}
