using Godot;
using System;

namespace Merlebirb.Managers
{
    //===== CAMERA TARGET AND FOLLOW TARGET =====//
    /*
    by: Merlebirb, 3/25/21

    Description: Logic that makes the camera follow the player at a specific angle and distance.

    WIP

    */

    public class CameraFollowTarget : Spatial
    {
        private Spatial target; // the target the camera will follow
        [Export] private Vector3 distance; // the distance from the target

        // Called every frame. 'delta' is the elapsed time since the previous frame. Better for physics.
        public override void _PhysicsProcess(float delta)
        {
            FollowTarget();
        }

        private void FollowTarget()
        {
            Translation = target.Translation + distance;
        }
    }

}

