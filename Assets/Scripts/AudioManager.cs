using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public enum Music
    {
        Death,
        Bar,
        Fight
    }

    [Space, Header("Music"), Space]
    [SerializeField] AudioSource backgroundMusic = null;
    [SerializeField] AudioClip death = null;
    [SerializeField] AudioClip bar = null;
    [SerializeField] AudioClip fight = null;

    [Space, Header("Fight"), Space]

    [SerializeField] AudioClip fightIntro = null;
    [SerializeField] AudioClip fightOutro = null;

    [Space, Header("UI"), Space]
    [SerializeField] AudioSource uiSource = null;
    [SerializeField] AudioClip buttonSound = null;
    [SerializeField] AudioClip fadeSound = null;

    private void Awake()
    {
        backgroundMusic.Play();
    }
    public void UpdateVolume(Slider slider)
    {
        AudioListener.volume = slider.value;
    }
    public void SetMusic(Music music)
    {
        backgroundMusic.Stop();
        if(music == Music.Death)
        {
            backgroundMusic.clip = death;
        }
        else if(music == Music.Bar)
        {
            backgroundMusic.clip = bar;
        }
        else
        {
            backgroundMusic.clip = fight;
        }
        backgroundMusic.Play();
    }
    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }
    public void PlayFightIntro(out float duration)
    {
        backgroundMusic.PlayOneShot(fightIntro);
        duration = fightIntro.length;
    }
    public void PlayFightOutro()
    {
        backgroundMusic.PlayOneShot(fightOutro);
    }
    public void PlayButtonSound()
    {
        uiSource.PlayOneShot(buttonSound);
    }
    public void PlayFadeSound()
    {
        uiSource.PlayOneShot(fadeSound);
    }
}
