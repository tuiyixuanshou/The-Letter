using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterRedLineEvent : MonoBehaviour
{
    public GameObject FatherButton;
    public GameObject Fog;

    //red line�����¼�
    public void AfterButtonPressedEvent()
    {
        //int index = this.transform.GetSiblingIndex();
        int index = FatherButton.transform.GetSiblingIndex();
        switch (index)
        {
            //��ʼ��Ϸ
            case 0:
                Debug.Log("���˿�ʼ");
                FogCoverStart(0);
                break;
            case 1:
                Debug.Log("���˼���");
                break;
            case 2:
                Debug.Log("�����˳�");
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
