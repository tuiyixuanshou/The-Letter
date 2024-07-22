using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffButton : MonoBehaviour
{
    public GameObject StuffLine;


    //控制StuffLine开关
    public void OnStuffButtonPressed()
    {
        if(StuffLine.activeInHierarchy)
        {
            StuffLine.SetActive(false);
        }
        else
        {
            StuffLine.SetActive(true);
        }
    }

    //给stuffline里面选中用的
    public void CloseStuffLineByLineButton()
    {
        StuffLine.SetActive(false);
    }
}
