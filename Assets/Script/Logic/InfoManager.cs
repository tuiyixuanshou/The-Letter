using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : Singleton<InfoManager>
{
    public InfoList_SO infoList_SO;
    public bool isFindNewInfo;

    public bool[] DMInfoBugList;
    //查找
    public InfoDetails GetInfoDetailsFromID(int InfoID)
    {
        return infoList_SO.InfoDetailsList.Find(i => i.InfoID == InfoID);
    }

    //检查是否找到
    public bool CheckIfInfoBeFind(int InfoID)
    {
        return GetInfoDetailsFromID(InfoID).isInfoFind;
    }

    public void MarkInfoFind(int InfoID)
    {
        infoList_SO.InfoDetailsList.Find(i => i.InfoID == InfoID).isInfoFind = true;
    }

    //初始化
    protected override void Awake()
    {
        base.Awake();
        EmptyOriData();
    }
    private void EmptyOriData()
    {
        for (int i = 0; i < infoList_SO.InfoDetailsList.Count; i++)
        {
            if (infoList_SO.InfoDetailsList[i].InfoID != 0)
            {
                if (infoList_SO.InfoDetailsList[i].InfoID == 20001)
                {
                    infoList_SO.InfoDetailsList[i].isInfoFind = true;
                }
                else
                {
                    infoList_SO.InfoDetailsList[i].isInfoFind = false;
                }
            }
        }

        for(int j = 0;j < DMInfoBugList.Length; j++)
        {
            DMInfoBugList[j] = false;
        }
    }

    private void OnEnable()
    {
        EventHandler.ShowNewInfoBookSign += OnShowNewInfoSign;
        EventHandler.NewGameEmptyData += EmptyOriData;
    }

    private void OnDisable()
    {
        EventHandler.ShowNewInfoBookSign -= OnShowNewInfoSign;
        EventHandler.NewGameEmptyData -= EmptyOriData;
    }

    private void OnShowNewInfoSign()
    {
        Debug.Log("enable true");
        isFindNewInfo = true;
    }

    public bool CheckKeyInfos()
    {
        if (CheckIfInfoBeFind(20016))
        {
            if (CheckIfInfoBeFind(20017))
            {
                if (CheckIfInfoBeFind(20104))
                {
                    if (CheckIfInfoBeFind(20109))
                    {
                        if (CheckIfInfoBeFind(20115))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
