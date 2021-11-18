// Merle Roji
// 11/18/21

using UnityEngine;
using MonkeyKick.Cameras;

namespace MonkeyKick.QualityOfLife
{
    public static class CameraQoL
    {
        public delegate void BattleStartTrigger(Vector3 camPos);
        public static event BattleStartTrigger OnBattleStart;
        public static void InvokeOnBattleStart(Vector3 camPos)
        {
            OnBattleStart?.Invoke(camPos);
        }


        public delegate void BattleEndTrigger();
        public static event BattleEndTrigger OnBattleEnd;
        public static void InvokeOnBattleEnd()
        {
            OnBattleEnd?.Invoke();
        }
    }
}
