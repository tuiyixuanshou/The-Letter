using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : Singleton<TimeManager>
{
    public int gameSecond = 1;
    public int gameMinute = 1;

    public bool gameClockPause;
    public bool isTempPause;
    private float tikTime;
    public string ActiveSceneName;

    [Header("����ʱ�ı���ʾ")]
    public Text MinuteText;
    public Text SecondText;
    public GameObject Total;

    [Header("�������")]
    public GameObject WarnPanel;

    protected override void Awake()
    {
        base.Awake();
        EmptyOriData();
    }
    private void OnEnable()
    {
        EventHandler.AfterSceneLoad += OnAfterSceneLoad;
        EventHandler.NewGameEmptyData += EmptyOriData;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoad -= OnAfterSceneLoad;
        EventHandler.NewGameEmptyData -= EmptyOriData;
    }

    private void OnAfterSceneLoad()
    {
        ActiveSceneName = SceneManager.GetActiveScene().name;
        //reset time
        tikTime = 0;
        isStarttikTime();
    }

    private void EmptyOriData()
    {
        gameClockPause = true;
        Total.SetActive(false);
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
                UpdateTimeText();
            }
        }
    }

    private void UpdateTimeText()
    {
        if (gameSecond < 10)
        {
            SecondText.text = "0" + gameSecond.ToString();
        }
        else
        {
            SecondText.text = gameSecond.ToString();
        }

        MinuteText.text = "0" + gameMinute.ToString();
    }

    private void UpdateGameTime()
    {
        gameSecond--;
        if (gameSecond < 0)
        {
            gameMinute--;
            gameSecond = 59;
            if (gameMinute < 0)
            {
                gameMinute = 0;
                gameSecond = 0;
                gameClockPause = true;
                ActiveSceneName = SceneManager.GetActiveScene().name;
                //ʱ���ѵ���ǿ��ת������ͼ��д����
                if (ActiveSceneName == "SafetyArea" || ActiveSceneName == "ResearchBase" || ActiveSceneName == "WarYield")
                {
                    StartCoroutine(ForceTransitionToMap());
                }
                

            }
        }
    }

    IEnumerator ForceTransitionToMap()
    {
        WarnPanel.SetActive(true);
        var shinego = WarnPanel.transform.GetChild(0);
        //��˸����
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            shinego.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.8f);
            shinego.gameObject.SetActive(true);
        }
        //��ʼת��������ͼ
        EventHandler.CallHomeToMap();
        WarnPanel.SetActive(false);

    }

    private void isStarttikTime()
    {
        int daycount = DayManager.Instance.DayCount;
        switch (ActiveSceneName)
        {
            case "SafetyArea":
                Total.SetActive(true);
                gameClockPause = false;
                if (!isTempPause)
                {
                    if (daycount < 6)
                    {
                        //��ȫ������ʱ������
                        gameSecond = 0;
                        gameMinute = 3;
                    }
                    else if (daycount < 9)
                    {
                        //ս��
                        gameSecond = 40;
                        gameMinute = 1;
                    }
                    else
                    {
                        //����
                        gameSecond = 0;
                        gameMinute = 1;
                    }
                }
                isTempPause = false;
                UpdateTimeText();
                break;

            case "ResearchBase":
                Total.SetActive(true);
                gameClockPause = false;
                if (daycount < 6)
                {
                    //��ȫ������ʱ�ķ���
                    gameSecond = 0;
                    gameMinute = 4;
                }
                else if (daycount < 9)
                {
                    //ս��
                    gameSecond = 20;
                    gameMinute = 2;
                }
                else
                {
                    //����
                    gameSecond = 30;
                    gameMinute = 1;
                }
                UpdateTimeText();
                break;

            case "Church":
                Total.SetActive(true);
                gameClockPause = true;
                isTempPause = true;
                break;

            case "WarYield":
                Total.SetActive(true);
                gameClockPause = false;
                if (daycount < 4)
                {
                    //��ȫ������ʱ�ķ���
                    gameSecond = 0;
                    gameMinute = 4;
                }
                else if (daycount < 10)
                {
                    //ս��
                    gameSecond = 20;
                    gameMinute = 2;
                }
                else
                {
                    //����
                    gameSecond = 30;
                    gameMinute = 1;
                }
                UpdateTimeText();
                break;

            default:
                //����Ҫ��ʱ
                gameClockPause = true;
                Total.SetActive(false);
                gameClockPause = true;
                break;
        }
    }
}
