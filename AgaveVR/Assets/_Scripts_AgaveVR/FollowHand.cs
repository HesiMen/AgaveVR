using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHand : MonoBehaviour
{
    Transform currFollow;
    bool follow = false;
   
    public void FollowNow(Transform whoToFollow)
    {

        currFollow = whoToFollow;
        follow = true;
    }


    public void StopFollowing()
    {
        follow = false;
    }
    private void Update()
    {
        if (follow)
        {
            transform.position = currFollow.position;
        }
    }


}
