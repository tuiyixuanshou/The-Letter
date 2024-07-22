using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasUITip : Singleton<MainCanvasUITip>
{
    public GameObject UITip;

    public void SetActiveUITip(string TipText)
    {
        UITip.SetActive(true);
        UITip.transform.GetChild(0).GetComponent<Text>().text = TipText;
    }

    public void SetUnActiveUITip()
    {
        UITip.SetActive(false);
    }

    public void UITipShowAndDisappear(string TipText)
    {
        StartCoroutine(UITIPRoutine(TipText));
    }


    IEnumerator UITIPRoutine(string TipText)
    {
        UITip.SetActive(true);
        UITip.transform.GetChild(0).GetComponent<Text>().text = TipText;
        yield return new WaitForSeconds(2f);
        UITip.SetActive(false);
    }
}
