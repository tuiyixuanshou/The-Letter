using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HomeEventButtonChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;  // 按钮原始的缩放比例
    public float zoomScale = 1.1f;  // 放大倍数
    private Image ButtonImage;
    private float oriColor;

    private void Start()
    {
        originalScale = transform.localScale;  // 记录按钮的原始缩放比例
        ButtonImage = this.GetComponent<Image>();
        oriColor = ButtonImage.color.a;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 鼠标指针进入按钮区域时触发
        ButtonImage.color = new Color(1, 1, 1, 1);
        // 放大按钮
        transform.localScale = originalScale * zoomScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 鼠标指针离开按钮区域时触发
        if(oriColor < 0.1f)
        {
            ButtonImage.color = new Color(1, 1, 1, 0);
        }
        // 还原按钮原始的缩放比例
        transform.localScale = originalScale;
    }
}
