using UnityEngine;
using DigitalRuby.SoundManagerNamespace;

public class SoundController : MonoBehaviour
{

    [Header("Adjustment")]
    [SerializeField] bool shouldPlayMusic;
    [SerializeField] bool shouldPlayAmbience;

    [Header("Sources")]
    [SerializeField] AudioSource[] SoundAudioSources;
    [SerializeField] AudioSource[] MusicAudioSources;

    [Header("Modifier")]
    [SerializeField, Range(0f, 1.0f)] float musicVol;
    [SerializeField, Range(0f, 1.0f)] float soundVol;
    [SerializeField, Range(0f, 1.0f)] float soundLoopVol;

    public void PlaySoundOnce(int index){
        SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip, soundVol);
    }

    public void PlaySoundOnceOverload(int index, float volume){
        SoundAudioSources[index].PlayOneShotSoundManaged(SoundAudioSources[index].clip, volume);
    }

    public void PlaySoundLoop(int index){
        SoundAudioSources[index].PlayLoopingSoundManaged(soundLoopVol, 1.0f);
    }

    public void StopSound(int index){
        SoundAudioSources[index].Stop();
    }

    public void PlayMusic(int index) {
        MusicAudioSources[index].PlayLoopingMusicManaged(musicVol, 1.0f, false);
    }

    public void PlayMusicOverload(int index, float vol) {
        MusicAudioSources[index].PlayLoopingMusicManaged(vol, 1.0f, false);
    }

    void Start(){
        if (shouldPlayMusic)
            PlayMusic(0);

        if (shouldPlayAmbience)   
            PlaySoundLoop(0);
    }
}
