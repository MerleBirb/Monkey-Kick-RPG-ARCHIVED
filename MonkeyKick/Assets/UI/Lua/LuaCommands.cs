// Merle Roji
// 1/14/22

using UnityEngine;
using TMPro;
using System.Collections;

namespace MonkeyKick.UserInterface
{
    public class LuaCommands : MonoBehaviour
    {
        private static LuaCommands instance; // singleton
        private LuaEnvironment lua;
        private string dialogue;
        [SerializeField] private TextMeshProUGUI textUI;
        
        #region UNITY METHODS

        private void Start()
        {
            lua = FindObjectOfType<LuaEnvironment>();
        }

        private void Awake()
        {
            if (instance == null) instance = this;
        }

        #endregion

        #region LUA COMMAND METHODS

        /// <summary>
        /// Sets the text inside the dialogue box.
        /// </summary>
        /// <param name="textString"></param>
        public static void SetText(string textString)
        {
            instance.StartCoroutine(instance.TypeDialogue(textString));
        }

        private IEnumerator TypeDialogue(string text)
        {
            textUI.text = "";
            foreach(char letter in text.ToCharArray())
            {
                textUI.text += letter;

                yield return new WaitForSeconds(0.03f);
            }
        }

        /// <summary>
        /// Sets the name of whatever is speaking inside the dialogue box.
        /// </summary>
        /// <param name="newName"></param>
        public static void SetName(string newName)
        {
            instance.lua.LuaState.CurrentName = newName;
        }

        #endregion
    }
}
