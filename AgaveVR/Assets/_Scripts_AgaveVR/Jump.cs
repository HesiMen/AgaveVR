using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Jump : MonoBehaviour
{
    Rigidbody rb;

    public float angle = 90f;
    public float force = 300f;
    public Vector2 rangeBetweenJump = new Vector2(2f, 7f);

    private float timerCount;
    private float randomJump;

    public bool held;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        randomJump = Random.Range(rangeBetweenJump.x, rangeBetweenJump.y);

    }

    private void Update()
    {
        if (!held)
        {
            timerCount += Time.deltaTime;

            if (timerCount > randomJump)
            {
                RandomRotateNow();
                AddForceAtAngle();

                randomJump = Random.Range(rangeBetweenJump.x, rangeBetweenJump.y);
                timerCount = 0f;


            }
        }

    }

    private void RandomRotateNow()
    {
        Vector3 rotation = new Vector3(0f, Random.Range(0f, 350f), 0f);

        transform.DORotate(rotation, 1f);
    }


    public void IsheldNow(bool nowHeld)
    {
        held = nowHeld;

    }

    public void AddForceAtAngle()
    {
        Vector3 dir = Quaternion.AngleAxis(angle, transform.up) * Vector3.one;
       // Debug.Log(dir);
        rb.AddRelativeForce(dir * force);
    }
}
