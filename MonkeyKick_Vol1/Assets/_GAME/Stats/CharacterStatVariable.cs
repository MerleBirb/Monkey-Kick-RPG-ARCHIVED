//===== CHARACTER STAT VARIABLE =====//
/*
5/22/21
Description: 
- Saves a Character Stat as a Scriptable Object.
- Special thanks to Ryan Hipple for this approach.

*/

using UnityEngine;

namespace MonkeyKick.Stats
{
    [CreateAssetMenu(fileName = "Stat", menuName = "New Stat")]
    public class CharacterStatVariable : ScriptableObject
    {
        public CharacterStat Stat;

        private void OnValidate()
        {
            Stat.BaseValue = Mathf.Clamp(Stat.BaseValue, 1, 9999);
        }
    }
}