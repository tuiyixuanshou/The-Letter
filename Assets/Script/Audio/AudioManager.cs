using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    [Header("声音数据库")]
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
    private float musicTransitionSecond = 2f;

    private string currentScene;
    private SoundDetails thismusic;
    private SoundDetails thisambient;

    private float MusicStartSeconds => Random.Range(3f, 5f);

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
        EventHandler.PlaySoundEvent += OnPlaySoundEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoad -= OnAfterSceneLoad;
        EventHandler.PlaySoundEvent -= OnPlaySoundEvent;
    }

    private void Start()
    {
        OnAfterSceneLoad();
    }


    //播放音乐的快捷方式
    private void OnPlaySoundEvent(SoundName soundName)
    {
        var soundDetails = GetSoundDetailsfromName(soundName); 
        if (soundDetails!= null)
        {
            EventHandler.CallInitSoundEffect(soundDetails);
        }

    }


    private void OnAfterSceneLoad()
    {
        currentScene = SceneManager.GetActiveScene().name;
       
        SceneSoundItem sceneSoundItem = GetSceneSoundfromName(currentScene);
        if (sceneSoundItem == null)
        {
            return;
        }
            
        thismusic = GetSoundDetailsfromName(sceneSoundItem.music);
        thisambient = GetSoundDetailsfromName(sceneSoundItem.ambient);

        //if (soundRoutine != null)
        //{
        //    muteSnapshot.TransitionTo(3f);
        //    StopCoroutine(soundRoutine);
        //}
        //soundRoutine = StartCoroutine(PlaySoundRoutine(thismusic, thisambient));
        StartCoroutine(StopOldMusic());

    }

    IEnumerator StopOldMusic()
    {
        muteSnapshot.TransitionTo(1f);
        yield return new WaitForSeconds(0.3f);
        if (soundRoutine != null)
        {
            StopCoroutine(soundRoutine);
        }
        soundRoutine = StartCoroutine(PlaySoundRoutine(thismusic, thisambient));
    }

    IEnumerator PlaySoundRoutine(SoundDetails music,SoundDetails ambient)
    {
        PlayAmbientClip(ambient,0.8f);
        if (currentScene != "IntroduceScene" && currentScene != "StartScene")
        {
            //yield return new WaitForSeconds(MusicStartSeconds);
            yield return new WaitForSeconds(0.01f);
        }
        PlayMusicClip(music, musicTransitionSecond);
    }


    private void PlayMusicClip(SoundDetails music,float transitionTime)
    {
        
        if (music == null)
        {
            musicSource.clip = null;
            normalSnapshot.TransitionTo(transitionTime);
            return;
        }
            
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
        {
            ambientSource.clip = null;
            ambientSnapshot.TransitionTo(transitionTime);
            return;
        }
            
        gameAudioMixer.SetFloat("AmbientVolume", ConvertSoundVolume(ambient.soundVloume));
        ambientSource.clip = ambient.audioClip;
        if (ambientSource.isActiveAndEnabled)
        {
            ambientSource.Play();
        }
        ambientSnapshot.TransitionTo(transitionTime);
    }


    public void ChangeBGMInDial(string newBGMName)
    {
        var name = (SoundName)System.Enum.Parse(typeof(SoundName), newBGMName);
        thismusic = GetSoundDetailsfromName(name);
        StartCoroutine(StopOldMusic());
        
    }


    private float ConvertSoundVolume(float amount)
    {
        return (amount * 100 - 80);
    }
}
