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
        //start���ۼ���������ٴζ�ֻ����һ��
        //instantiateEventItem();
        //DeletOldEventItem();
        //instantiateEventItem();
    }


    /// <summary>
    ///  ���ԭ�е��¼�
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
    /// ÿ������ʱ������ˢ�£�ˢ�����Ϊ��һ��
    /// </summary>
    private void instantiateEventItem()
    {

        var currentList = HomeEventManager.Instance.EventDayList[ControlDay-1];
        foreach(var i in currentList)
        {
            var newEvent = Instantiate(OneEvent, DayGroup.transform);
            newEvent.transform.GetChild(0).gameObject.GetComponent<Text>().text = i.EventContain;

            //�������������Ϣ

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
