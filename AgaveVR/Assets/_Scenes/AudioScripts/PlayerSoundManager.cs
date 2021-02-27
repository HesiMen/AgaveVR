using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerSoundManager : BaseSoundManager
{

    public static PlayerSoundManager i;

    [FMODUnity.EventRef]
    public string eatingPlantString, eatingBugString, stepsString, seedSpawnString, grabSeedsString, grabbingObjectString, hungerString, teethChatteringString;

   
    //public List<string> bankPath;


    // public Rigidbody playerRB;
    


    FMOD.Studio.EventInstance eatingPlant;
    FMOD.Studio.EventInstance eatingBug;
    FMOD.Studio.EventInstance steps;
    FMOD.Studio.EventInstance seedSpawn;
    FMOD.Studio.EventInstance seedGrab;
    FMOD.Studio.EventInstance grabbingObject;
    FMOD.Studio.EventInstance teethChattering;
    FMOD.Studio.EventInstance hunger;

   
    public KeyCode[] pressForSound;

    [Range(1, 40)]
    public int timeTillNextSound;


    private void Awake()
    {
        i = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //Create Instances for sound events


        //eatingPlant = FMODUnity.RuntimeManager.CreateInstance(eatingPlantString);
        //eatingBug = FMODUnity.RuntimeManager.CreateInstance(eatingBugString);
        //steps = FMODUnity.RuntimeManager.CreateInstance(stepsString);
        //seedSpawn = FMODUnity.RuntimeManager.CreateInstance(seedSpawnString);
        //seedGrab = FMODUnity.RuntimeManager.CreateInstance(grabSeedsString);
        //grabbingObject = FMODUnity.RuntimeManager.CreateInstance(grabbingObjectString);
        //teethChattering = FMODUnity.RuntimeManager.CreateInstance(teethChatteringString);
        //hunger = FMODUnity.RuntimeManager.CreateInstance(hungerString);


        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(eatingPlant, transform);
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(eatingBug, transform);
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(receiveSeeds,transform);
    }

   
    // Update is called once per frame
    void Update()
    {
        // Attach to a game object

        //DebugPlaySound();

        ////Breathing Sounds
        //if ((Mathf.RoundToInt(Time.time) + 1) % timeTillNextSound == 0)
        //{
        //    StartCoroutine(PlaySoundOverTime());
        //}

    }

    void DebugPlaySound()
    {
        if (Input.GetKeyDown(pressForSound[0]))
        {
            //Barking
            //eatingPlant.start();
            PlaySoundSimple(eatingPlantString, transform.position);
            Debug.Log("Played 1");
        }
        if (Input.GetKeyDown(pressForSound[1]))
        {
            //breathing
            //eatingBug.start();
            Debug.Log("Played 2");
        }
        if (Input.GetKeyDown(pressForSound[2]))
        {
            //Whining
            //seedSpawn.start();
            PlaySoundSimple(stepsString, PlayerStateObjects.i.feet.position);
            Debug.Log("Played 3");
        }
    }

    IEnumerator PlaySoundOverTime()
    {
        eatingBug.start();
        yield return new WaitForSeconds(5f);
    }
}
