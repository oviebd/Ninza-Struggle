using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    public AudioSource uiButtonClickedAudioSource;

    public AudioSource backgroundAudioSource;
    public AudioClip menuSoundClip, stageBackGroundSoundClip, bossStageBackgroundSoundClip;

    public AudioSource collectableAudioSource;

    public Image soundImage;
    public Sprite soundOn, soundOff;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void SwapSound()
    {
        Utility.SwapSound();
        SetSoundImage();

        if (Utility.getIsSoundOn() == false)
            PauseBackgroundSound();
        else
            PlayBackgroundSound();
    }

    public void SetSoundImage()
    {
        if (Utility.getIsSoundOn() == true)
            soundImage.sprite = soundOn;
       else
            soundImage.sprite = soundOff;
    }

    public void PlayCollectableCollectSound()
    {
        if (Utility.getIsSoundOn() == true)
        {
            collectableAudioSource.Play();
        }
    }

    public void PlayUiButtonSound()
    {
        if(Utility.getIsSoundOn()== true)
        {
            uiButtonClickedAudioSource.Play();
        }
    }

    public void PauseBackgroundSound()
    {
        backgroundAudioSource.Pause();

    }

    public void PlayBackgroundSound()
    {
        if (Utility.getIsSoundOn())
        {
            backgroundAudioSource.Play();
        }
        else
            PauseBackgroundSound();
    }

    public void SetBackGroundAudioClip(VariableUtilities.SoundType type)
    {
            backgroundAudioSource.Pause();
            switch (type){
                case VariableUtilities.SoundType.menuSound:
                    backgroundAudioSource.clip = menuSoundClip;
                    break;

                case VariableUtilities.SoundType.stageSound:
                    backgroundAudioSource.clip = stageBackGroundSoundClip;
                    break;
                case VariableUtilities.SoundType.bossSound:
                    backgroundAudioSource.clip = bossStageBackgroundSoundClip;
                    break;
            }

        if (Utility.getIsSoundOn() == true)
        {
            backgroundAudioSource.Play();
        }
    }
}
