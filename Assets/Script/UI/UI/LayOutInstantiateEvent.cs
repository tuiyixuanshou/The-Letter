using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LayOutInstantiateEvent : MonoBehaviour
{
    public GameObject OneEvent;

    public GameObject DayGroup;

    public int ControlDay;

    private void OnEnable()
    {
        DeletOldEventItem();
        instantiateEventItem();
    }

    private void Start()
    {
        //start无论激活吊销多少次都只调用一次
        //instantiateEventItem();
        //DeletOldEventItem();
        //instantiateEventItem();
    }


    /// <summary>
    ///  清除原有的事件
    /// </summary>
    private void DeletOldEventItem()
    {
        if (this.GetComponentsInChildren<Image>().Length != 0)
        {
            foreach (var i in DayGroup.GetComponentsInChildren<Image>())
            {
                Destroy(i.gameObject);
            }
        }
    }

    /// <summary>
    /// 每次重启时都重新刷新，刷新面板为第一天
    /// </summary>
    private void instantiateEventItem()
    {

        var currentList = HomeEventManager.Instance.EventDayList[ControlDay-1];
        foreach(var i in currentList)
        {
            var newEvent = Instantiate(OneEvent, DayGroup.transform);
            newEvent.transform.GetChild(0).gameObject.GetComponent<Text>().text = i.EventContain;

            //可以添加其他信息

        }
    }


    private void UpdateLayOutGroup()
    {

    }

    

    private void OnLayOutToAddEvent(string textContain)
    {
        //var newEvent = Instantiate(OneEvent, this.transform);
        //newEvent.transform.GetChild(3).gameObject.GetComponent<Text>().text = textContain;

    }

   
}
