using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleInfoInBook : MonoBehaviour
{
    //人工填写InfoID
    public int InfoID;
    public bool isFind;
    public Image InfoImage;

    public Text InfoName;
    public Text InfoDecribe;
    public Image BlackCircle;
    public Image RedUnknow;

    private InfoDetails infoDetails = new();

    public void Init(int ID)
    {
        InfoID = ID;
        if (InfoID == 0)
            return;
        infoDetails = InfoManager.Instance.GetInfoDetailsFromID(InfoID);
        isFind = InfoManager.Instance.CheckIfInfoBeFind(InfoID);
        if (isFind)
        {
            //线索已找到
            //Debug.Log(infoDetails.InfoName);
            InfoName.text = infoDetails.InfoName;
            InfoDecribe.text = infoDetails.InfoDescribe;
            BlackCircle.enabled = true;
            RedUnknow.enabled = false;
            //InfoImage.color = new Color(1, 0, 0, 1);
            // Debug.Log("已找到线索");
        }
        else
        {
            //线索没找到
            InfoName.text = string.Empty;
            InfoDecribe.text = string.Empty;
            BlackCircle.enabled = false;
            RedUnknow.enabled = true;
            //InfoImage.color = new Color(0, 0, 0, 1);
            //Debug.Log("没找到线索");
            //this.gameObject.SetActive(false);
        }
    }
}
