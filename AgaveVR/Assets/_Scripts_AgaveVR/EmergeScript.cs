using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

[Serializable]
public class EmergeEvent : UnityEvent { }
public class EmergeScript : MonoBehaviour
{





    public float emergeTime = .5f;
    public Vector2 shakeAmmount;
    public int vibrato = 40;
    public int randomness = 20;
    [SerializeField] Transform rock;

    [SerializeField] ParticleSystem dirt;

    public float endValueY = 1.5f;

    Vector3 directionShake;

    public EmergeEvent EmergeShotStart;
    public EmergeEvent EmergeShotEnd;


    private float startValueY;


    public bool emergeNow = false;
    private void Start()
    {
        directionShake = new Vector3(shakeAmmount.x, 0, shakeAmmount.y);

        startValueY = rock.transform.position.y;
        Debug.Log(startValueY);

        if (emergeNow)
        {
            EmergeNowEventActive();
        }
    }


    public void EmergeNowEventActive()
    {
        Debug.Log("EmergeNow");
        //_emergeNow = true;
        Emerge(endValueY);
        

        dirt.Play();
    }


    public void SubmergeNowEventActive()
    {
        //_emergeNow = true;
        Emerge(startValueY);
        

        dirt.Play();
    }

    private void Emerge(float ammountY)
    {

        
        Sequence emergeSequence = DOTween.Sequence();

        emergeSequence.Append(rock.transform.DOShakePosition(emergeTime, directionShake, vibrato, randomness, false, false))
            .Insert(0, rock.transform.DOMoveY(ammountY, emergeTime)).OnStart(StartedEmerge).OnComplete(FinishedEmerge);


    }

   





    private void FinishedEmerge()
    {
        EmergeShotEnd.Invoke();
        //endLoop = 1;
        // stoneRise.setParameterByName("EndStoneLoop", endLoop);
    }

    private void StartedEmerge()
    {
        EmergeShotStart.Invoke();


        // endLoop = 0;
        // stoneRise.setParameterByName("EndStoneLoop", endLoop);


    }


}
