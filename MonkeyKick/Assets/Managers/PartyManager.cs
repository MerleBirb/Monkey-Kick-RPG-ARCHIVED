// Merle Roji
// 10/12/21

using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.PhysicalObjects.Characters;

namespace MonkeyKick.Managers
{
    [CreateAssetMenu(fileName = "PartyManager", menuName = "Managers/Party Manager", order = 2)]
    public class PartyManager : ScriptableObject
    {
        [Header("The current Party goes into this list.")]
        [SerializeField] private List<CharacterBattle> playerParty;

        [Header("All playable characters go into this list.")]
        [SerializeField] private List<CharacterBattle> allPlayableCharacters;
    }
}
