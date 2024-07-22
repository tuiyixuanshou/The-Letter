using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_ToMap : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject Panel;

    private float OriPos;       //初始位置
    private float Unchangey;
    public float TarPos1;       //目标位置


    private void OnEnable()
    {
        EventHandler.ClosePanelAndButtonWhenFight += ControlButton;
    }

    private void OnDisable()
    {
        EventHandler.ClosePanelAndButtonWhenFight -= ControlButton;
    }

    private void ControlButton(BattleState state)
    {
        this.gameObject.GetComponent<Button>().interactable = !this.gameObject.GetComponent<Button>().IsInteractable();
    }

    private void Start()
    {
        OriPos = this.GetComponent<Image>().rectTransform.anchoredPosition.x;
        Unchangey = this.GetComponent<Image>().rectTransform.anchoredPosition.y;
    }


    public void OnButtonPressed()
    {
        //if(/*!TimeManager.Instance.gameClockPause &&*/ (TimeManager.Instance.gameSecond > 0 || TimeManager.Instance.gameMinute > 0))
        //{
        //    Panel.SetActive(true);
        //    TimeManager.Instance.gameClockPause = true;
        //    BattleSystem.Instance.gameClockPause = true;
        //}
        Panel.SetActive(true);
        TimeManager.Instance.gameClockPause = true;
        BattleSystem.Instance.gameClockPause = true;
    }

    public void OnLeftButtonPressed()
    {
        Panel.SetActive(false);
        EventHandler.CallHomeToMap();
    }

    public void OnStayButtonPressed()
    {
        TimeManager.Instance.gameClockPause = false;
        BattleSystem.Instance.gameClockPause = false;
        Panel.SetActive(false);
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
