//===== BILLBOARD SPRITE =====//
/*
5/11/21
Description: 
- Makes the sprite rotate and face the camera. 

*/

using UnityEngine;

namespace Merlebirb.SpriteEffects
{
    public class BillboardSprite : MonoBehaviour
    {
        private Camera mainCam; // save the main camera

        // Start is called before the first frame update
        private void Start()
        {
            mainCam = Camera.main;
        }

        // LateUpdate is called once at the end of each frame
        private void LateUpdate()
        {
            if (transform.rotation != mainCam.transform.rotation)
            {
                transform.rotation = mainCam.transform.rotation;
            }
        }
    }

}
