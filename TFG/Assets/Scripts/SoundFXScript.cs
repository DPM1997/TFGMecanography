using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;

public enum MusicTypes {general, music, sfx};

public class SoundFXScript : MonoBehaviour
{
    public static SoundFXScript instance;
    [SerializeField] private List<AudioMixerGroup> mixer;

    [SerializeField] private List<AudioSource> sources;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        // Debug.Log(instance);
    }

    // Update is called once per frame
    void Update()
    { 
    }

    public void PlayAudio(AudioClip audioClip, float volume, MusicTypes type, float delayed){
        if (type == MusicTypes.music){
            sources[0].volume = volume;
            sources[0].outputAudioMixerGroup = mixer[0];
            sources[0].clip= audioClip;
            sources[0].PlayDelayed(delayed);
        }
        else{
            sources[1].volume = volume;
            sources[1].outputAudioMixerGroup = mixer[1];
            sources[1].PlayOneShot(audioClip);
        }
    }
    
    public void Pause(){
        foreach(AudioSource source in sources)
            source.Pause();
    }

    public void Resume(){
        foreach(AudioSource source in sources)
            source.UnPause();
    }
}
