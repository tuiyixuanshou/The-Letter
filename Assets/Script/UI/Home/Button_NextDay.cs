using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_NextDay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject EventUnDonePanel;

    private float OriPos;       //��ʼλ��
    private float Unchangey;
    public float TarPos1;       //Ŀ��λ��

    private void Start()
    {
        OriPos = this.GetComponent<Image>().rectTransform.anchoredPosition.x;
        Unchangey = this.GetComponent<Image>().rectTransform.anchoredPosition.y;
    }


    public void callButton_AddDay()
    {
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.ButtonClick));
        foreach (var i in FindObjectsOfType<HomeEventUI>())
        {
            int dayCount = DayManager.Instance.DayCount;
            //�Ź㲥
            if (dayCount == 1 && i.HomeEventID == 5102)
            {
                i.ButtonPressed();return;
            }
            else if(dayCount == 3 && i.HomeEventID == 5104)
            {
                i.ButtonPressed(); return;
            }
            else if(dayCount == 4 &&i.HomeEventID == 5024)
            {
                i.ButtonPressed(); return;
            }
            else if (dayCount == 5 && i.HomeEventID == 5103)
            {
                i.ButtonPressed(); return;
            }
            else if(dayCount == 8 && i.HomeEventID == 5032)
            {
                i.ButtonPressed(); return;
            }
            else if (dayCount == 9 && i.HomeEventID == 5040)
            {
                i.ButtonPressed(); return;
            }
            else if (dayCount == 10 && i.HomeEventID == 5041)
            {
                i.ButtonPressed(); return;
            }

            if (!HomeEventManager.Instance.CheckIfEventDone(i.HomeEventID))
            {
                EventUnDonePanel.SetActive(true);
                return;
            }
        }
        //foreach(var i in FindObjectsOfType<HomeEventUI>())
        //{
        //    Destroy(i.gameObject); ���д��homeui dobutton�����ˣ������Ǻ���֮������ʧ
        //}

        //�����޸���DayUI����
        EventHandler.CallButton_AddDay();
        
    }



    //������
    public void callButton_ChangePlayerP()
    {
        if (FindObjectsOfType<HomeEventUI>().Length == 0)
        {
            EventHandler.CallButton_ChangePlayerPorperty();
        }
        else
        {
            EventUnDonePanel.SetActive(true);
        }
            
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
