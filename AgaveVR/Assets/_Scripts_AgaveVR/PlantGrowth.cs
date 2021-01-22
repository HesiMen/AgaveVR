using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlantGrowth : MonoBehaviour
{
    public enum PlantState
    {
        Baby,
        Young,
        Adult,
        Old,
        None,
    }
    public PlantState currPlantState;
    public bool usingMultipleModels;
    public Transform[] objectToScale = new Transform[4];
    public Transform[] scaleStages = new Transform[4];

    public float[] growthTime = new float[4];

    //public Material[] // I need to do the fade in fade out
    // Start is called before the first frame update

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePlantState(PlantState.Baby);
        }
    }
    public virtual void ChangePlantState(PlantState plantState)
    {
        currPlantState = plantState;
        Debug.Log((int)currPlantState);
        if (!usingMultipleModels)
        {

            ActivateCurrentDeactivatePrev(0);
            GrowingObjectInStages(objectToScale[0], 0, growthTime[0]);


        }

        else
        {


            switch (plantState)
            {//Here we can add feedback sounds, visuals, etc.
                case PlantState.Baby:

                    break;
                case PlantState.Young:

                    break;
                case PlantState.Adult:

                    break;
                case PlantState.Old:

                    break;
            }
            ActivateCurrentDeactivatePrev((int)currPlantState);
            GrowingObjectInStages(objectToScale[(int)currPlantState], (int)currPlantState, growthTime[(int)currPlantState]);

        }
    }

    private void ActivateCurrentDeactivatePrev(int curr)
    {

        //Debug.Log(curr);
        if (curr == 0)
        {
            objectToScale[curr].gameObject.SetActive(true);
        }
        else if (curr < 4)
        {
            if (!objectToScale[curr].gameObject.activeSelf && objectToScale[curr - 1].gameObject.activeSelf)
            {
                objectToScale[curr].gameObject.SetActive(true);
                objectToScale[curr - 1].gameObject.SetActive(false);
            }
        }
    }

    public virtual void GrowingObjectInStages(Transform objToScale, int whichStage, float scaleTime) // Method to 
    {
        if (!objToScale.gameObject.activeSelf && objectToScale != null)
        {
            objToScale.gameObject.SetActive(true);
        }
        else
        {

        }
        ScalingObject(objToScale, scaleStages[whichStage], scaleTime);


    }

    public virtual void ScalingObject(Transform objToScale, Transform scaleTo, float timeOfAnimation) // scaling with Itween
    {
        objToScale.DOScale(scaleTo.localScale, timeOfAnimation).OnComplete(EndOfScaling);
    }

    public virtual void EndOfScaling()
    {





        if ((int)currPlantState < 4 && usingMultipleModels)
        {
            ChangePlantState(currPlantState + 1);
        }


    }

}
