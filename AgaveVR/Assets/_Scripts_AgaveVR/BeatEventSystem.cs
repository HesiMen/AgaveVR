using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[Serializable]
public class MyTasksEvents : UnityEvent { }
public class BeatEventSystem : MonoBehaviour
{
    public PlayableDirector daylight;


    [Header("Beat Events Go Here")]
    [Tooltip("Add the BeatEvents Here!")]// writing this because I(hesi)forgot where to add it
    public BeatEvent[] beatEvents;

    [Header("Events for learning how to grab objects")]
    public MyTasksEvents HeldObject;

    [Header("Events For Learning How To Plant")]
    public MyTasksEvents HoldingSeeds;
    public MyTasksEvents PlantingSeeds;
    public MyTasksEvents PlantingReward;
    public MyTasksEvents RewardInStonePlanting;
    [Header("Events For Learning How To MakeFire")] // Should also teach Weather System Here
    public MyTasksEvents FiveSticks;
    public MyTasksEvents FireStickAndStickReady;
    public MyTasksEvents StickToMakeFire;
    public MyTasksEvents FireReward;
    public MyTasksEvents RewardInStoneFire;

    [Header("Events For Learning How To MakeFire")]
    public MyTasksEvents EatingReward;
    public MyTasksEvents RewardInStoneEating;

    private void Start()
    {
        StartCoroutine(WaitSomeSecond());
    }
    private void OnEnable()
    {
        foreach (BeatEvent beat in beatEvents)
        {
            beat.completeEvent += TaskCompleted;
        }
    }

    private void OnDisable()
    {
        foreach (BeatEvent beat in beatEvents)
        {
            beat.completeEvent -= TaskCompleted;
        }
    }



    private void TaskCompleted(BeatEvent.Beats whichBeat, BeatEvent.WhichTask whichTask)
    {
        Debug.Log(whichBeat);
        Debug.Log(whichTask);
        switch (whichBeat)
        {

            case BeatEvent.Beats.IntroTeachingGrab:

                switch (whichTask)
                {
                    case BeatEvent.WhichTask.HeldObject:
                        PlayerSoundManager.i.PlaySoundSimple(PlayerSoundManager.i.taskCompleteString, transform.position);

                        HeldObject.Invoke();
                        break;

                }

                break;

            case BeatEvent.Beats.IntroTeachingPlanting:

                switch (whichTask)
                {
                    case BeatEvent.WhichTask.HoldingSeeds:
                        HoldingSeeds.Invoke();
                        Debug.Log("HoldingSeedsDone");
                        break;

                    case BeatEvent.WhichTask.PlantingSeeds:
                        Debug.Log("PlantingSeedsDone");
                        PlantingSeeds.Invoke();
                        break;

                    case BeatEvent.WhichTask.PlantingReward:
                        PlantingReward.Invoke();
                        break;

                    case BeatEvent.WhichTask.RewardInStonePlanting:
                        RewardInStonePlanting.Invoke();
                        PlayerSoundManager.i.PlaySoundSimple(PlayerSoundManager.i.taskCompleteString, transform.position);

                        break;

                }
                break;

            case BeatEvent.Beats.TeachingMakingFire:

                switch (whichTask)
                {
                    case BeatEvent.WhichTask.FiveSticks:
                        Debug.Log("Ready To Make Fire");
                        FiveSticks.Invoke();
                        break;

                    case BeatEvent.WhichTask.FireStickAndStickReady:
                        FireStickAndStickReady.Invoke();
                        break;

                    case BeatEvent.WhichTask.FireReward:
                        FireReward.Invoke();
                        break;

                    case BeatEvent.WhichTask.RewardInStoneFire:
                        PlayerSoundManager.i.PlaySoundSimple(PlayerSoundManager.i.taskCompleteString, transform.position);

                        RewardInStoneFire.Invoke();
                        break;
                }

                break;

            case BeatEvent.Beats.TeachingEating:

                switch (whichTask)
                {
                    case BeatEvent.WhichTask.EatingReward:
                        EatingReward.Invoke();
                        break;

                    case BeatEvent.WhichTask.RewardInStoneEat:
                        PlayerSoundManager.i.PlaySoundSimple(PlayerSoundManager.i.taskCompleteString, transform.position);

                        RewardInStoneEating.Invoke();
                        break;

                }
                break;



            case BeatEvent.Beats.TeachingComplete:

                break;
        }
    }

    IEnumerator WaitSomeSecond()
    {

        yield return new WaitForSeconds(2f);

        daylight.Pause();
    }
}
