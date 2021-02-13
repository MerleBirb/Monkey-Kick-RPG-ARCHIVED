using UnityEngine;

public class Interactable : MonoBehaviour
{
    /// INTERACTABLE SCRIPT ///
    /// Just stores basic information for interactables

    // the name of the interactable
    public string objName = "Interactable";
    // store the talking sounds of the sign
    public AudioClip[] talkSounds;
    public float maxPitch = 0.0f;
    public float minPitch = 0.0f;
}
