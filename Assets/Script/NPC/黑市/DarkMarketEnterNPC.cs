using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DarkMarketEnterNPC : MonoBehaviour,IPointerClickHandler
{
    public GameObject DarkMarket;
  
    //�������˷����
    public void SetDarkMarketNPCLast()
    {
        this.transform.SetAsLastSibling();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        //�������
        if(eventData.pointerCurrentRaycast.gameObject.name == "�������")
        {
            //�ر��̵�,��������
            HomeUI.Instance.CloseShopAndShelves();
            DarkMarket.SetActive(true);
        }
      
    }

}
