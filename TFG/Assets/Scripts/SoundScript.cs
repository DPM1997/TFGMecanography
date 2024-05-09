using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour
{
    [SerializeField] List<Slider> soundSlider;
    [SerializeField] AudioMixer masterMixer;

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
        RefreshSlide(value,Types.master);
        PlayerPrefs.SetFloat("SavedMasterVolume", value);
        PlayerPrefs.Save();
        masterMixer.SetFloat("VolumeMusic", Mathf.Log10(value / 100) * 20f);
    }

    public void SetVolumeSFX(float value){
        if(value < 1){
            value = .001f;
        }
        Debug.Log("SFX");
        RefreshSlide(value,Types.sfx);
        PlayerPrefs.SetFloat("SavedSFXVolume", value);
        PlayerPrefs.Save();
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(value / 100) * 20f);
    }

    public void SetVolumeFromSlider(Types type){
        if (type == Types.master){
            SetVolumeMaster(soundSlider[0].value);
        }
        else{
            SetVolumeSFX(soundSlider[1].value);
        }
    }

    public void RefreshSlide(float value,Types type){
        if (type == Types.master){
        soundSlider[0].value = value;
        }
        else{
        soundSlider[0].value = value;   
        }
    }
}
