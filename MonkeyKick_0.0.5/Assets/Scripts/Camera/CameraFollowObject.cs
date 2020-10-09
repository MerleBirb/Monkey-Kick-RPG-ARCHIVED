using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    public Transform follow;
    public Vector3 offset;

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = new Vector3(follow.position.x, follow.position.y + offset.y, follow.position.z - offset.z);
    }
}
