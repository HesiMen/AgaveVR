using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//Plan
//OpenHole
//Seed the hole
//Check which seed it is.
//instantiate the plant object that will make it grow. 
[Serializable]
public class SeedEvent : UnityEvent<AgaveObject> { }

public class SeedsHolePhysics : BeatEvent
{

    public SeedEvent seedWasPlanted;
    public enum HoleState { Close, Open, SeededOpen, SeededClosed, None }
    [Tooltip("HoleState")]
    public HoleState holeState;

    public float secondsToWaitEvent = 3f;


    private bool _HasBeenSeeded = false;
    private int _SeedCount = 0;

    public GameObject completeHole;



    private List<AgaveObject> seedsInHole = new List<AgaveObject>();

    private AgaveObject wutSeed;
    public void HoleStateChange(HoleState holeState)
    {
        switch (holeState)
        {
            case HoleState.Close:
                completeHole.SetActive(false);
                break;

            case HoleState.Open:
                completeHole.SetActive(true);
                break;

            case HoleState.SeededOpen:

                break;

            case HoleState.SeededClosed:
                //Instantiate
                //Here Hide Hole and make a litle bump to show something has been planted
                break;

        }
    }


    public void HoleOpens()
    {
        HoleStateChange(HoleState.Open);
    }
    private void Start()
    {

    }

    public void CloseHole()
    {
        switch (_SeedCount)
        {
            case 0:
                //NoSeed
                HoleStateChange(HoleState.SeededClosed);
                break;
            case 1:
                //Seed With One Seed Check which Seed
                if (seedsInHole.Count >= 1)
                {
                    wutSeed = seedsInHole[0];
                    //Debug.Log(wutSeed);
                    seedWasPlanted.Invoke(wutSeed);
                }
                else
                {
                    Debug.Log("Seeds are nott being added to list");
                }
                _HasBeenSeeded = true;
                HoleStateChange(HoleState.SeededClosed);
                break;

            default:
                if (_SeedCount > 1 && seedsInHole.Count > 1)
                {
                    int thisRandomSeed = UnityEngine.Random.Range(0, seedsInHole.Count); // index of randomSeed
                    wutSeed = seedsInHole[thisRandomSeed];
                    _HasBeenSeeded = true;
                    HoleStateChange(HoleState.SeededClosed);
                    Debug.Log(wutSeed);
                    seedWasPlanted.Invoke(wutSeed);

                    //Randomize and get one of the seeds
                }
                break;
        }

    }
    private void OnTriggerEnter(Collider other)
    {



        if (other.gameObject.GetComponentInParent<AgaveObject>() != null && other.gameObject.GetComponentInParent<AgaveObject>().agaveObject == AgaveObject.AgaveObjectsInteractables.Seed)
        {

            AgaveObject seed = other.gameObject.GetComponentInParent<AgaveObject>();

            //Debug.Log(other.gameObject.name);

            other.gameObject.layer = 11;//hole
                                        //Change material when inside hole
                                        //Physics.IgnoreCollision(seed.col, groundColliderTest, true);
                                        //if (_SeedCount > 0 && !seedsInHole.Contains(seed))// if seed count is grater than 0, and if the seed is not in the list 
                                        //{
                                        //    HoleStateChange(HoleState.SeededOpen);

            //}
            if (!seedsInHole.Contains(seed))
            {
                seedsInHole.Add(seed);
                _SeedCount += 1;
            }
            seed.gameObject.SetActive(false);
            StartCoroutine(SeedWasPlanted(secondsToWaitEvent));

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<AgaveObject>() != null && other.gameObject.GetComponent<AgaveObject>().agaveObject == AgaveObject.AgaveObjectsInteractables.Seed)
        {
            AgaveObject seed = other.gameObject.GetComponent<AgaveObject>();

            other.gameObject.layer = 8;// grab
                                       // Physics.IgnoreCollision(seed.col, groundColliderTest, false);
            if (_SeedCount > 0)
            {
                seedsInHole.Remove(seed);
                _SeedCount -= 1;
            }
            else
            {
                HoleStateChange(HoleState.Open);
            }
        }
    }




    IEnumerator SeedWasPlanted(float seconds) // Thow this when there is a planted action
    {

        yield return new WaitForSeconds(seconds);
        CloseHole();

        TaskDone();
    }
}
