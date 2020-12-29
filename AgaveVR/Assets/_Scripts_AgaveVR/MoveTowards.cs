using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.AI;
using UnityEngine.Events;

[Serializable]
public class StonePosEvent : UnityEvent { }

public class MoveTowards : MonoBehaviour
{
    public enum BeatPosition { Beat0Pos, Beat1Pos, Beat2Pos, Beat3Pos, Beat4Pos, NotMoving, Moving }

    public BeatPosition positionState;

    [SerializeField] NavMeshAgent agent;

    [SerializeField] Transform agaveCenter;

    public Transform Beat2;
    public Transform Beat3;
    public Transform Beat4;


    public LayerMask layerToHit;

    public float radiousCheck = 5f;
    public float moveToTargetSpeed = 3f;
    public float lookAtSpeed = .6f;

    private int whichBeatCount = 0;

    private Vector3 stoneStop;

    public bool[] beatComplete = { false, false, false, false };//beat 1, 2, 3,4

    public StonePosEvent ArraviedPos1;
    public StonePosEvent ArraviedPos2;
    public StonePosEvent ArraviedPos3;
    public StonePosEvent ArraviedPos4;


    private bool arrivedDes = false;
    private void Update()
    {
        StoneStatePosUpdate();
    }


    private void StoneStatePosUpdate()
    {
        switch (positionState)
        {
            case BeatPosition.Moving:
                Vector3 targetPos = new Vector3(Camera.main.transform.position.x, this.transform.position.y, Camera.main.transform.position.z);
                transform.DOLookAt(targetPos, lookAtSpeed);
                bool closeToNext = Vector3.Distance(transform.position, nextPos) < .01f;
                if (closeToNext)
                {
                    switch (whichBeatCount)
                    {
                        case 0:
                            whichBeatCount = 1;
                            ArraviedPos1.Invoke();
                            positionState = BeatPosition.Beat1Pos;
                            break;

                        case 1:
                            whichBeatCount = 2;
                            ArraviedPos2.Invoke();
                            positionState = BeatPosition.Beat2Pos;
                            break;

                        case 2:
                            whichBeatCount = 3;
                            ArraviedPos3.Invoke();
                            positionState = BeatPosition.Beat3Pos;
                            break;
                        case 3:
                            whichBeatCount = 4;
                            ArraviedPos4.Invoke();
                            positionState = BeatPosition.Beat4Pos;
                            break;
                    }


                }
                break;


            case BeatPosition.Beat0Pos:
                if (FoundPlayerAroundAgave()) //go to position near player if found
                {
                    agent.SetDestination(stoneStop);
                    positionState = BeatPosition.Moving;
                }

                break;

            case BeatPosition.Beat1Pos:
                // Just Chilling until Beat Pos 2 Happens
                if (beatComplete[0])
                {
                    if (FoundRandomPointAroundNavMesh(Beat2))
                    {

                        positionState = BeatPosition.Moving;
                    }


                }
                break;

            case BeatPosition.Beat2Pos:
                if (beatComplete[1])
                {
                    if (FoundRandomPointAroundNavMesh(Beat3))
                    {
                        positionState = BeatPosition.Moving;
                    }
                   
                }
                break;

            case BeatPosition.Beat3Pos:

                if (beatComplete[2])
                {
                    if (FoundRandomPointAroundNavMesh(Beat4))
                    {
                        positionState = BeatPosition.Moving;
                    }
                    
                }
                break;

            case BeatPosition.Beat4Pos:
                if (beatComplete[3])
                {
                    //if (FoundRandomPointAroundNavMesh(Beat2))
                    //{
                    //    positionState = BeatPosition.Moving;
                    //}
                }
                break;

            case BeatPosition.NotMoving:

                break;

        }

    }

    Vector3 nextPos;
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

            return foundPos;
        }

        // NavMesh.SamplePosition(possiblePos, out hit, 1.0f, NavMesh.AllAreas)


        return foundPos;
    }
    public void WhichBeatComplete(int beat) // use this in an event when beat is complete
    {
        beatComplete[beat] = true;
    }
    //private bool ArrivedDestination()
    //{
    //    bool arrived = false;

    //    if (!agent.pathPending)
    //    {
    //        if (agent.remainingDistance <= agent.stoppingDistance)
    //        {
    //            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
    //            {
    //                Debug.Log("Arrived to Destination!");

    //                arrived = true;
    //                return arrived;
    //            }
    //        }
    //    }

    //    return arrived;

    //}



    private bool FoundPlayerAroundAgave()
    {
        bool playerWasFound = false;

        int maxCol = 3;
        Vector3 possiblePos;
        Collider[] hitColliders = new Collider[maxCol];
        int numColl = Physics.OverlapSphereNonAlloc(agaveCenter.position, radiousCheck, hitColliders, layerToHit);
        for (int i = 0; i < numColl; i++)
        {
            // Debug.Log(hitColliders[i]);

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





}
