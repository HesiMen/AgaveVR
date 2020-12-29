using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatEvent : MonoBehaviour
{

    

    public enum Beats { Beat1, Beat2, Beat3, Beat4, Beat5, Beat6}

    public enum WhichTask {HoldingSeeds, PlantingSeeds, PlantingReward, RewardInStone , FiveSticks, FireStickAndStickReady , StickToMakeFire, FireReward }

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
