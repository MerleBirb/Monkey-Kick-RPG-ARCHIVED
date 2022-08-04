// Merle Roji 8/4/22

using UnityEngine;

namespace MonkeyKick.Skills
{
    public class SAChangeColor : StateAction
    {
        private SpriteRenderer _renderer; // the animator that will play the new animation
        private Color _color; // color to change to

        public SAChangeColor(SpriteRenderer renderer, Color color)
        {
            _renderer = renderer;
            _color = color;
        }

        public override bool Execute()
        {
            _renderer.color = _color;

            return true;
        }
    }
}

