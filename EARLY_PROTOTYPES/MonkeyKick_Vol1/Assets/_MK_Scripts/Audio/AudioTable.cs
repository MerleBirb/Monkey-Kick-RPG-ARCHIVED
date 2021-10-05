//===== AUDIO TABLE =====//
/*
7/23/21
Description:
- Holds every audio file

Author: Merlebirb
*/

using UnityEngine;
using System.Collections.Generic;
using MonkeyKick.AudioFX;

namespace MonkeyKick.AudioFX
{
	[CreateAssetMenu(menuName = "Audio Table", fileName = "Audio Table")]
    public class AudioTable : ScriptableObject
    {
    	#region PRIVATE VARIABLES

		private Dictionary<string, Sound> _lookup = new Dictionary<string, Sound>();

    	#endregion

    	#region PUBLIC VARIABLES

		public List<Sound> entries;

    	#endregion

    	#region UNITY METHODS

		private void OnValidate()
		{
			foreach (var entry in entries) entry.id = entry.Clip.name;
			entries.Sort((a, b) => a.Clip.name.CompareTo(b.Clip.name));
		}

		private void OnEnable()
		{
			entries.Sort((a, b) => a.id.CompareTo(b.id));
			foreach (var entry in entries) _lookup.Add(entry.id, entry);
		}

    	#endregion

    	#region PRIVATE METHODS

	

    	#endregion

    	#region PUBLIC METHODS

		public Sound GetSound(string id)
		{
			return _lookup[id];
		}

    	#endregion
    }
}
