// Merle Roji
// 10/11/21

namespace MonkeyKick.Cameras
{
    public enum PixelScreenMode { Resize, Scale }

    [System.Serializable]
    public struct ScreenSize
    {
        // int vector2 to store screen size desired
        public int width;
        public int height;
    }
}
