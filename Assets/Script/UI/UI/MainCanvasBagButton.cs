using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MainCanvasBagButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float OriPos;       //初始位置
    private float Unchangey;
    public float TarPos1;       //目标位置
    public float TarPos2;       //目标位置2
    public GameObject Bag;

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
        if(Bag.activeInHierarchy == false)
        {
            StartCoroutine(ButtonMove(OriPos, 0.15f));
        }
    }


    public void OnBagButtonPressed()
    {
        StartCoroutine(ButtonMove(TarPos2, 0.15f));
    }

    public void OnBagClosed()
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
