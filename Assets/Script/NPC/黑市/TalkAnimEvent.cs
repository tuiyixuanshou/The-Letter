using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkAnimEvent : MonoBehaviour
{
    //在DMButtonControl里面赋值
    public string TalkSentence;

    public Text Talk;

    public void SenctenceShowAnimEvent()
    {
        Talk.gameObject.SetActive(true);
        Talk.text = TalkSentence;
    }

    public void CloseSenctenceAnimEvent()
    {
        Talk.gameObject.SetActive(false);
    }
}
