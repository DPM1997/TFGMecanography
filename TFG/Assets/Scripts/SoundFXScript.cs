using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXScript : MonoBehaviour
{
    public static SoundFXScript instance;
    //[SerializeField] private AudioSource soundFXObject;
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

    public void PlayAudio(AudioClip audioClip, float volume){
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float lenght = audioSource.clip.length;
        Destroy(audioSource,lenght);
    }
}
