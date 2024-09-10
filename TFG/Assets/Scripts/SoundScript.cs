using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// This class manages the sound from the menu and is used by the sliders.
/// </summary>
public class SoundScript : MonoBehaviour
{
    /// <summary>
    /// The sliders in the scene. The number is the same as MusicTypes lenght.
    /// </summary>
    [SerializeField] private List<Slider> soundSlider;
    /// <summary>
    /// The AudioMixer where all the AudioSources are.
    /// </summary>
    [SerializeField] private AudioMixer masterMixer;

    /// <summary>
    /// When the scene loads, all the sliders are setter to the saved volume.
    /// </summary>
    private void Start()
    {
        SetVolumeGeneral(PlayerPrefs.GetFloat("SavedGeneralVolume", 100));
        SetVolumeMusic(PlayerPrefs.GetFloat("SavedMusicVolume", 100));
        SetVolumeSFX(PlayerPrefs.GetFloat("SavedVolumeSFX", 100));
    }

    /// <summary>
    /// Set the volume of <b>general</b> slider to the float value.
    /// </summary>
    /// <param name="value">Value from 1 to 100.</param>
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

    /// <summary>
    /// Set the volume of <b>music</b> slider to the float value.
    /// </summary>
    /// <param name="value">Value from 1 to 100.</param>
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

    /// <summary>
    /// Set the volume of <b>sfx</b> slider to the float value.
    /// </summary>
    /// <param name="value">Value from 1 to 100.</param>
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

    /// <summary>
    /// Used by the sliders in the scene when the slider value is modified. Set the volume of that slider to the value.
    /// </summary>
    /// <param name="type">Has to be part of the <see cref="MusicTypes"/>MusicTypes</see> enum</param>
    public void SetVolumeFromSlider(String type)
    {
        if (type == MusicTypes.general.ToString())
            SetVolumeGeneral(soundSlider[0].value);
        else if (type == MusicTypes.music.ToString())
            SetVolumeMusic(soundSlider[1].value);
        else if (type == MusicTypes.sfx.ToString())
            SetVolumeSFX(soundSlider[2].value);

    }

    /// <summary>
    /// To change the value of the slider asset in the game. Although is only needed when Start(),
    /// there is no harm in called when the slider is changed.
    /// </summary>
    /// <param name="value">Value from 1 to 100</param>
    /// <param name="type">Has to be part of the <see cref="MusicTypes"/>MusicTypes</see> enum</param>
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
