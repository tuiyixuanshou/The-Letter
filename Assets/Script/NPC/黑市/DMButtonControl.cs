using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DMButtonControl : MonoBehaviour
{
    //private float oriPosx;

    [Header("��Ʒҳ�����")]
    public GameObject DMPanel1;
    public GameObject DMPanel2;
    public GameObject DMPanel3;
    public GameObject TalkThing;
    public string TalkSentence;

    [Header("�滻ͼƬ")]
    public Sprite OriSprite;
    public Sprite HlightSprite;

    private void Start()
    {
        //oriPosx = this.GetComponent<Image>().rectTransform.anchoredPosition.x;
    }

    public void OnButtonPressed()
    {
        this.GetComponent<Image>().sprite = HlightSprite;
        this.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(-430, this.GetComponent<Image>().rectTransform.anchoredPosition.y);
        EventHandler.CallPlaySoundEvent(SoundName.ButtonClick);
        DMPanel1.SetActive(true);
        DMPanel2.SetActive(false);
        DMPanel3.SetActive(false);
        //ˢ�¶Ի�����
        TalkThing.SetActive(false);
        TalkThing.SetActive(true);
        TalkThing.GetComponent<TalkAnimEvent>().TalkSentence = TalkSentence;
    }

    public void ResetPos()
    {
        this.GetComponent<Image>().sprite = OriSprite;
        this.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(-397, this.GetComponent<Image>().rectTransform.anchoredPosition.y);
    }
}
