using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    [Header("�����Ƿ����")]
    public bool isRBshow;
    public bool isWarYshow;

    [Header("���볡����ͼ�İ�ť��")]
    public GameObject RBButton;
    public GameObject WarYButton;

    private void Start()
    {
        //���¸�����ͼ
        IsRBshowFunc();
        IsWarYShow();
    }

    private void IsRBshowFunc()
    {
        //6001�¼���ͨ�����˵ĶԻ��������л���
        isRBshow = NPCManager.Instance.GetNPCDetailsFromID(6001).isThisAppear;
        RBButton.SetActive(isRBshow);
    }

    private void IsWarYShow()
    {
        isWarYshow = HomeEventManager.Instance.GetHomeEventFromEventID(5102).isEventOver;
        WarYButton.SetActive(isWarYshow);
    }
}
