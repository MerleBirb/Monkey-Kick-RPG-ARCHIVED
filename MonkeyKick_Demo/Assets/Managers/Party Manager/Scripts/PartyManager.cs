// Merle Roji 7/11/22

using UnityEngine;
using System.Collections.Generic;
using MonkeyKick.Characters;

namespace MonkeyKick.Managers.Party
{
    /// <summary>
    /// Holds Party member data.
    /// 
    /// Notes:
    /// 
    /// </summary>
    
    [CreateAssetMenu(fileName = "Party", menuName = "RPG/Create a Party/Party")]
    public class PartyManager : ScriptableObject
    {
        public List<Character> Characters;
    }
}
