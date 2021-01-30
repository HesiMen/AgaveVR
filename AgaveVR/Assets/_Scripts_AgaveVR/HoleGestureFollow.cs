using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleGestureFollow : MonoBehaviour
{
    //[SerializeField] Transform limitBox;
    [SerializeField] Transform chasePos;

    public Transform minZRange;
    public Transform maxZRange;


    // [SerializeField] Transform ogParent;
    private float zMin, zMax;
    private float yPos, xPos;

    private void Start()
    {
        zMin = minZRange.position.z ;

        zMax = maxZRange.position.z;

        yPos = transform.position.y;// initialPos
        xPos = transform.position.x;

        Debug.Log(zMax);
        Debug.Log(zMin);



        //transform.parent = chasePos;
    }

    // Update is called once per frame

    private void Update()
    {

        Vector3 pointA = new Vector3(transform.position.x, transform.position.y, zMin);
        Vector3 pointB = new Vector3(transform.position.x, transform.position.y, zMax);

        Debug.DrawLine(pointA, pointB, Color.red);

        float zPos = Mathf.Clamp(chasePos.position.z, zMin, zMax);

        transform.position = new Vector3(xPos, yPos, zPos);
    }
    


}
