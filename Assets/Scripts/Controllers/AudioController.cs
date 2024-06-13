using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public AudioClip CardFlip, Correct, Wrong, Win, GameDone, ButtonClick;

    public AudioSource MusicSrc;
    public AudioSource AudioSrc;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { Destroy(this.gameObject); }
    }

    public void PlaySound(AudioClip Sound)
    {
        AudioSrc.PlayOneShot(Sound);
    }

    public void PlaySound(AudioClip Sound, float Volume)
    {
        AudioSrc.PlayOneShot(Sound, Volume);
    }

    public void StopSound()
    {
        AudioSrc.Stop();
    }

    public void MuteUnmute(bool IsMute) 
    {
        MusicSrc.mute = IsMute;
        AudioSrc.mute = IsMute;
    }

}
