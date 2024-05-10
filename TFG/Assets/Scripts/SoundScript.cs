using System;
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
        SetVolumeGeneral(PlayerPrefs.GetFloat("SavedGeneralVolume", 100));
        SetVolumeMusic(PlayerPrefs.GetFloat("SavedMusicVolume", 100));
        SetVolumeSFX(PlayerPrefs.GetFloat("SavedVolumeSFX", 100));
    }

    public void SetVolumeGeneral(float value)
    {
        if (value < 1)
        {
            value = .001f;
        }
        RefreshSlide(value, MusicTypes.general);
        PlayerPrefs.SetFloat("SavedGeneralVolume", value);
        PlayerPrefs.Save();
        masterMixer.SetFloat("VolumeGeneral", Mathf.Log10(value / 100) * 20f);
    }

    public void SetVolumeMusic(float value)
    {
        if (value < 1)
        {
            value = .001f;
        }
        RefreshSlide(value, MusicTypes.music);
        PlayerPrefs.SetFloat("SavedMusicVolume", value);
        PlayerPrefs.Save();
        masterMixer.SetFloat("VolumeMusic", Mathf.Log10(value / 100) * 20f);
    }

    public void SetVolumeSFX(float value)
    {
        if (value < 1)
        {
            value = .001f;
        }
        RefreshSlide(value, MusicTypes.sfx);
        PlayerPrefs.SetFloat("SavedVolumeSFX", value);
        PlayerPrefs.Save();
        masterMixer.SetFloat("VolumeSFX", Mathf.Log10(value / 100) * 20f);
    }

    public void SetVolumeFromSlider(String type)
    {
        if (type == MusicTypes.general.ToString())
            SetVolumeGeneral(soundSlider[0].value);
        else if (type == MusicTypes.music.ToString())
            SetVolumeMusic(soundSlider[1].value);
        else if (type == MusicTypes.sfx.ToString())
            SetVolumeSFX(soundSlider[2].value);

    }

    public void RefreshSlide(float value, MusicTypes type)
    {
        //soundSlider[(int) type].value = value;
        if (type == MusicTypes.general)
            soundSlider[0].value = value;
        else if (type == MusicTypes.music)
            soundSlider[1].value = value;
        else soundSlider[2].value = value;

    }
}
