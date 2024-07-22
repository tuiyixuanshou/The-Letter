using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonChangeSet : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    //private Vector3 originalScale;  // ��ťԭʼ�����ű���
    //public float zoomScale = 1.1f;  // �Ŵ���
    private Image ButtonImage;
    public Sprite ButtonImageSmall;
    public Sprite ButtonImageBig;
    public GameObject RedLine;

    private void Start()
    {
        ButtonImage = this.GetComponent<Image>();
        RedLine.SetActive(false);
        ButtonImage.sprite = ButtonImageSmall;
        ButtonImage.SetNativeSize();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���ָ����밴ť����ʱ����
        ButtonImage.sprite = ButtonImageBig;
        ButtonImage.SetNativeSize();


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonImage.sprite = ButtonImageSmall;
        ButtonImage.SetNativeSize();

    }

    public void ButtonClickedToPlayAmin()
    {
        ButtonImage.sprite = ButtonImageSmall;
        ButtonImage.SetNativeSize();
        RedLine.SetActive(true);
        Debug.Log(this.transform.GetSiblingIndex());
      
    }

}
