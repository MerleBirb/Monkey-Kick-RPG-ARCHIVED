// Merle Roji
// 1/15/22

using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.UserInterface;
using MonkeyKick.Controls;
using MonkeyKick.QualityOfLife;
using MonkeyKick.Managers;
using MonkeyKick.RPGSystem.Characters;

namespace MonkeyKick.RPGSystem.Entities
{
    public class InteractRange : MonoBehaviour
    {
        [SerializeField] private EntityDescription[] _entitiesInDialogue;
        [SerializeField ]private LuaEnvironment dialogueEnvironment;
        [SerializeField] private GameObject talkBubble;
        [SerializeField] private GameManager gameManager;
        private bool _inRange = false;
        private CharacterMovement _player;

        #region CONTROLS

        private PlayerControls _controls;
        private InputAction _interact;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            // create a new instance of controls
            InputSystem.pollingFrequency = 180;
            _controls = new PlayerControls();

            // set up controls
            _interact = _controls.Overworld.Action;
        }

        private void Update()
        {
            CheckInput();
        }

        private void OnEnable()
        {
            talkBubble.SetActive(false);
            _controls?.Enable();
        }

        private void OnDisable()
        {
            talkBubble.SetActive(false);
            _controls?.Disable();
        }

        private void OnTriggerEnter(Collider col)
        {
            if (gameManager.GameState == GameStates.Overworld)
            {
                if (col.tag == TagsQoL.PLAYER_TAG)
                {
                    talkBubble.SetActive(true);
                    _inRange = true;
                    _player = col.GetComponent<CharacterMovement>();
                }
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (gameManager.GameState == GameStates.Overworld)
            {
                if (col.tag == TagsQoL.PLAYER_TAG)
                {
                    talkBubble.SetActive(false);
                    _inRange = false;
                    _player = null;
                }
            }
        }

        #endregion

        /// <summary>
        /// Checks if the player has pressed certain buttons.
        /// </summary>
        private void CheckInput()
        {
            bool playerCanStartDialogue = _interact.triggered && gameManager.GameState == GameStates.Overworld && _inRange;
            if (playerCanStartDialogue)
            {
                dialogueEnvironment.gameObject.SetActive(true);
                dialogueEnvironment.Player = _player;
                StartCoroutine(dialogueEnvironment.Setup());
                gameManager.GameState = GameStates.Dialogue;
                _inRange = false;
                _player.enabled = false;
            }
        }
    }
}
