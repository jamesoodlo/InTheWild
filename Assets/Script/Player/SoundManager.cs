using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    Animator _animator;
    AudioSource _audio;

    [Header("Character Sfx")]
    public AudioSource _audioFootStep;
    public AudioClip walkingSfx;

    public AudioSource _audioRoll;
    public AudioClip rollSfx;

    [Header("Attack Sfx")]
    public AudioClip[] atkSfx;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
    
    }

    public void footStepSfx()
    {
        _audioFootStep.clip = walkingSfx;
        _audioFootStep.Play();
    }

    public void rollingSfx()
    {
        _audioRoll.clip = rollSfx;
        _audioRoll.Play();
    }

    public void axeSfx()
    {
        _audio.clip = atkSfx[0];
        _audio.Play(); 
    }

    public void bladeSfx()
    {
        _audio.clip = atkSfx[1];
        _audio.Play(); 
    }

    public void spearSfx()
    {
        _audio.clip = atkSfx[2];
        _audio.Play(); 
    }

    public void hammerSfx()
    {
        _audio.clip = atkSfx[3];
        _audio.Play(); 
    }

    public void lightSaberSfx()
    {
        _audio.clip = atkSfx[4];
        _audio.Play(); 
    }
}
