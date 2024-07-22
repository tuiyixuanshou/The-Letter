using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SourceCheck : Singleton<SourceCheck>, IPointerClickHandler
{
    [Header("item")]
    public int itemTarNum;
    public Text itemTar;
    public int itemCurNum;
    public Text itemCur;

    [Header("NPC")]
    private int NPCTarNum;
    public Text NPCTar;
    public int NPCCurNum;
    public Text NPCCur;

    [Header("Player")]
    public Player player;

    private void OnEnable()
    {
        itemCurNum = player.GetNum;
        itemCur.text = itemCurNum.ToString();

        NPCTarNum = DayManager.Instance.NPCNum;
        NPCTar.text = NPCTarNum.ToString();
        NPCCurNum = DayManager.Instance.NPCCurNum;
        NPCCur.text = NPCCurNum.ToString();
    }

    private void Start()
    {
        itemTarNum = ItemManager.Instance.itemNum;
        itemTar.text = itemTarNum.ToString();
        itemCur.text = itemCurNum.ToString();

        
        
    }


    public void UpdateitemNum()
    {
        if (itemCurNum <= itemTarNum)
        {
            itemCurNum++;
            itemCur.text = itemCurNum.ToString();
        }
    }


    //¹Ø±Õ
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
       
        if (eventData.pointerCurrentRaycast.gameObject.name == "close")
        {
            this.gameObject.SetActive(false);
        }

    }
}
