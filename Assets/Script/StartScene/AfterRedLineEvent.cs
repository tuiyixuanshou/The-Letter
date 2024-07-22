using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterRedLineEvent : MonoBehaviour
{
    public GameObject FatherButton;
    public GameObject Fog;

    //red line动画事件
    public void AfterButtonPressedEvent()
    {
        //int index = this.transform.GetSiblingIndex();
        int index = FatherButton.transform.GetSiblingIndex();
        switch (index)
        {
            //开始游戏
            case 0:
                Debug.Log("点了开始");
                FogCoverStart(0);
                break;
            case 1:
                Debug.Log("点了继续");
                break;
            case 2:
                Debug.Log("点了退出");
                FogCoverStart(2);
                break;
            default:
                break;
        }
    }


    private void FogCoverStart(int index)
    {
        Fog.SetActive(true);
        Fog.GetComponent<FogChangeScene>().index = index;
    }


}
