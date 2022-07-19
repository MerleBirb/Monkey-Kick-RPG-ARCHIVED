// Merle Roji 7/18/22

using UnityEngine;

namespace MonkeyKick
{
    public struct ParabolaData
    {
        public readonly Vector3 InitialVelocity;
        public readonly float TimeToTarget;

        public ParabolaData(in Vector3 initialVelocity, in float timeToTarget)
        {
            this.InitialVelocity = initialVelocity;
            this.TimeToTarget = timeToTarget;
        }
    }
}
