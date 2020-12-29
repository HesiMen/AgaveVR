using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class ProtectScript : MonoBehaviour
{
    [SerializeField] Transform stone;
    public float radiousSphereProtect = 2f;
    public LayerMask mask;
    public int searchCap = 7;

    public float moveSpeed = 3f;
   

    public List<GameObject> listObj;

    private bool _isProtect = false;


    Vector3 ogPost;
    private void Start()
    {
        listObj = new List<GameObject>();
        ogPost = stone.transform.position; 
    }
    private void FixedUpdate()
    {
        SphereCheck();
        if (listObj.Count > 2)
        {
            ProtectWith(stone);
        }
       
        if(listObj.Count == 1)
        {
            float step = moveSpeed * Time.deltaTime;
            Vector3 newPosY = new Vector3(stone.position.x, Mathf.Clamp(listObj[0].transform.position.y, 0f, 15f), stone.position.z);
            stone.position = Vector3.MoveTowards(stone.position,newPosY , step); 
                
                //new Vector3(stone.position.x, Mathf.Clamp( listObj[0].transform.position.y,0f,15f), stone.position.z);
        }

        if (listObj.Count == 0)
        {
            float step = moveSpeed * Time.deltaTime;
            stone.position = Vector3.MoveTowards(stone.position, ogPost, step);
        }
    }


    private void ProtectWith(Transform thisStone)
    {
        float step = moveSpeed * Time.deltaTime;
        Vector3 newPosY = new Vector3(thisStone.position.x, Mathf.Clamp(GetClosestObject(listObj, stone).position.y,0f,15f), thisStone.position.z);


        thisStone.position = Vector3.MoveTowards(thisStone.position, newPosY, step);//newPosY;
    }

    Transform GetClosestObject(List<GameObject> listObjects, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (GameObject potentialTarget in listObjects)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }
        return bestTarget;
    }
    private void SphereCheck()
    {
       
        
        Collider[] possibleObjects = new Collider[searchCap];
        int targetCount = Physics.OverlapSphereNonAlloc(transform.position, radiousSphereProtect, possibleObjects, mask,QueryTriggerInteraction.Ignore );


        listObj.Clear();
        for (int i = 0; i < targetCount; i++)
        {
            if (possibleObjects[i] != null )
            {
                Rigidbody rb = possibleObjects[i].attachedRigidbody;
                if (rb != null)
                {
                    listObj.Add(possibleObjects[i].gameObject);
                }
            }
        }


        // We need to check when an object is hed // if is not held then move stone towards object. 
        foreach (Collider coll in possibleObjects)
        {
            Color color = Color.red;
            if (coll != null)
                Debug.DrawLine(transform.position, coll.transform.position, color);
        }


    }



#if UNITY_EDITOR

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiousSphereProtect);


    }


#endif
}
