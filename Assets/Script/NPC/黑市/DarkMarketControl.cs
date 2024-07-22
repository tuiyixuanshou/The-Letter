using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkMarketControl : MonoBehaviour
{
    public Image DarkMarketFade;
    public GameObject DarkMarketBG;
    public Animator SoldPanelAnim;
    public GameObject DarkMarketEnter;

    [Header("黑市物品面板")]
    public GameObject EWPanel;
    public GameObject ItemPanel;
    public GameObject InfoPanel;

    [Header("黑市按钮面板")]
    public DMButtonControl InfoButton;
    public DMButtonControl Button1;
    public DMButtonControl Button2;

    private void OnEnable()
    {
        StartCoroutine(DarkMarketOpenSteps());
    }

    //进入黑市时的渐入
    IEnumerator DarkMarketOpenSteps()
    {
        DarkMarketFade.color = new Color(0, 0, 0, 0);
        DarkMarketFade.raycastTarget = true;
        yield return TransitionFade(1, 1f);
        DarkMarketBG.SetActive(true);
        EWPanel.GetComponent<LayOutInsEquip>().InitDayEWItem();
        ItemPanel.GetComponent<LayOutInsEquip>().InitDayEWItem();
        InfoButton.OnButtonPressed();
        Button1.ResetPos(); Button2.ResetPos();
        yield return new WaitForSeconds(0.5f);
        SoldPanelAnim.SetTrigger("SoldPanelIn");
        DarkMarketFade.raycastTarget = false;
        yield return TransitionFade(0, 0.3f);
    }

    IEnumerator TransitionFade(float targetAlpha, float transitionFadeDuration)
    {
        float a = DarkMarketFade.color.a;
        float speed = Mathf.Abs(a - targetAlpha) / transitionFadeDuration;

        while (!Mathf.Approximately(a, targetAlpha))
        {
            a = Mathf.MoveTowards(a, targetAlpha, speed * Time.deltaTime);
            DarkMarketFade.color = new Color(0, 0, 0, a);
            yield return null;
        }
    }

    //离开黑市，回到shop中
    public void LeaveButtonPressed()
    {
        EventHandler.CallPlaySoundEvent(SoundName.ButtonClick);
        StartCoroutine(DarkMarketClose());
    }

    IEnumerator DarkMarketClose()
    {
        DarkMarketFade.raycastTarget = true;
        yield return TransitionFade(1, 0.8f);
        DarkMarketBG.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        yield return TransitionFade(0, 0.3f);
        DarkMarketEnter.SetActive(false);
        HomeUI.Instance.OpenShopButtonPressed();
        this.gameObject.SetActive(false);
    }
}
