// Merle Roji
// 10/15/21

using UnityEngine;
using MonkeyKick.Managers;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.Cameras
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        #region CAMERAS

        [Header("Spawn when in the Overworld State")]
        [SerializeField] private GameObject overworldCamera;

        [Header("Spawn when in the Battle State")]
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

        #region CAMERA METHODS

        private void InitializeOverworldCamera()
        {
            overworldCamera?.SetActive(true);
            battleCamera?.SetActive(false);
            battleUI?.SetActive(false);
        }

        private void InitializeBattleCamera(Vector3 camPos)
        {
            battleCamera?.SetActive(true);
            overworldCamera?.SetActive(false);

            battleCamera.transform.position = new Vector3(camPos.x, camPos.y, camPos.z);
            battleUI?.SetActive(true);
        }

        private void InitiateCameras()
        {
            switch (gameManager.GameState)
            {
                case GameStates.Overworld:
                    {
                        overworldCamera.SetActive(true);
                        battleCamera.SetActive(false);
                        battleUI.SetActive(false);

                        break;
                    }
                case GameStates.Battle:
                    {
                        overworldCamera.SetActive(false);
                        battleCamera.SetActive(true);

                        Vector3 overworldCamPos = overworldCamera.transform.position;
                        battleCamera.transform.position = new Vector3(overworldCamPos.x, overworldCamPos.y, overworldCamPos.z);
                        battleUI.SetActive(true);

                        break;
                    }
            }
        }

        private void InitiateCameras(Vector3 camPos)
        {
            switch (gameManager.GameState)
            {
                case GameStates.Overworld:
                    {
                        overworldCamera.SetActive(true);
                        battleCamera.SetActive(false);
                        battleUI.SetActive(false);

                        break;
                    }
                case GameStates.Battle:
                    {
                        overworldCamera.SetActive(false);
                        battleCamera.SetActive(true);

                        battleCamera.transform.position = new Vector3(camPos.x, camPos.y, camPos.z);
                        battleUI.SetActive(true);

                        break;
                    }
            }
        }

        #endregion
    }
}
