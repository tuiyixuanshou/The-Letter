using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeEventManager : Singleton<HomeEventManager>
{
    //获取事件的SO文件
    public EventList_SO eventList_SO;

    public List<List<EventItemOnPanel>> EventDayList = new List<List<EventItemOnPanel>>();


    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < 14; i++)
        {
            List<EventItemOnPanel> EventList = new();

            EventDayList.Add(item: EventList);
        }
        foreach(var i in eventList_SO.HomeEventList)
        {
            i.isEventOver = false;
        }
    }

    private void OnEnable()
    {
        EventHandler.LayOutToAddEvent += OnLayOutToAddEvent;
    }

    private void OnDisable()
    {
        EventHandler.LayOutToAddEvent -= OnLayOutToAddEvent;
    }

    private void Start()
    {
        
    }

    /// <summary>
    /// 已呼叫，添加列表新item
    /// </summary>
    /// <param name="eventContain"></param>
    private void OnLayOutToAddEvent(string eventContain)
    {
        EventItemOnPanel newEvent = new ()
        {
            EventContain = eventContain
        };

        int Day = DayManager.Instance.DayCount;

        EventDayList[Day-1].Add(newEvent);
    }

    /// <summary>
    /// 添加其他信息，未呼叫，需要另写EventHandler里面的呼叫方法，在LayOutAdd之后调用
    /// </summary>
    /// <param name="OtherDetails"></param>
    private void LayOutEventOtherDetails(string OtherDetails)
    {
        int i = EventDayList[DayManager.Instance.DayCount].Count - 1;
        EventDayList[DayManager.Instance.DayCount][i].EventNPCName = OtherDetails;
    }


    public HomeEvent GetHomeEventFromEventID(int EventID)
    {
        return eventList_SO.HomeEventList.Find(i => i.EventID == EventID);
    }

    public void MarkEventDone(int eventID)
    {
        eventList_SO.HomeEventList.Find(i => i.EventID == eventID).isEventOver = true;
    }
}
