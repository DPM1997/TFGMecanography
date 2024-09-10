using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;

public enum MusicTypes {general, music, sfx};

/// <summary>
/// This class manages all the sound in the game.
/// </summary>
public class SoundFXScript : MonoBehaviour
{
    /// <summary>
    /// Singleton of the class.
    /// </summary>
    public static SoundFXScript instance;
    /// <summary>
    /// All the AudioMixerGroup in the AudioMixer. The number is the same as MusicTypes lenght.
    /// </summary>
    [SerializeField] private List<AudioMixerGroup> mixer;
    /// <summary>
    /// The AudioSources in the AudioMixer. The number is the same as MusicTypes lenght.
    /// </summary>
    [SerializeField] private List<AudioSource> sources;
    // Variables to pause the game
    private float pressTime= 0f;
    private float delayedTime = 0f;
     private float resumeTime = 0f;
    /// <summary>
    /// Intances the singleton.
    /// </summary>
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    /// <summary>
    /// Reproduces the audio in the scene.
    /// </summary>
    /// <param name="audioClip">The audio to play.</param>
    /// <param name="volume">Value form 1 to 0.</param>
    /// <param name="type">1 of 3 values: general, music, sfx.</param>
    /// <param name="delayed">Time in seconds before the sound plays.</param>
    public void PlayAudio(AudioClip audioClip, float volume, MusicTypes type, float delayed){
        if (type == MusicTypes.music){
            sources[0].volume = volume;
            sources[0].outputAudioMixerGroup = mixer[0];
            sources[0].clip= audioClip;
            sources[0].PlayDelayed(delayed);
            delayedTime = delayed;
            pressTime = Time.time;
        }
        else{
            sources[1].volume = volume;
            sources[1].outputAudioMixerGroup = mixer[1];
            sources[1].PlayOneShot(audioClip);
        }
    }
    /// <summary>
    /// Pause all audios.
    /// </summary>
    public void Pause(){
        for(int i =0; i< sources.Count; i++){
            if(i == 0){
                float pressTime2=Time.time;
                float auxTime = pressTime2- pressTime;
                if (auxTime < delayedTime)
                    resumeTime = delayedTime-auxTime;
                else
                    resumeTime = 0f; 
            }
            sources[i].Pause();
        }
    }
    /// <summary>
    /// Resume all audios
    /// </summary>
    public IEnumerator Resume(){
        for(int i =0; i< sources.Count; i++){
            if(i == 0 ){
                yield return new WaitForSeconds(resumeTime);
            }
            sources[i].UnPause();
        }
    }
}
