//===== FOLLOW TARGET =====//
/*
5/22/21
Description:
- Sets the follow target for the cinemachine.

Author: Merlebirb
*/

using UnityEngine;
using Cinemachine;

public class FollowTarget : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    public Transform target; // the object the camera is going to follow

    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        cam.Follow = target;
    }
}
