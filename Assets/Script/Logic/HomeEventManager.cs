using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeEventManager : Singleton<HomeEventManager>
{
    //��ȡ�¼���SO�ļ�
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
    /// �Ѻ��У�����б���item
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
    /// ���������Ϣ��δ���У���Ҫ��дEventHandler����ĺ��з�������LayOutAdd֮�����
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
