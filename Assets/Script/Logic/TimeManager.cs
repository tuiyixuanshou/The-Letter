using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : Singleton<TimeManager>
{
    public int gameSecond;

    public bool gameClockPause;
    private float tikTime;
    public string ActiveSceneName;

    protected override void Awake()
    {
        base.Awake();
        gameClockPause = true;
    }
    private void OnEnable()
    {
        EventHandler.AfterSceneLoad += OnAfterSceneLoad;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoad -= OnAfterSceneLoad;
    }

    private void OnAfterSceneLoad()
    {
        ActiveSceneName = SceneManager.GetActiveScene().name;
        //reset time
        gameSecond = 0;
        tikTime = 0;
        isStarttikTime();
    }

    private void Update()
    {
        if (!gameClockPause)
        {
            tikTime += Time.deltaTime;

            if(tikTime >= Settings.secondThreshold)
            {
                tikTime -= Settings.secondThreshold;
                UpdateGameTime();
            }
        }
    }

    private void UpdateGameTime()
    {
        gameSecond++;
    }

    private void isStarttikTime()
    {
        switch (ActiveSceneName)
        {
            case "Home":
                gameClockPause = true;
                break;
            case "Map":
                gameClockPause = true;
                break;
            case "IntroduceScene":
                gameClockPause = true;
                break;

            default:
                //需要计时
                gameClockPause = false;
                break;
        }
    }
}
