using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    [Header("各地是否解锁")]
    public bool isRBshow;
    public bool isWarYshow;

    [Header("进入场景地图的按钮们")]
    public GameObject RBButton;
    public GameObject WarYButton;

    private void Start()
    {
        //更新各个地图
        IsRBshowFunc();
        IsWarYShow();
    }

    private void IsRBshowFunc()
    {
        //6001事件，通过老人的对话解锁科研基地
        isRBshow = NPCManager.Instance.GetNPCDetailsFromID(6001).isThisAppear;
        RBButton.SetActive(isRBshow);
    }

    private void IsWarYShow()
    {
        isWarYshow = HomeEventManager.Instance.GetHomeEventFromEventID(5102).isEventOver;
        WarYButton.SetActive(isWarYshow);
    }
}
