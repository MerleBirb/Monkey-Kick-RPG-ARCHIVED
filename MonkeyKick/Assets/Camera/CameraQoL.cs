// Merle Roji
// 11/18/21

using UnityEngine;

namespace MonkeyKick.Cameras
{
    public static class CameraQoL
    {
        public delegate void BattleStartTrigger(Vector3 camPos);
        public static event BattleStartTrigger OnBattleStart;

        /// <summary>
        /// Invokes the 'OnBattleStart' event.
        /// </summary>
        /// <param name="camPos"></param>
        public static void InvokeOnBattleStart(Vector3 camPos)
        {
            OnBattleStart?.Invoke(camPos);
        }


        public delegate void BattleEndTrigger();
        public static event BattleEndTrigger OnBattleEnd;

        /// <summary>
        /// Invokes the 'OnBattleEnd' event.
        /// </summary>
        public static void InvokeOnBattleEnd()
        {
            OnBattleEnd?.Invoke();
        }
    }
}
