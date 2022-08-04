// Merle Roji 8/4/22

using UnityEngine;

namespace MonkeyKick.Skills
{
    public class SAChangeColorOverTime : StateAction
    {
        private SpriteRenderer _renderer; // the animator that will play the new animation
        private Color _originalColor;
        private Color _color; // color to change to
        private float _timeLimit; // time it'll take for the color to change
        private float _currentTime = 0f;

        public SAChangeColorOverTime(SpriteRenderer renderer, Color color, float timeLimit)
        {
            _renderer = renderer;
            _originalColor = _renderer.color;
            _color = color;
            _timeLimit = timeLimit;
            _currentTime = 0f;
        }

        public override bool Execute()
        {
            _renderer.color = Color.Lerp(_originalColor, _color, _currentTime);

            if (_currentTime < 1)
            {
                _currentTime += Time.deltaTime / _timeLimit;
            }

            return false;
        }
    }
}

