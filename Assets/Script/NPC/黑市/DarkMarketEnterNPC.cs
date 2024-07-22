using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DarkMarketEnterNPC : MonoBehaviour,IPointerClickHandler
{
    public GameObject DarkMarket;
  
    //黑市商人放最后
    public void SetDarkMarketNPCLast()
    {
        this.transform.SetAsLastSibling();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        //进入黑市
        if(eventData.pointerCurrentRaycast.gameObject.name == "黑市入口")
        {
            //关闭商店,启动黑市
            HomeUI.Instance.CloseShopAndShelves();
            DarkMarket.SetActive(true);
        }
      
    }

}
