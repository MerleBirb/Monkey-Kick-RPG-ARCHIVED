using MoonSharp.Interpreter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LuaEnvironment : MonoBehaviour
{
    ////////// LUA ENVIRONMENT //////////
    /// ok so this one is kind of huge. it allows me to use Lua to read text files into dialogue trees.

    // store the manager
    public GameObject dialogueManager;

    // store the file name of the file needing to be loaded
    [SerializeField]
    private string loadFile;
    
    // store a Lua environment script using the Lua interpreter
    private Script enviro;

    // a stack of active coroutines
    private Stack<MoonSharp.Interpreter.Coroutine> corStack;

    // is the player in dialogue?
    public static bool isPlayerInDialogue = false;

    // create a new game state for lua to be in
    private GameState luaGameState;

    public GameState LuaGameState
    {
        get
        {
            return luaGameState;
        }
    }

    // Awake happens before anything
    private void Awake()
    {
        luaGameState = new GameState();
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
        enviro.Globals["State"] = UserData.Create(luaGameState);

        yield return 1;

        LoadFile(loadFile);
        AdvanceScript();
    }

    // load the lua file
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

        if(ret.Type == DataType.Function)
        {
            corStack.Push(enviro.CreateCoroutine(ret).Coroutine);
        }
    }

    // Continue the dialogue
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
            isPlayerInDialogue = false;
            dialogueManager.SetActive(false);
        }
    }
}