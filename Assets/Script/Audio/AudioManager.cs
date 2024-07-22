using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    [Header("ÉùÒôÊý¾Ý¿â")]
    public SoundDetailList_SO soundDetailList_SO;
    public SceneSoundList_SO sceneSoundList_SO;

    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource ambientSource;

    private Coroutine soundRoutine;

    [Header("Audio Mixer")]
    public AudioMixer gameAudioMixer;

    [Header("SnapShots")]
    public AudioMixerSnapshot normalSnapshot;
    public AudioMixerSnapshot ambientSnapshot;
    public AudioMixerSnapshot muteSnapshot;
    private float musicTransitionSecond = 4f;

    private float MusicStartSeconds => Random.Range(5f, 10f);

    public SoundDetails GetSoundDetailsfromName(SoundName soundname)
    {
        return soundDetailList_SO.soundDetailsList.Find(i => i.soundName == soundname);
    }

    public SceneSoundItem GetSceneSoundfromName(string sceneName)
    {
        return sceneSoundList_SO.sceneSoundList.Find(i => i.sceneName == sceneName);
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneLoad += OnAfterSceneLoad;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoad -= OnAfterSceneLoad;
    }

    private void Start()
    {
        OnAfterSceneLoad();
    }

    private void OnAfterSceneLoad()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        SceneSoundItem sceneSoundItem = GetSceneSoundfromName(currentScene);
        if (sceneSoundItem == null)
            return;
        SoundDetails music = GetSoundDetailsfromName(sceneSoundItem.music);
        SoundDetails ambient = GetSoundDetailsfromName(sceneSoundItem.ambient);

        if (soundRoutine != null)
        {
            StopCoroutine(soundRoutine);
        }
        soundRoutine = StartCoroutine(PlaySoundRoutine(music, ambient));


    }

    IEnumerator PlaySoundRoutine(SoundDetails music,SoundDetails ambient)
    {
        PlayAmbientClip(ambient,1f);
        yield return new WaitForSeconds(MusicStartSeconds);
        PlayMusicClip(music, musicTransitionSecond);
    }


    private void PlayMusicClip(SoundDetails music,float transitionTime)
    {
        if (music == null)
            return;
        gameAudioMixer.SetFloat("MusicVolume", ConvertSoundVolume(music.soundVloume));
        musicSource.clip = music.audioClip;
        if (musicSource.isActiveAndEnabled)
        {
            musicSource.Play();
        }
        normalSnapshot.TransitionTo(transitionTime);
    }

    private void PlayAmbientClip(SoundDetails ambient,float transitionTime)
    {
        if (ambient == null)
            return;
        gameAudioMixer.SetFloat("AmbientVolume", ConvertSoundVolume(ambient.soundVloume));
        ambientSource.clip = ambient.audioClip;
        if (ambientSource.isActiveAndEnabled)
        {
            ambientSource.Play();
        }
        ambientSnapshot.TransitionTo(transitionTime);
    }

    private float ConvertSoundVolume(float amount)
    {
        return (amount * 100 - 80);
    }
}
