using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.AI;
using UnityEngine.Events;

public class MovingRockInstructions : MonoBehaviour
{
    // Start is called before the first frame update
    [Serializable]
    public class StonePosEvent : UnityEvent { }
    public enum BeatPosition { TeachingGrabSeeds, TeachingFire, TeachingFood, TeachingBodyTemp, Moving, NotMoving }

    public BeatPosition positionState;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform agaveCenter;

    public Transform TeachingFire;
    public Transform TeachingFood;
    public Transform TeachingTemp;


    public LayerMask layerToHit;

    public float radiousCheck = 5f;
    public float moveToTargetSpeed = 3f;
    public float lookAtSpeed = .6f;

    private int whichBeatCount = 0;

    private Vector3 stoneStop;

    public bool[] beatComplete = { false, false, false, false };//beat 1, 2, 3,4

    public StonePosEvent ArrivedTeachingGrabSeed;
    public StonePosEvent ArraviedTeachingFire;
    public StonePosEvent ArraviedTeachingFood;

    Vector3 nextPos;

    public Rigidbody rb;
    private void Update()
    {
        StoneStatePosUpdate();
    }


    private void StoneStatePosUpdate()
    {
        switch (positionState)
        {
            case BeatPosition.Moving:
                WorldSoundManager.i.PlayAndAttach(WorldSoundManager.i.stoneRiseInstance, agent.transform,rb );
                Vector3 targetPos = new Vector3(Camera.main.transform.position.x, this.transform.position.y, Camera.main.transform.position.z);
                agent.transform.DOLookAt(targetPos, lookAtSpeed);
                bool closeToNext = Vector3.Distance(agent.transform.position, nextPos) < .01f;
               // Debug.Log(Vector3.Distance(transform.position, nextPos));
                if (closeToNext)
                {
                   
                    Debug.Log(whichBeatCount);
                    switch (whichBeatCount)
                    {
                        case 0:
                            whichBeatCount = 1;
                            ArrivedTeachingGrabSeed.Invoke();
                            positionState = BeatPosition.TeachingGrabSeeds;
                            WorldSoundManager.i.stoneRiseInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                            WorldSoundManager.i.PlaySoundSimple(WorldSoundManager.i.stoneKill, agent.transform.position);

                            break;

                        case 1:
                            whichBeatCount = 2;
                            ArraviedTeachingFire.Invoke();
                            positionState = BeatPosition.TeachingFire;
                            break;

                        case 2:
                            whichBeatCount = 3;
                            ArraviedTeachingFood.Invoke();
                            positionState = BeatPosition.TeachingFood;
                            break;
                        
                    }


                }
                break;


            case BeatPosition.TeachingGrabSeeds:
                if (FoundPlayerAroundAgave()) //go to position near player if found
                {
                    agent.SetDestination(stoneStop);
                    positionState = BeatPosition.Moving;
                    
                }

                break;

            case BeatPosition.TeachingFire:
                // Just Chilling until Beat Pos 2 Happens
                if (beatComplete[0])
                {
                    if (FoundRandomPointAroundNavMesh(TeachingFire))
                    {

                        positionState = BeatPosition.Moving;
                    }


                }
                break;

            case BeatPosition.TeachingFood:
                if (beatComplete[1])
                {
                    if (FoundRandomPointAroundNavMesh(TeachingFood))
                    {
                        positionState = BeatPosition.Moving;
                    }

                }
                break;

            case BeatPosition.TeachingBodyTemp:

                if (beatComplete[2])
                {
                    if (FoundRandomPointAroundNavMesh(TeachingTemp))
                    {
                        positionState = BeatPosition.Moving;
                    }

                }
                break;

           

            case BeatPosition.NotMoving:

                break;

        }




    }
    private bool FoundRandomPointAroundNavMesh(Transform trans)
    {
        bool foundPos = false;

        Vector3 randPos = trans.position + UnityEngine.Random.insideUnitSphere;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randPos, out hit, 1.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);// go to position if found point
            nextPos = hit.position;
            //positionState = BeatPosition.Moving;
            foundPos = true;
            Debug.DrawLine(agent.transform.position, hit.position);
            return foundPos;
        }
      

        // NavMesh.SamplePosition(possiblePos, out hit, 1.0f, NavMesh.AllAreas)


        return foundPos;
    }
    private bool FoundPlayerAroundAgave()
    {
        bool playerWasFound = false;

        int maxCol = 3;
        Vector3 possiblePos;
        Collider[] hitColliders = new Collider[maxCol];
        int numColl = Physics.OverlapSphereNonAlloc(agaveCenter.position, radiousCheck, hitColliders, layerToHit);
        for (int i = 0; i < numColl; i++)
        {
            //Debug.Log(hitColliders[i].);

            if (hitColliders[i].CompareTag("Player"))
            {

                possiblePos = (3 * UnityEngine.Random.insideUnitSphere) + (hitColliders[0].transform.position + (hitColliders[0].transform.forward * 2));

                //Debug.Log("I have possible Pos:" + possiblePos);
                NavMeshHit hit;
                if (NavMesh.SamplePosition(possiblePos, out hit, 1.0f, NavMesh.AllAreas))
                {
                    // Debug.Log("I has a Position: " + hit.position);
                    stoneStop = hit.position;
                    nextPos = stoneStop;
                    playerWasFound = true;

                    return playerWasFound;
                }





            }
        }


        return playerWasFound;
    }


    public void WhichBeatComplete(int beat) // use this in an event when beat is complete
    {
        beatComplete[beat] = true;
    }
}
