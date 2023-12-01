using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singletons<SoundManager>
{
    public Sound[] musicSound, sfxSound;
    public AudioSource musicSource, sfxSource;
    bool mute = false;
    float holdTimeMusic;
    float holdTimeSFX;
    [SerializeField] float delayholdTime;


    void Start()
    {

    }
    void Update()
    {
        VolumeControl();
    }
    public void PlayMusic(string nameSound)
    {
        Sound s = Array.Find(musicSound, x => x.nameSound == nameSound);
        if (s == null)
        {
            Debug.Log("Not have music");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySfx(string nameSfx)
    {
        Sound s = Array.Find(sfxSound, x => x.nameSound == nameSfx);
        if (s == null)
        {
            Debug.Log("Not have SFX");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public AudioClip SearchSfx(string nameSound)
    {
        AudioClip sound = null;
        Sound s = Array.Find(sfxSound, x => x.nameSound == nameSound);
        if (s == null)
        {
            Debug.Log("Not have SFX");
        }
        else
        {
            sound = s.clip;
        }
        return sound;
    }

    #region VolumeControl
    void VolumeControl()
    {
        VolumeUp();
        VolumeDown();
        MuteSound();
    }
    void MuteSound()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            mute = !mute;
            musicSource.mute = mute;
            sfxSource.mute = mute;
        }
    }
    void VolumeUp()
    {
        VolumeUpMusic();
        VolumeUpSFX();
    }
    void VolumeUpMusic()
    {
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            musicSource.volume += 0.01f;
        }
        if (Input.GetKey(KeyCode.Keypad8))
        {
            holdTimeMusic += Time.deltaTime;
            if (holdTimeMusic > delayholdTime)
            {
                musicSource.volume += 0.01f;
            }
        }
        if (Input.GetKeyUp(KeyCode.Keypad8))
        {
            holdTimeMusic = 0;
        }
    }

    void VolumeUpSFX()
    {
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            sfxSource.volume += 0.01f;
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            holdTimeSFX += Time.deltaTime;
            if (holdTimeSFX > delayholdTime)
            {
                sfxSource.volume += 0.01f;
            }
        }
        if (Input.GetKeyUp(KeyCode.Keypad6))
        {
            holdTimeSFX = 0;
        }
    }
    void VolumeDown()
    {
        VolumeDownMusic();
        VolumeDownSFX();
    }
    void VolumeDownMusic()
    {
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            musicSource.volume -= 0.01f;
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            holdTimeMusic += Time.deltaTime;
            if (holdTimeMusic > delayholdTime)
            {
                musicSource.volume -= 0.01f;
            }
        }
        if (Input.GetKeyUp(KeyCode.Keypad2))
        {
            holdTimeMusic = 0;
        }
    }
    void VolumeDownSFX()
    {
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            sfxSource.volume -= 0.01f;
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            holdTimeSFX += Time.deltaTime;
            if (holdTimeSFX > delayholdTime)
            {
                sfxSource.volume -= 0.01f;
            }
        }
        if (Input.GetKeyUp(KeyCode.Keypad4))
        {
            holdTimeSFX = 0;
        }
    }
    #endregion
}
