using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject projectile;
    public float forceAdd = 50f;

    public float timming= 4f;
    float timer = 0f;
    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer > timming)
        {
            var prj = Instantiate(projectile, transform.position, Quaternion.identity);
            prj.GetComponent<Rigidbody>().AddRelativeForce(transform.up * forceAdd);
            timer = 0f;

            
        }
    }


}
