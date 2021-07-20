//===== CHARACTER PARTY DATA =====//
/*
5/23/21
Description:
- Holds the data of a party of characters.

Author: Merlebirb
*/

using UnityEngine;
using System.Collections.Generic;
using MonkeyKick.EntityInformation;

namespace MonkeyKick.Battle
{
    [CreateAssetMenu(menuName = "New Party", fileName = "CharacterParty")]
    public class CharacterPartyData : ScriptableObject
    {
        public List<CharacterInformation> CharacterList;
    }
}