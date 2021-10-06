// Merle Roji
// 10/6/21

using UnityEngine;

namespace MonkeyKick.PhysicalObjects
{
    public class SpriteBillboard : MonoBehaviour
    {
        private Camera _mainCam;

        private void Start()
        {
            _mainCam = Camera.main;
        }

        private void LateUpdate()
        {
            transform.rotation = _mainCam.transform.rotation;
        }
    }
}
