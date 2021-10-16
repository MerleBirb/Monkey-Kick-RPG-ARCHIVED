// Merle Roji
// 10/11/21

using UnityEngine;
using UnityEngine.UI;

namespace MonkeyKick.Cameras
{
    public class PixelatedCamera : MonoBehaviour
    {
        private Camera _renderCamera;
        private RenderTexture _renderTexture;
        private int _screenWidth, _screenHeight;

        [Header("Screen scaling settings")]
        public PixelScreenMode Mode;
        public ScreenSize TargetScreenSize = new ScreenSize { width = 256, height = 144 }; // only use with Mode = PixelScreenMode.Resize;
        public uint ScreenScaleFactor = 1; // only use with Mode = PixelScreenMode.Scale;
        //public RenderTexture ScreenTexture;

        [Header("Display")]
        public RawImage Display;

        #region UNITY METHODS

        private void Start()
        {
            // initialize the Pixelated Camera system
            Init();
        }

        private void Update()
        {
            // if screen resized, initialize again
            if (CheckScreenResize()) Init();
        }

        #endregion

        #region METHODS

        public void Init()
        {
            // initialize camera and get screen size values
            if (!_renderCamera) _renderCamera = GetComponent<Camera>();
            _screenWidth = Screen.width;
            _screenHeight = Screen.height;

            // prevent going below 1 error
            if (ScreenScaleFactor < 1) ScreenScaleFactor = 1;
            if (TargetScreenSize.width < 1) TargetScreenSize.width = 1;
            if (TargetScreenSize.height < 1) TargetScreenSize.height = 1;

            // calculate render texture size
            int width = Mode == PixelScreenMode.Resize ? (int)TargetScreenSize.width : _screenWidth / (int)ScreenScaleFactor;
            int height = Mode == PixelScreenMode.Resize ? (int)TargetScreenSize.height : _screenHeight / (int)ScreenScaleFactor;

            // initialize render texture
            //ScreenTexture.depth = 24;
            //ScreenTexture.filterMode = FilterMode.Point;
            //ScreenTexture.antiAliasing = 1;

            _renderTexture = new RenderTexture(width, height, 24)
            {
                filterMode = FilterMode.Point,
                antiAliasing = 1 // no AA
            };

            // set render texture as the camera's output
            _renderCamera.targetTexture = _renderTexture;

            // attach texture to the display UI RawImage
            Display.texture = _renderTexture;
        }

        // checks whether screen has been resized or not
        public bool CheckScreenResize() => Screen.width != _screenWidth || Screen.height != _screenHeight;

        #endregion
    }
}
