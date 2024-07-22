using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleInfoInBook : MonoBehaviour
{
    //�˹���дInfoID
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
            //�������ҵ�
            //Debug.Log(infoDetails.InfoName);
            InfoName.text = infoDetails.InfoName;
            InfoDecribe.text = infoDetails.InfoDescribe;
            BlackCircle.enabled = true;
            RedUnknow.enabled = false;
            //InfoImage.color = new Color(1, 0, 0, 1);
            // Debug.Log("���ҵ�����");
        }
        else
        {
            //����û�ҵ�
            InfoName.text = string.Empty;
            InfoDecribe.text = string.Empty;
            BlackCircle.enabled = false;
            RedUnknow.enabled = true;
            //InfoImage.color = new Color(0, 0, 0, 1);
            //Debug.Log("û�ҵ�����");
            //this.gameObject.SetActive(false);
        }
    }
}
