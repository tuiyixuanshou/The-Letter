using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Map_GoOut : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float OriPos;       //初始位置
    private float Unchangey;
    public float TarPos1;       //目标位置
    public void callHomeToMap()
    {
        if(DayManager.Instance.DayCount == 2 || DayManager.Instance.DayCount == 5)
        {
            foreach(var i in FindObjectsOfType<HomeEventUI>())
            {
                if(i.HomeEventID == 5002 && !HomeEventManager.Instance.CheckIfEventDone(5002))
                {
                    i.ButtonPressed();
                    return;
                }
                else if(i.HomeEventID == 5026 && !HomeEventManager.Instance.CheckIfEventDone(5026))
                {
                    i.ButtonPressed(); return;
                }
            }
        }
        EventHandler.CallHomeToMap();
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.ButtonClick));
    }
    //public void Button_Fake()
    //{
    //    EventHandler.CallButton_ToENDGame();
    //}

    private void Start()
    {
        OriPos = this.GetComponent<Image>().rectTransform.anchoredPosition.x;
        Unchangey = this.GetComponent<Image>().rectTransform.anchoredPosition.y;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ButtonMove(TarPos1, 0.15f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(ButtonMove(OriPos, 0.15f));
    }

    IEnumerator ButtonMove(float targetPos, float Duration)
    {
        float curPos = this.GetComponent<Image>().rectTransform.anchoredPosition.x;
        float speed = Mathf.Abs(curPos - targetPos) / Duration;
        while (!Mathf.Approximately(curPos, targetPos))
        {
            curPos = Mathf.MoveTowards(curPos, targetPos, speed * Time.deltaTime);
            this.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(curPos, Unchangey);
            yield return null;
        }
    }
}
