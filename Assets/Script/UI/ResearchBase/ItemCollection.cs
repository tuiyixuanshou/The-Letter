using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    [Header("�ں���Ʒ���б�")]
    public List<int> ItemIDList;

    [Header("������Ϣ")]
    public GameObject UISign;
    public bool isSearched;     //�Ƿ���������
    public bool isCanSearched;  //�ܷ�����
    public string Contains;

    private void Start()
    {
        var list = ItemManager.Instance.ReLoadCollection(this.transform.GetSiblingIndex());
        if(list != null)
        {
            Init(list);
        }

    }

    public void Init(List<int> itemIDList)
    {
        //Debug.Log(itemIDList.Count);
        ItemIDList = new();
        foreach (var itemID in itemIDList)
        {
            ItemIDList.Add(itemID);
        }
        UISign.SetActive(false);
        isSearched = false;
        isCanSearched = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isSearched)
            {
                UISign.SetActive(true);
                isCanSearched = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCanSearched = false;
        UISign.SetActive(false);
    }

    private void Update()
    {
        if (!isSearched && isCanSearched && Input.GetKeyDown(KeyCode.F))
        {
            isSearched = true;
            InventoryManager.Instance.AddBagItemsFromCollection(ItemIDList, Contains);
            ItemIDList = new();
        }
    }
}
