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
    public enum BeatPosition { TeachingGrabObjects, TeachingGrabSeeds, TeachingFire, TeachingFood, TeachingBodyTemp, Moving, NotMoving }

    public BeatPosition positionState;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform teachingGrabSeed;
    [SerializeField] Transform teachingRockCenter;


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

    public StonePosEvent ArriveTeachingGrab;
    public StonePosEvent ArrivedTeachingGrabSeed;
    public StonePosEvent ArraviedTeachingFire;
    public StonePosEvent ArraviedTeachingFood;

    Vector3 nextPos;

    public Rigidbody rb;
    private void Update()
    {
        StoneStatePosUpdate();
    }

    public void LookAtPlayer()
    {
        Vector3 targetPos = new Vector3(Camera.main.transform.position.x, this.transform.position.y, Camera.main.transform.position.z);
        agent.transform.DOLookAt(targetPos, lookAtSpeed);

    }
    private void StoneStatePosUpdate()
    {
        switch (positionState)
        {
            case BeatPosition.Moving:

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


                            ArriveTeachingGrab.Invoke();
                            PlayStopRockSound();
                            whichBeatCount = 1;

                            positionState = BeatPosition.TeachingGrabSeeds;
                            break;

                        case 1:

                            ArrivedTeachingGrabSeed.Invoke();
                            PlayStopRockSound();
                            whichBeatCount = 2;
                            positionState = BeatPosition.TeachingFire;


                            break;

                        case 2:

                            ArraviedTeachingFire.Invoke();
                            PlayStopRockSound();
                            whichBeatCount = 3;
                            positionState = BeatPosition.TeachingFood;
                            break;

                        case 3:

                            ArraviedTeachingFood.Invoke();
                            PlayStopRockSound();
                            whichBeatCount = 4;
                            positionState = BeatPosition.NotMoving;
                            break;

                    }


                }
                break;

            case BeatPosition.TeachingGrabObjects:
                if (FoundPlayerAroundObject(teachingRockCenter, 1f))
                {


                    agent.SetDestination(stoneStop);
                    positionState = BeatPosition.Moving;
                }
                break;
            case BeatPosition.TeachingGrabSeeds:
                if (beatComplete[0])
                {

                    if (FoundRandomPointAroundNavMesh(teachingGrabSeed)) //go to position near player if found
                    {
                        WorldSoundManager.i.PlayAndAttach(WorldSoundManager.i.stoneRiseInstance, agent.transform, rb);

                        //agent.SetDestination(stoneStop);
                        positionState = BeatPosition.Moving;

                    }
                }
                break;

            case BeatPosition.TeachingFire:
                // Just Chilling until Beat Pos 2 Happens
                if (beatComplete[1])
                {
                    if (FoundRandomPointAroundNavMesh(TeachingFire))
                    {
                        WorldSoundManager.i.PlayAndAttach(WorldSoundManager.i.stoneRiseInstance, agent.transform, rb);

                        positionState = BeatPosition.Moving;
                    }


                }
                break;

            case BeatPosition.TeachingFood:
                if (beatComplete[2])
                {
                    if (FoundRandomPointAroundNavMesh(TeachingFood))
                    {
                        WorldSoundManager.i.PlayAndAttach(WorldSoundManager.i.stoneRiseInstance, agent.transform, rb);

                        positionState = BeatPosition.Moving;
                    }

                }
                break;

            case BeatPosition.TeachingBodyTemp:

                if (beatComplete[3])
                {
                    if (FoundRandomPointAroundNavMesh(TeachingTemp))
                    {
                        WorldSoundManager.i.PlayAndAttach(WorldSoundManager.i.stoneRiseInstance, agent.transform, rb);

                        positionState = BeatPosition.Moving;
                    }

                }
                break;



            case BeatPosition.NotMoving:

                break;

        }




    }

    private void PlayStopRockSound()
    {
        WorldSoundManager.i.stoneRiseInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        WorldSoundManager.i.PlaySoundSimple(WorldSoundManager.i.stoneKill, agent.transform.position);
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
    private bool FoundPlayerAroundObject(Transform transObj, float radiousTravel)
    {

        bool playerWasFound = false;

        int maxCol = 3;
        Vector3 possiblePos;
        Collider[] hitColliders = new Collider[maxCol];
        int numColl = Physics.OverlapSphereNonAlloc(transObj.position, radiousCheck, hitColliders, layerToHit);
        for (int i = 0; i < numColl; i++)
        {
            //Debug.Log(hitColliders[i].);

            if (hitColliders[i].CompareTag("Player"))
            {

                possiblePos = transObj.position + (radiousTravel * UnityEngine.Random.insideUnitSphere); //+ (hitColliders[0].transform.position + (hitColliders[0].transform.forward));

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
