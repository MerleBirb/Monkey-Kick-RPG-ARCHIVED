// Merle Roji
// 10/15/21

using UnityEngine;
using Cinemachine;
using MonkeyKick.Managers;
using MonkeyKick.UserInterface;

namespace MonkeyKick.Cameras
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        #region CAMERAS

        [Header("Spawn when in the Overworld State")]
        [SerializeField] private CinemachineVirtualCamera overworldCamera;
        private GameObject _overworldCamObject;
        private CameraController _overworldCamController;

        [Header("Spawn when in the Battle State")]
        [SerializeField] private CinemachineVirtualCamera battleCamera;
        private GameObject _battleCamObject;

        #endregion

        #region UI

        [Header("Spawn the Battle UI when battle starts")]
        [SerializeField] private GameObject _battleUI;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            // save the cameras as GameObjects
            _overworldCamObject = overworldCamera.gameObject;
            _overworldCamController = overworldCamera.GetComponent<CameraController>();
            _battleCamObject = battleCamera.gameObject;

            InitiateCameras();
        }

        private void Start()
        {
            gameManager.OnBattleStart += InitiateCameras; // add the InitiateCameras() function to the OnBattleStart event
            gameManager.OnBattleEnd += InitiateCameras; // add the InitiateCameras() function to the OnBattleEnd event
        }

        #endregion

        #region CAMERA METHODS

        private void InitiateCameras()
        {
            switch (gameManager.GameState)
            {
                case GameStates.Overworld:
                    {
                        _overworldCamObject.SetActive(true);
                        _battleCamObject.SetActive(false);
                        _battleUI.SetActive(false);

                        break;
                    }
                case GameStates.Battle:
                    {
                        _overworldCamObject.SetActive(false);
                        Vector3 overworldCamPos = _overworldCamObject.transform.position;

                        _battleCamObject.transform.position = new Vector3(overworldCamPos.x, overworldCamPos.y, overworldCamPos.z);
                        _battleCamObject.SetActive(true);
                        _battleUI.SetActive(true);

                        break;
                    }
            }
        }

        private void InitiateCameras(in Vector3 camPos)
        {
            switch (gameManager.GameState)
            {
                case GameStates.Overworld:
                    {
                        _overworldCamObject.SetActive(true);
                        _battleCamObject.SetActive(false);
                        _battleUI.SetActive(false);

                        break;
                    }
                case GameStates.Battle:
                    {
                        _overworldCamObject.SetActive(false);
                        Vector3 overworldCamPos = _overworldCamObject.transform.position;

                        _battleCamObject.transform.position = new Vector3(camPos.x, camPos.y, camPos.z);
                        _battleCamObject.SetActive(true);
                        _battleUI.SetActive(true);

                        break;
                    }
            }
        }

        #endregion
    }
}
