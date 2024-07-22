using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DMInfoControl : MonoBehaviour
{
    public Button BuyButton;
    public Button CheckButton;
    public GameObject InfoPanel;

    [Header("��Ʒ��Ϣ")]
    public Text InfoName;
    public string Infoname;
    public Text InfoShort;
    public string Short;
    [TextArea]
    public string InfoDescribe;
    public Text InfoPrice;
    public int Price;
    public int index;

    private void Init()
    {
        InfoName.text = Infoname;
        InfoShort.text = Short;
        InfoPrice.text = Price.ToString();
        if (!InfoManager.Instance.DMInfoBugList[index])
        {
            BuyButton.gameObject.SetActive(true);CheckButton.gameObject.SetActive(false); BuyButton.interactable = true;
        }
        else
        {
            BuyButton.gameObject.SetActive(false); CheckButton.gameObject.SetActive(true); BuyButton.interactable = true;
        }
    }

    private void OnEnable()
    {
        Init();
    }

    public void BuyButtonPressed()
    {
        int cur = PlayerProperty.Instance.PlayerWealth;
        if (cur - Price < 0)
        {
            BuyButton.interactable = false;
            EventHandler.CallPlaySoundEvent(SoundName.ButtonClick);
            string mtext = "�ʽ��㡭��";
            MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
        }
        else
        {
            PlayerProperty.Instance.PlayerWealth = cur - Price;
            EventHandler.CallPlaySoundEvent(SoundName.MoneyChange);
            InfoPanel.SetActive(true);
            InfoPanel.transform.GetChild(2).GetComponent<Text>().text = InfoDescribe;
            InfoManager.Instance.DMInfoBugList[index] = true;
            BuyButton.gameObject.SetActive(false); CheckButton.gameObject.SetActive(true);
        }
    }

    public void CheckButtonPressed()
    {
        InfoPanel.SetActive(true);
        InfoPanel.transform.GetChild(2).GetComponent<Text>().text = InfoDescribe;
    }

}
