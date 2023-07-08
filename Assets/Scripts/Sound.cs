using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string soundName;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;

    [Range(0.1f, 3f)]
    public float pitch = 1f;

    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
}
