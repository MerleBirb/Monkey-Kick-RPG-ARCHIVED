//===== BATTLE SFX =====//
/*
7/24/21
Description:
- Holds all battle sfx for the character / entity

Author: Merlebirb
*/

using System;
using System.Collections.Generic;

namespace MonkeyKick.AudioFX
{
    [Serializable]
	public class BattleSFX
	{
		public List<AudioManager> PhysicalHitTracks;
		public List<AudioManager> RangedHitTracks;
	}
}
