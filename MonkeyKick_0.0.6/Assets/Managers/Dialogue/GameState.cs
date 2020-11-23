using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[MoonSharpUserData]
public class GameState
{
    /// GAME STATE FOR LUA ///
    /// this is a class for Lua to interpret different strings into different types, like names VS dialogue. Part of the Lua Dialogue System.
    /// Thank you @ Ned Makes Games. 11/22/20

    /// VARIABLES ///
    // store some flags
    private HashSet<string> flags;
    // store the choice selected
    private int choiceSelected;
    // store the name of the character
    private string characterName;

    /// FUNCTIONS ///
    // instantiate the game state
    [MoonSharpHidden]
    public GameState()
    {
        flags = new HashSet<string>();
    }

    // store the name of the character in a get set   
    public string CharacterName
    {
        get
        {
            return characterName;
        }
        [MoonSharpHidden]
        set
        {
            characterName = value;
        }
    }

    // store the dialogue choice with the choice pressed
    public int ChoiceSelected
    {
        get
        {
            return choiceSelected;
        }
        [MoonSharpHidden]
        set
        {
            choiceSelected = value;
        }
    }

    // flags... fun, right? for setting off booleans in the lua file
    public bool GetFlag(string flag)
    {
        return flags.Contains(flag);
    }

    public void SetFlag(string flag, bool set)
    {
        if(set)
        {
            flags.Add(flag);
        }
        else
        {
            flags.Remove(flag);
        }
    }

    // this is better for multi character conversations, setting and getting the character name!
    public void SetCharacterName(string name)
    {
        characterName = name;
    }
}
