//===== SOUND =====//
/*
7/21/21
Description:
- Holds info for an audio clip

Author: Merlebirb
*/

using System;
using UnityEngine;
using UnityEngine.Audio;
using MonkeyKick.References;

namespace MonkeyKick.AudioFX
{
	[Serializable]
    public class Sound
    {
    	#region PUBLIC VARIABLES

		public string id;
		public AudioClip Clip;
		[Range(0f, 1f)] public float Volume = 1f;
		[Range(0f, 3f)] public float Pitch = 1f;

    	#endregion
    }
}
