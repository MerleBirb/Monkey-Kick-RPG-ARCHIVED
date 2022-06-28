// Merle Roji 6/28/22

using UnityEngine;

namespace MonkeyKick.Sprites
{
    /// <summary>
    /// Rotates the sprite based on camera x-axis.
    /// 
    /// Notes:
    /// 
    /// </summary>

    public class SpriteBillboard : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            Billboard();
        }

        private void Billboard()
        {
            transform.rotation = _mainCamera.transform.rotation;
        }
    }
}
