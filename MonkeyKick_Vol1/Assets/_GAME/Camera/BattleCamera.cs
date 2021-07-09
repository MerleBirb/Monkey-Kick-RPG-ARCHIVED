//===== BattleCamera =====//
/*
7/8/21
Description:
- Moves the camera around according to the battle.

Author: Merlebirb
*/

using UnityEngine;
using System.Collections.Generic;

namespace MonkeyKick
{
    public class BattleCamera : MonoBehaviour
    {
    	//===== VARIABLES =====//
        private List<Transform> _targets;
        public List<Transform> GetTargets() => _targets;
        public void SetTargets(List<Transform> newTargets) => _targets = newTargets;

        public Vector3 Offset;

    	//===== INIT =====//

    	//===== METHODS =====//

        private void LateUpdate()
        {
            if (_targets.Count == 0) return;
        }
    }
}
