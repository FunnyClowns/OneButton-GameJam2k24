using UnityEngine;
using DigitalRuby.SoundManagerNamespace;

public class SoundController : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] AudioSource[] SoundAudioSources;
    [SerializeField] AudioSource[] MusicAudioSources;

    [Header("Modifier")]
    [SerializeField, Range(0f, 1.0f)] float musicVol;
    [SerializeField, Range(0f, 1.0f)] float soundVol;

    public void PlaySoundOnce(int index){
        SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip);
    }

    public void PlaySoundLoop(int index){
        SoundAudioSources[index].PlayLoopingSoundManaged(soundVol, 1.0f);
    }



    public void PlayMusic(int index) {
        MusicAudioSources[index].PlayLoopingMusicManaged(musicVol, 1.0f, true);
    }

    void Start(){
        PlayMusic(0);
        PlaySoundLoop(0);
    }
}