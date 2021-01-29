using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Transition
{
    Smooth,
    Step,
    Cut,
    None
};
public enum CutAnimationType
{
    ToBeginning,
    ToEnd,
    ToSetPoint,
    None
}
public class AnimateBlendShapes : MonoBehaviour
{
    [Header("General")]
    [Tooltip("Select Animation Type | Smooth Animation Type: Smoothly transitions between blendMin and blendMax the starting point and ending points of the blend shape transitions. | Step Animation Type: Allows you to jump in increments throughout the blend shape animation, set your increment count in the step increment variable. | Cut Animation Type: Cut allows you to jump to the end and begining states of the blend shape animation, you can also predefine a cut point using the setCutPoint variable.")]
    [SerializeField]
    private Transition transition = Transition.None;

    [Tooltip("Select the blend shape animation you want to use. Use this to select between different blend shape transitions if your object has different blend shape animations")]
    public int blendShapeIndex = 0;

    [Tooltip("Current blend shape animation state for object.")]
    [Range(0,100)] public int blendState;
    [Tooltip ("The minimum blend shape state the object can be at. Change this to change the starting point of the blend shape animation.")]
    [Range(0,100)] public int blendMin = 0;
    [Tooltip ("The maximum blend shape state the object can be at. Change this to change the ending point of the blend shape animation.")]
    [Range(0,100)] public int blendMax = 100;


//Smooth Animation Transition
    [Header("Smooth Animation")]
        [Tooltip("The rate at which the blend shape animation will play. The lower the value the slower the animation will play, the higher the value, the faster the animation will play. Negative numbers will play the animation in reverse. Zero will pause the animation.")]
        [SerializeField] [Range(-2,2)] private float shapeBlendRate;
        [System.NonSerialized] private float blendTime = 0;

        [Tooltip("If checked, Ping Pong, allows animation to be in a looped state playing from initial state to end state and then from end state to initial state back and forth")]
        [SerializeField] private bool pingPong;

// Step Animation Transition
    [Header("Step Animation")]
        [Tooltip("Step Increment is a variable that is used to do a stutter animation and 'skip' frames.")]
        [SerializeField] [Range (0,100)]private int stepIncrement;
        [HideInInspector] public bool stepEnd, stepInitial;
        //[SerializeField] private bool stepTime;
        //[SerializeField] private int stepRate;

    [Header("Cut Animation")]
        [Tooltip("Select cut animation type | ToEnd: Jump the end of the blend shape animation set by the blendMax variable. | ToBeginning: Jump to the beginning of the blend shape animation set by the blendMax variable. | ToSetPoint: set the specific state in the blendshape animation to jump to.")]
        [SerializeField] public CutAnimationType cutAnimationType = CutAnimationType.None;
        [SerializeField] [Range(0,100)] private int setCutPoint;

    // Start is called before the first frame update
    /*void Start()
    {

    }*/

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(blendShapeIndex, blendState);
        
        // Selects Animation Type for blendshapes
        switch (transition)
        {
            case Transition.Smooth:
                SmoothAnimation();
                break;
            case Transition.Step:
                StepAnimation();
                break;
            case Transition.Cut:
                CutAnimation();
                break;
            case Transition.None:
                break;
            default:
                Debug.LogWarning("Select a type of blend shape transition for: " + this.gameObject.name + ".");
                break;
        }
    }

    // Smooth Animation / Transition Between Set Blend Shapes in Blend Shape Index
    private void SmoothAnimation()
    {
        blendState = (int)Mathf.Lerp(blendMin, blendMax, blendTime);
        blendTime += shapeBlendRate * Time.deltaTime;

        if (pingPong)
        {
            if (blendTime > 1.0f)
                shapeBlendRate *= -1;

            if (blendTime < 0.0f)
                shapeBlendRate *= -1;
        }

        //Allows for ping pong to be used after the animation has already been started
        if (blendTime > 1.0f)
            blendTime = 1.0f;
        if (blendTime < 0.0f)
            blendTime = 0.0f;
    }

    // Step Animation sets blend shape state at intervals (stutter animation)
    private void StepAnimation()
    {
        DebugStepInput();
        //Go to Blended Shape State Size
        if (stepEnd && blendState <= blendMax - stepIncrement)
        {
            blendState += stepIncrement;
            stepEnd = false;
        }

        //Go to Initial State
        else if (stepInitial && blendState >= blendMin + stepIncrement)
        {
            blendState -= stepIncrement;
            stepInitial = false;
        }

    }

    // Cuts directly to a specific point in the animation
    private void CutAnimation()
    {
        switch(cutAnimationType)
        {
            case CutAnimationType.ToBeginning:
                blendState = blendMin;
                break;
            case CutAnimationType.ToEnd:
                blendState = blendMax;
                break;
            case CutAnimationType.ToSetPoint:
                blendState = setCutPoint;
                break;
            case CutAnimationType.None:
                break;
            default:
                break;
        }
    }

    private void DebugStepInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            stepEnd = true;

        
        if (Input.GetKeyDown(KeyCode.UpArrow))
            stepInitial = true;
    }
}
