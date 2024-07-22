using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayOutInsEquip : MonoBehaviour
{
    [Header("��Ʒ��")]
    public int DMEquipAmount;
    public List<int> DMEquipList;

    [Header("��Ʒ��Prefab")]
    public GameObject ItemPrefab;

    private void OnEnable()
    {
        //DeletOldDMItem();
        //InitDayEWItem();
    }

    private void Start()
    {
        
    }

    public void InitDayEWItem()
    {
        DeletOldDMItem();
        int[] EWItem = GetRandomArray(8, 1, 15);
        foreach(int ID in EWItem)
        {
            GameObject newDMItem = Instantiate(ItemPrefab, this.transform);
            newDMItem.GetComponent<DMitemControl>().initDMitem(DMEquipList[ID-1]);
        }
    }

    private void DeletOldDMItem()
    {
        if (this.GetComponentsInChildren<DMitemControl>().Length != 0)
        {
            foreach (var i in this.GetComponentsInChildren<DMitemControl>())
            {
                Destroy(i.gameObject);
            }
        }
    }


    //�������ĳһ����Χ�ڲ��ظ�������
    public int[] GetRandomArray(int Number, int minNum, int maxNum)
    {
        int j; 
        int[] b = new int[Number];

        for(j = 0; j < Number; j++)
        {
            int i = Random.Range(minNum, maxNum + 1);
            int num = 0;
            for(int m = 0; m < j; m++)
            {
                if(b[m] == i)
                {
                    num ++;
                }
            }
            if(num == 0)
            {
                b[j] = i;
            }
            else
            {
                j--;
            }
        }
        return b;
    }

}
