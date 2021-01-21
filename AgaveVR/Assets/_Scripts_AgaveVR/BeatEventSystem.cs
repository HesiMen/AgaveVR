using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class MyTasksEvents : UnityEvent { }
public class BeatEventSystem : MonoBehaviour
{
    [Header("Beat Events Go Here")]
    [Tooltip ("Add the BeatEvents Here!")]// writing this because I(hesi)forgot where to add it
    public BeatEvent[] beatEvents;

    public MyTasksEvents HoldingSeeds;
    public MyTasksEvents PlantingSeeds;
    public MyTasksEvents PlantingReward;
    public MyTasksEvents RewardInStone;
    public MyTasksEvents FiveSticks;
    public MyTasksEvents FireStickAndStickReady;
    public MyTasksEvents StickToMakeFire;
    public MyTasksEvents FireReward;



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
            case BeatEvent.Beats.IntroTeachingPlanting:

                switch (whichTask)
                {
                    case BeatEvent.WhichTask.HoldingSeeds:
                        HoldingSeeds.Invoke();
                        Debug.Log("HoldingSeedsDone");
                        break;

                    case BeatEvent.WhichTask.PlantingSeeds:
                        PlantingSeeds.Invoke();
                        break;

                    case BeatEvent.WhichTask.PlantingReward:
                        PlantingReward.Invoke();
                        break;

                    case BeatEvent.WhichTask.RewardInStone:
                        RewardInStone.Invoke();
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
                }

                break;

            case BeatEvent.Beats.TeachingCrafting:

                break;

            case BeatEvent.Beats.PlantingCycle:

                break;

            case BeatEvent.Beats.Beat5:

                break;


            case BeatEvent.Beats.Beat6:

                break;
        }
    }
}
