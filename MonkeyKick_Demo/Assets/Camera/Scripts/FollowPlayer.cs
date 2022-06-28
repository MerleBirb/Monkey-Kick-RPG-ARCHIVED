// Merle Roji 6/28/22

using UnityEngine;

namespace MonkeyKick.Cameras
{
    /// <summary>
    /// Follows a target, preferably the player.
    /// 
    /// Notes:
    /// - Simple, placeholder
    /// 
    /// </summary>

    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _distanceFromTarget;

        private void LateUpdate()
        {
            transform.position = _target.position + _distanceFromTarget;
        }
    }
}
