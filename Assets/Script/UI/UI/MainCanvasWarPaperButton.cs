using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainCanvasWarPaperButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float OriPos;       //初始位置
    private float Unchangey;
    public float TarPos1;       //目标位置
    public float TarPos2;       //目标位置2
    public GameObject WarPaper;

    private void Start()
    {
        OriPos = this.GetComponent<Image>().rectTransform.anchoredPosition.x;
        Unchangey = this.GetComponent<Image>().rectTransform.anchoredPosition.y;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ButtonMove(TarPos1, 0.15f));
        EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.RubButton));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (WarPaper.activeInHierarchy == false)
        {
            StartCoroutine(ButtonMove(OriPos, 0.15f));
            //EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.RubButton));
        }
            
    }

    public void OnWarParperButtonPressed()
    {
        StartCoroutine(ButtonMove(TarPos2, 0.15f));

    }

    public void OnWarParperClosed()
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
