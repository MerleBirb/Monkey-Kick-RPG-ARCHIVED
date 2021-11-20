// Merle Roji
// 11/20/21

using UnityEngine;
using TMPro;

namespace MonkeyKick
{
    public class FPSCounter : MonoBehaviour
    {
        private TextMeshProUGUI _fpsCounter;

        // Start is called before the first frame update
        void Start()
        {
            _fpsCounter = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            float current = 0f;
            current = (int)(1f / Time.unscaledDeltaTime);
            _fpsCounter.text = "FPS: " + current;
        }
    }
}
