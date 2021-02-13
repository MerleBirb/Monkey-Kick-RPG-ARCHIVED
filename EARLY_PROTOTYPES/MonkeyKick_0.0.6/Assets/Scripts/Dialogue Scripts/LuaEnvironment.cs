using MoonSharp.Interpreter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Kryz.CharacterStats;

public class LuaEnvironment : MonoBehaviour
{
    /// LUA ENVIRONMENT ///
    /// This script uses Lua to read text files and turns them into dialogue trees. Thank you @ Ned Makes Games for the long tutorial
    /// on this, it really helped. 11/22/20

    /// VARIABLES ///
    // store the dialogue manager object that doubles down as a canvas and encapsulates everything.
    public GameObject dialogueManager;
    // store the string of the file that must be loaded.
    [HideInInspector]
    public string loadFile;
    // store the environment script in this variable
    private Script enviro;
    // create a stack of active coroutines to loop through
    private Stack<MoonSharp.Interpreter.Coroutine> corStack;
    // check if the player is in dialogue
    public static bool inDialogue = false;
    // create a new gamestate for the lua environment to run in
    private LuaState luaGameState;

    /// FUNCTIONS ///
    // the getter function for luaGameState
    public LuaState LuaGameState
    {
        get
        {
            return luaGameState;
        }
    }

    // Awake happens before anything
    private void Awake()
    {
        luaGameState = new LuaState();
    }

    // Setup loads the file
    public IEnumerator Setup()
    {
        Debug.Log("Loaded Script");
        Script.DefaultOptions.DebugPrint = (s) => Debug.Log(s);
        UserData.RegisterAssembly();

        corStack = new Stack<MoonSharp.Interpreter.Coroutine>();

        enviro = new Script(CoreModules.Preset_SoftSandbox);
        enviro.Globals["SetCharacterName"] = (Action<string>)LuaCommands.SetCharacterName;
        enviro.Globals["SetText"] = (Action<string>)LuaCommands.SetText;
        enviro.Globals["ShowButtons"] = (Action<string, string>)LuaCommands.ShowButtons;
        enviro.Globals["ToggleChoosingChoice"] = (Action<bool>)LuaCommands.ToggleChoosingChoice;
        enviro.Globals["SetStatValue"] = (Action<char, CharacterStat>)LuaCommands.SetStatValue;
        enviro.Globals["State"] = UserData.Create(luaGameState);

        yield return 1;

        LoadFile(loadFile);
        AdvanceScript();
    }

    // LoadFile loads the lua file
    private void LoadFile(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        DynValue ret = DynValue.Nil;

        try
        {
            using (BufferedStream stream = new BufferedStream(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
            {
                ret = enviro.DoStream(stream);
            }
        }
        catch (SyntaxErrorException ex)
        {
            Debug.LogError(ex.DecoratedMessage);
        }

        if (ret.Type == DataType.Function)
        {
            corStack.Push(enviro.CreateCoroutine(ret).Coroutine);
        }
    }

    // AdvanceScript continues the dialogue
    public void AdvanceScript()
    {
        if (corStack.Count > 0)
        {
            try
            {
                MoonSharp.Interpreter.Coroutine active = corStack.Peek();
                DynValue ret = active.Resume();

                if (active.State == CoroutineState.Dead)
                {
                    corStack.Pop();
                }

                if (ret.Type == DataType.Function)
                {
                    corStack.Push(enviro.CreateCoroutine(ret).Coroutine);
                }
            }
            catch (ScriptRuntimeException ex)
            {
                Debug.LogError(ex.DecoratedMessage);
                corStack.Clear();
            }
        }
        else
        {
            inDialogue = false;
            dialogueManager.SetActive(false);
        }
    }
}
