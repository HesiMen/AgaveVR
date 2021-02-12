using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatEvent : MonoBehaviour
{



    public enum Beats { IntroTeachingPlanting, TeachingMakingFire, TeachingCrafting, TeachingEating, TeachingWeather , TeachingComplete}// PlantingCycle, MakingFire, Beat6 }
    public enum WhichTask { HoldingSeeds, PlantingSeeds, PlantingReward, RewardInStonePlanting, FiveSticks, FireStickAndStickReady, StickToMakeFire, FireReward,SeedsInHole, SeedsPlanted,Empty, RewardInStoneFire, EatingReward,RewardInStoneEat }

    public Beats whichBeat;
    public WhichTask whichTask;

    public delegate void TaskComplete(Beats beat, WhichTask task);
    public event TaskComplete completeEvent;




    public void TaskDone()
    {

        if (completeEvent != null)
        {
            completeEvent(whichBeat, whichTask);
        }
    }
}
