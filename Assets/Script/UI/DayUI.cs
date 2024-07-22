using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayUI : Singleton<DayUI>
{

    public Text DayCountTextGe;
    public Text DayCountTextShi;
    private CanvasGroup fadeCanvasGroup;
    //private bool isFade;
    //public Button NextDayButton;

    private void OnEnable()
    {
        EventHandler.Button_AddDay += OnButton_AddDay;
        EventHandler.Button_AddDay += OnButton_UpdateText;

    }

    private void OnDisable()
    {
        EventHandler.Button_AddDay -= OnButton_AddDay;
        EventHandler.Button_AddDay -= OnButton_UpdateText;
    }

    private void Start()
    {
        fadeCanvasGroup = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<CanvasGroup>();
        DayManager.Instance.DayCount = 1;
        UpdateTextCount();

    }

    //增加天数
    private void OnButton_AddDay()
    {
        DayManager.Instance.DayCount++;

    }

    //更新文本
    public void UpdateTextCount()
    {
        int DayCount = DayManager.Instance.DayCount;
        if (DayCount < 10)
        {
            DayCountTextGe.text = DayCount.ToString();
            DayCountTextShi.text = 0.ToString();
        }
        else
        {
            DayCountTextGe.text = (DayCount-10).ToString();
            DayCountTextShi.text = 1.ToString();

        }
    }

    IEnumerator TransitionFadeCanvas(float targetAlpha, float transitionFadeDuration)
    {
        //isFade = true;
        //fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / transitionFadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            //yield return new WaitForSeconds(0.01f);
            yield return null;
        }
        //isFade = false;
        //fadeCanvasGroup.blocksRaycasts = false;

        yield return new WaitForSeconds(1.5f);

    }

    IEnumerator ToTheNextDay()
    {
        fadeCanvasGroup.blocksRaycasts = true;
        yield return TransitionFadeCanvas(1, 0.7f);

        //1.更新天数UI
        UpdateTextCount();

        //2.更新账单昨日余额和人物属性
        PlayerProperty.Instance.ChangePPtheNextDay();

        //3.展示与更新面板内容
        GameObject.FindGameObjectWithTag("ConsumePanel").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.FindGameObjectWithTag("ConsumePanel").GetComponent<CanvasGroup>().blocksRaycasts = true;


        HomeUI.Instance.DoTheButton();

        DayManager.Instance.instantiateHomeEventInMorning();
        HomeDisPlayer.Instance.changeMorningSprite();
        yield return TransitionFadeCanvas(0, 0.3f);
        fadeCanvasGroup.blocksRaycasts = false;
    }

    private void OnButton_UpdateText()
    {
        StartCoroutine(ToTheNextDay());
    }
}
