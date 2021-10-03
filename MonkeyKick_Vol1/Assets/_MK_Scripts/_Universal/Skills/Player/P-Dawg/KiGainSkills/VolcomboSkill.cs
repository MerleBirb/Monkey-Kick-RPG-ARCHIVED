//===== VOLCOMBO =====//
/*
10/3/21 (4th of july)
Description:
- P-Dawg's default combination move

Author: Merlebirb
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.Battle;
using MonkeyKick.AudioFX;
using MonkeyKick.UI;

namespace MonkeyKick.Skills
{
    public class VolcomboSkill : PlayerSkill
    {
        //===== VARIABLES =====//
        private enum SequenceState
        {
            WaitingToBegin,
            DashToTarget,
            JabPunch,
            Return,
            EndSequence
        }

        [SerializeField] private SequenceState sequence = SequenceState.WaitingToBegin;
        [SerializeField] private float dashSpeed = 1.0f;

        #region TIMERS

        [SerializeField] private float jabRankTimer1;
        [SerializeField] private float jabRankTimer2;
        [SerializeField] private float jabRankTimer3;
        [SerializeField] private float jabRankTimer4;
        [SerializeField] private float jabRankTimer5;

        #endregion
    }
}
