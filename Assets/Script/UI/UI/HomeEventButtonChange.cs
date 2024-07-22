using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HomeEventButtonChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;  // ��ťԭʼ�����ű���
    public float zoomScale = 1.1f;  // �Ŵ���
    private Image ButtonImage;
    private float oriColor;

    private void Start()
    {
        originalScale = transform.localScale;  // ��¼��ť��ԭʼ���ű���
        ButtonImage = this.GetComponent<Image>();
        oriColor = ButtonImage.color.a;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���ָ����밴ť����ʱ����
        ButtonImage.color = new Color(1, 1, 1, 1);
        // �Ŵ�ť
        transform.localScale = originalScale * zoomScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ���ָ���뿪��ť����ʱ����
        if(oriColor < 0.1f)
        {
            ButtonImage.color = new Color(1, 1, 1, 0);
        }
        // ��ԭ��ťԭʼ�����ű���
        transform.localScale = originalScale;
    }
}
