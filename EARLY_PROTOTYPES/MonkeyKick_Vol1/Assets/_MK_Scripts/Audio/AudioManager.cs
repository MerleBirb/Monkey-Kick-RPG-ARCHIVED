//===== AUDIO MANAGER =====//
/*
7/21/21
Description:
- Manages audio

Author: Merlebirb, Bravo
*/

using UnityEngine;

namespace MonkeyKick.AudioFX
{
	[RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
    	#region PUBLIC VARIABLES

		internal AudioSource source;

		// volume
		[Range(0f, 1f)] public float BaseVolume = 1f;
		public Vector2 VolumeOffset = Vector2.zero;

		// pitch
		[Range(0f, 3f)] public float BasePitch = 1f;
		public Vector2 PitchOffset = Vector2.zero;

		public AudioTable AudioTable;

    	#endregion

    	#region UNITY METHODS

		private void Awake()
		{
			source = GetComponent<AudioSource>();
		}

		void OnValidate()
		{
			if (TryGetComponent<AudioSource>(out source))
			{
				source.volume = BaseVolume;
				source.pitch = BasePitch;
			}
		}

    	#endregion

    	#region PUBLIC METHODS

		public void Play() => Play(source.clip);
		public void Play(AudioClip clip) => PlayRaw(clip, BaseVolume, BasePitch);

		public void PlayRaw(AudioClip clip, float baseVolume = 1f, float basePitch = 1f)
		{
			if (clip) source.clip = clip;

			source.volume = baseVolume + Random.Range(VolumeOffset.x, VolumeOffset.y);
			source.pitch = basePitch + Random.Range(PitchOffset.x, PitchOffset.y);

			source.Play();
		}

		public void PlayWithVolume(float newVolume) => PlayRaw(source.clip, baseVolume: newVolume * BaseVolume);
		public void PlayWithPitch(float newPitch) => PlayRaw(source.clip, basePitch: newPitch * BasePitch);

    	#endregion
    }
}
