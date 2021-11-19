// Merle Roji
// 10/15/21

using UnityEngine;
using MonkeyKick.Managers;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.Cameras
{
    public class CameraManager : MonoBehaviour
    {
        #region CAMERAS

        [Header("Spawn this camera when in the Overworld State")]
        [SerializeField] private GameObject overworldCamera;

        [Header("Spawn this camera when in the Battle State")]
        [SerializeField] private GameObject battleCamera;

        #endregion

        #region UI

        [Header("Spawn the Battle UI when battle starts")]
        [SerializeField] private GameObject battleUI;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            CameraQoL.OnBattleStart += InitializeBattleCamera;
            CameraQoL.OnBattleEnd += InitializeOverworldCamera;
        }

        private void OnDestroy()
        {
            CameraQoL.OnBattleStart -= InitializeBattleCamera;
            CameraQoL.OnBattleEnd -= InitializeOverworldCamera;
        }

        #endregion

        #region INITIALIZE CAMERA METHODS

        /// <summary>
        /// Initializes the overworld camera, and de-activates any other camera.
        /// </summary>
        private void InitializeOverworldCamera()
        {
            overworldCamera?.SetActive(true);
            battleCamera?.SetActive(false);
            battleUI?.SetActive(false);
        }

        /// <summary>
        /// Initializes the battle camera, and de-activates any other camera.
        /// Sets the battle camera's position to a new position.
        /// </summary>
        /// <param name="camPos"></param>
        private void InitializeBattleCamera(Vector3 camPos)
        {
            battleCamera?.SetActive(true);
            overworldCamera?.SetActive(false);

            battleCamera.transform.position = new Vector3(camPos.x, camPos.y, camPos.z);
            battleUI?.SetActive(true);
        }

        #endregion
    }
}
