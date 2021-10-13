// Merle Roji
// 10/12/21

using UnityEngine;

namespace MonkeyKick.QualityOfLife
{
    public struct ParabolaData
    {
        public readonly Vector3 InitialVelocity;
        public readonly float TimeToTarget;

        public ParabolaData(Vector3 initialVelocity, float timeToTarget)
        {
            this.InitialVelocity = initialVelocity;
            this.TimeToTarget = timeToTarget;
        }
    }
}
