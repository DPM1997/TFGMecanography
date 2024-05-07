using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum Types {master, sfx};

public class SoundScript : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioMixer masterMixer;

    public Types type = Types.master;

    // Start is called before the first frame update
    void Start()
    {
        SetVolumeMaster(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
        SetVolumeSFX(PlayerPrefs.GetFloat("SavedSFXVolume", 100));
    }

    public void SetVolumeMaster(float value){
        if(value < 1){
            value = .001f;
        }

        RefreshSlide(value);
        PlayerPrefs.SetFloat("SavedMasterVolume", value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(value / 100) * 20f);
    }

    public void SetVolumeSFX(float value){
        if(value < 1){
            value = .001f;
        }

        RefreshSlide(value);
        PlayerPrefs.SetFloat("SavedSFXVolume", value);
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(value / 100) * 20f);
    }

    public void SetVolumeFromSlider(){
        if (type == Types.master){
            SetVolumeMaster(soundSlider.value);
        }
        else{
           SetVolumeSFX(soundSlider.value);
        }
    }

    public void RefreshSlide(float value){
        soundSlider.value = value;
    }
}
