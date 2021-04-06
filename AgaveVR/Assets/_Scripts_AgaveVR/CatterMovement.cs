using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CatterMovement : MonoBehaviour
{

    Rigidbody rb;

    public Vector2 rangeBetweenpush = new Vector2(.5f, 1.5f);

    private float timerCount;
    private float randomPush;

    public bool held;

    public float force = 300f;
    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!held)
        {
            timerCount += Time.deltaTime;

            if (timerCount > randomPush)
            {
                RandomRotateNow();

                rb.AddRelativeForce(transform.forward * force);

                randomPush = Random.Range(rangeBetweenpush.x, rangeBetweenpush.y);
                timerCount = 0f;


            }
        }
    }


    private void RandomRotateNow()
    {
        Vector3 rotation = new Vector3(0f, Random.Range(-20f, 20f), 0f);

        transform.DORotate(rotation, .5f);
    }


    public void IsheldNow(bool nowHeld)
    {
        held = nowHeld;

    }
}
