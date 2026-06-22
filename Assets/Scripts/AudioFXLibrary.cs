using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioFXLibrary : MonoBehaviour
{

    [SerializeField] private SoundFXGroup[] soundFXGroups;
    //string = name of the sound fx
    private Dictionary<string, List<AudioClip>> soundDictionary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        soundDictionary = new Dictionary<string, List<AudioClip>>();
        foreach (SoundFXGroup soundFXGroup in soundFXGroups)
        {
            soundDictionary[soundFXGroup.name] = soundFXGroup.audioClips;
        }
    }


    public AudioClip GetRandomClip(string name)
    {
        if (soundDictionary.ContainsKey(name))
        {
            List<AudioClip> audioClips = soundDictionary[name];
            if (audioClips.Count > 0)
            {
                return audioClips[UnityEngine.Random.Range(0, audioClips.Count)];

            }
        }
        return null;
    }
}


[System.Serializable]
public struct SoundFXGroup
{
    public string name;
    public List<AudioClip> audioClips;
}
