// Merle Roji
// 10/12/21

using UnityEngine;

namespace MonkeyKick.QualityOfLife
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
