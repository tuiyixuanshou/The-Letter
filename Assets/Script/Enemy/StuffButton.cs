using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffButton : MonoBehaviour
{
    public GameObject StuffLine;


    //����StuffLine����
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

    //��stuffline����ѡ���õ�
    public void CloseStuffLineByLineButton()
    {
        StuffLine.SetActive(false);
    }
}
