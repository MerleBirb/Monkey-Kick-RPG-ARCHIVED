//===== FOLLOW TARGET =====//
/*
5/22/21
Description:
- Sets the follow target for the cinemachine.

Author: Merlebirb
*/

using UnityEngine;
using Cinemachine;

namespace MonkeyKick.CameraTools
{
    public class FollowTarget : MonoBehaviour
    {
        private CinemachineVirtualCamera cineCamera;

        public Transform Target; // the object the camera is going to follow

        private void Awake()
        {
            cineCamera = GetComponent<CinemachineVirtualCamera>();
            cineCamera.Follow = Target;
        }
    }
}