// Merle Roji
// 1/14/22

using System.IO;
using UnityEngine;
using UnityEditor;
using MoonSharp.Interpreter;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MonkeyKick.UserInterface
{
    public class LuaEnvironment : MonoBehaviour
    {
        #region MOONSHARP FIELDS

        private Script _environment;
        private Stack<MoonSharp.Interpreter.Coroutine> _corStack;
        private LuaState _luaState;
        public LuaState LuaState
        {
            get => _luaState;
        }

        [SerializeField] private UnityEngine.Object loadFile; // serialized lua file

        #endregion

        #region UNITY METHODS

        private IEnumerator Start()
        {
            Debug.Log("Load Script");
            Script.DefaultOptions.DebugPrint = (s) => Debug.Log(s);
            UserData.RegisterAssembly(); // register the LuaState class in Lua

            _corStack = new Stack<MoonSharp.Interpreter.Coroutine>();
            _environment = new Script(CoreModules.Preset_SoftSandbox);
            _luaState = new LuaState();

            // add command functions and variables to the global environment
            _environment.Globals["SetText"] = (Action<string>)LuaCommands.SetText;
            _environment.Globals["SetName"] = (Action<string>)LuaCommands.SetName;
            _environment.Globals["State"] = UserData.Create(_luaState);

            yield return 1;
            
            LoadFile(loadFile);
            AdvanceScript();
        }

        #endregion

        #region CUSTOM LUA METHODS

        /// <summary>
        /// Loads a file from a file path.
        /// If the file doesn't exist, it throws an error.
        /// </summary>
        /// <param name="fileName"></param>
        private void LoadFile(UnityEngine.Object fileName)
        {
            string filePath = AssetDatabase.GetAssetPath(fileName); // get the path of the lua file

            DynValue ret = DynValue.Nil;

            // opens file at file path, throws error if file doesnt exist, only reads the file
            // automatically disposes file after use
            // handle the error manually
            try
            {
                using(BufferedStream stream = new BufferedStream(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
                {
                    ret = _environment.DoStream(stream);
                }
            }
            catch (SyntaxErrorException ex)
            {
                Debug.LogError(ex.DecoratedMessage);
            }

            if (ret.Type == DataType.Function)
            {
                _corStack.Push(_environment.CreateCoroutine(ret).Coroutine);
            }
        }

        /// <summary>
        /// Moves to the next step in the Coroutine in the Lua Dialogue file.
        /// </summary>
        public void AdvanceScript()
        {
            if (_corStack.Count > 0)
            {
                try
                {
                    MoonSharp.Interpreter.Coroutine active = _corStack.Peek();
                    DynValue ret = active.Resume();

                    if (active.State == CoroutineState.Dead)
                    {
                        _corStack.Pop();
                        Debug.Log("Dialogue Complete.");
                    }
                    if (ret.Type == DataType.Function)
                    {
                        _corStack.Push(_environment.CreateCoroutine(ret).Coroutine);
                    }
                }
                catch (ScriptRuntimeException ex)
                {
                    Debug.LogError(ex.DecoratedMessage);
                    _corStack.Clear();
                } 
            }
            else
            {
                Debug.Log("No Active Dialogue.");
            }
        }

        #endregion
    }
}
