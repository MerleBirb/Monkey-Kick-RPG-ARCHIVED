//===== BILLBOARD SPRITE =====//
/*
5/11/21
Description: 
- Makes the sprite rotate and face the camera. 

Author: Merlebirb
*/

using UnityEngine;

namespace MonkeyKick.Effects
{
    public class BillboardSprite : MonoBehaviour
    {
        private Camera _mainCamera; // save the main camera

        // Start is called before the first frame update
        private void Start()
        {
            _mainCamera = Camera.main;
        }

        // LateUpdate is called once at the end of each frame
        private void LateUpdate()
        {
            if (transform.rotation != _mainCamera.transform.rotation)
            {
                transform.rotation = _mainCamera.transform.rotation;
            }
        }
    }

}
