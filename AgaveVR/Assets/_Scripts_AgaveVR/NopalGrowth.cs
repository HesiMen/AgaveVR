using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NopalGrowth : MonoBehaviour
{

    /// <summary>
    /// start growing
    /// once it stops growin spawn a new leaf
    /// randomize how many times to iterate
    /// 
    /// /// </summary>
    /// 
    public float startScale = 0f;
    public float endScale = .075f;
    public bool imRoot = false;
    public bool imLast = false;
    public float animatioTime;
    public Transform spawnPointParent;

    public Transform[] spawnPoints;

    public GameObject leafPrefab;
    public GameObject pearObject;

    public GameObject activateGesture;

    public GameObject rootObject;
    public Vector2 rangeMaxInst;
    private int maxInst;
    private int rootCount = 0;



    private void Start()
    {
        spawnPoints = spawnPointParent.GetComponentsInChildren<Transform>();

        StartGrowing();
    }

    public void StartGrowing()
    {

        if (imRoot) // for testing
        {
            ScaleAnimation(transform);
            maxInst = Random.Range((int)rangeMaxInst.x, (int)rangeMaxInst.y);

        }
    }


    public Transform GetRandomSpawn()
    {
        Transform randTransform;

        int randInt = Random.Range(1, spawnPoints.Length);
        //Debug.Log(randInt);
        randTransform = spawnPoints[randInt];
        return randTransform;
    }

    public void ScaleAnimation(Transform trans, bool onComplete = true)
    {
        Vector3 evenScale = new Vector3(endScale, endScale, endScale);
        if (onComplete)
            trans.DOScale(evenScale, animatioTime).OnComplete(EndOfScale);
        else
        {
            trans.DOScale(evenScale, animatioTime);
        }
    }

    private void EndOfScale()
    {

        IntantiateNow();
    }

    public void IntantiateNow()
    {
        if (!imLast)
        {

            Transform spawnTransform = GetRandomSpawn();
            if (GetComponentInParent<NopalGrowth>().imRoot)
                rootObject = GetComponentInParent<NopalGrowth>().gameObject;


            if (rootObject != null)
            {

                var newLeaf = Instantiate(leafPrefab, spawnTransform.position, spawnTransform.rotation, rootObject.transform);
                NopalGrowth newLeafNopal = newLeaf.GetComponent<NopalGrowth>();
                newLeafNopal.leafPrefab = rootObject.GetComponentInParent<NopalGrowth>().leafPrefab;
                newLeafNopal.pearObject = rootObject.GetComponentInParent<NopalGrowth>().pearObject;
                newLeafNopal.activateGesture = rootObject.GetComponentInParent<NopalGrowth>().activateGesture;


                newLeafNopal.rootObject = rootObject;
                rootObject.GetComponent<NopalGrowth>().AddOneInRoot(newLeafNopal);

                newLeafNopal.ScaleAnimation(newLeafNopal.transform);
            }
            else
            {
                Debug.Log("didnt find root " + transform.GetComponentInParent<NopalGrowth>().name);
            }

        }
        else
        {
            Transform spawnTransform = GetRandomSpawn();

            var newPear = Instantiate(pearObject, spawnTransform.position, spawnTransform.rotation, rootObject.transform);

            ScaleAnimation(newPear.transform, false);

            activateGesture.SetActive(true);
        }

    }


    public void AddOneInRoot(NopalGrowth lastNopal)
    {
        if (imRoot)
        {
            rootCount += 1;

            if (rootCount >= maxInst)
            {
                lastNopal.imLast = true;
            }
        }

    }


}
