using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainCanvasEquipButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float OriPos;       //��ʼλ��
    private float Unchangey;
    public float TarPos1;       //Ŀ��λ��
    public float TarPos2;       //Ŀ��λ��2
    public GameObject Equip;

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
        if(Equip.activeInHierarchy == false)
            StartCoroutine(ButtonMove(OriPos, 0.15f));
    }

    public void OnEquipButtonPressed()
    {
        StartCoroutine(ButtonMove(TarPos2, 0.15f));
    }

    public void OnEquipClosed()
    {
        StartCoroutine(ButtonMove(OriPos, 0.15f));
    }

    IEnumerator ButtonMove(float targetPos, float Duration)
    {
        float curPos = this.GetComponent<Image>().rectTransform.anchoredPosition.x;
        float speed = Mathf.Abs(curPos - targetPos) / Duration;
        while (!Mathf.Approximately(curPos, targetPos))
        {
            curPos = Mathf.MoveTowards(curPos, targetPos, speed*Time.deltaTime);
            this.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(curPos, Unchangey);
            yield return null;
        }
    }
}
