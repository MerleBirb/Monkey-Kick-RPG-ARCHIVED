using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SetCharacterName : MonoBehaviour
{
    ////////// SET THE CHARACTER'S NAME //////////
    ///self explanatory.
    
    // create the variable for the character name
    [SerializeField]
    private string characterName;

    // Start is called on the first frame
    private void Start()
    {
        FindObjectOfType<LuaEnvironment>().LuaGameState.CharacterName = characterName;
    }
}
