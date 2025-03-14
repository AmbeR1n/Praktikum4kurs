using System;
using UnityEngine;

[Serializable]
public struct SoundEffect
{
    public string groupID;
    public AudioClip[] clips;
}
public class SoundLibrary : MonoBehaviour
{

    public SoundEffect[] soundEffects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip GetClipsFromName(string name)
    {
        foreach(var soundEffect in soundEffects)
        {
            if (soundEffect.groupID == name)
            {
                return soundEffect.clips[UnityEngine.Random.Range(0, soundEffect.clips.Length)];
            }
        }
        return null;
    }
}
