using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateSelectButtons : Singleton<InstantiateSelectButtons>
{
    public Transform LayOutParent;

    //��û����Ԥ�Ƽ�
    public GameObject SelectedButtonPrefab;

    public GameObject SelectPanel;

    public GameObject DialoguePanel;

    InventoryUI inventoryUI => FindObjectOfType<InventoryUI>();

    public void ShowSelectPanel()
    {
        SelectPanel.SetActive(true);
        DialoguePanel.SetActive(false);

    }



    public void instantiateShopButtons(int NPCID,int DialogueIndex)
    {
        //����ID�ͶԻ�ƥ�����ɵİ�ť
        //1���̵���˵�ѡ��
        switch (NPCID)
        {
            case 1001:
                switch (DialogueIndex)
                {
                    case 0:
                        var newButton1001_0_1 = LayOutParent.GetChild(0).gameObject;
                        newButton1001_0_1.SetActive(true);
                        newButton1001_0_1.GetComponent<SelectedButton>().InitSelectButton("�������ǣ���60%�۸��չ�",
                            1,1);
                        
                        var newButton1001_0_2 = LayOutParent.GetChild(1).gameObject;
                        newButton1001_0_2.SetActive(true);
                        newButton1001_0_2.GetComponent<SelectedButton>().InitSelectButton("��ҲûǮ����80%�۸��չ�",
                           /* NPCManager.Instance.GetSingleDialogue(1001).dialogueElemList[2]*/2,1);

                        break;
                    default:
                        break;
                }
                break;
            case 1030:
                switch (DialogueIndex)
                {
                    case 0:
                        var newButton1030_0_1 = LayOutParent.GetChild(0).gameObject;
                        newButton1030_0_1.SetActive(true);
                        newButton1030_0_1.GetComponent<SelectedButton>().InitSelectButton("��ʵ����",1, 1);
                        var newButton1030_0_2 = LayOutParent.GetChild(1).gameObject;
                        newButton1030_0_2.SetActive(true);
                        newButton1030_0_2.GetComponent<SelectedButton>().InitSelectButton("Ͷ��ȡ��",2, 1);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }


    public void InstantiateHomeEventButton(int HomeEventID)
    {
        //���ɵ����¼�
        //2�Ǽ��¼���ѡ��
        switch (HomeEventID)
        {
            case 5002:
                var newButton5002_1 = LayOutParent.GetChild(0).gameObject;
                newButton5002_1.SetActive(true);
                newButton5002_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("������Ϊ�������Ͻ�����"
                    ,5003,2);

                var newButton5002_2 = LayOutParent.GetChild(1).gameObject;
                newButton5002_2.SetActive(true);
                newButton5002_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("��֮������ȫ����Ҫ��"
                    , 5004, 2);
                break;

            case 5006:
                var newButton5002_3 = LayOutParent.GetChild(0).gameObject;
                newButton5002_3.SetActive(true);
                newButton5002_3.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("��������"
                    , 5007, 2);

                var newButton5002_4 = LayOutParent.GetChild(1).gameObject;
                newButton5002_4.SetActive(true);
                newButton5002_4.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("����������"
                    , 5008, 2);
                break;
            case 5009:
                var newButton5009_1 = LayOutParent.GetChild(0).gameObject;
                newButton5009_1.SetActive(true);
                newButton5009_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("��ס�����"
                    , 5010, 2);

                var newButton5009_2 = LayOutParent.GetChild(1).gameObject;
                newButton5009_2.SetActive(true);
                newButton5009_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("���ֳ�Ĭ"
                    , 5011, 2);
                break;
            case 5013:
                var newButton5013_1 = LayOutParent.GetChild(0).gameObject;
                newButton5013_1.SetActive(true);
                if (HomeEventManager.Instance.CheckIfEventDone(5007))
                {
                    newButton5013_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("��������", 5014, 2);
                }
                else
                {
                    newButton5013_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("��������", 5015, 2);
                }
               
                var newButton5013_2 = LayOutParent.GetChild(1).gameObject;
                newButton5013_2.SetActive(true);
                newButton5013_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("����������", 5008, 2);
                break;
            case 5020:
                var newButton5020_1 = LayOutParent.GetChild(0).gameObject; newButton5020_1.SetActive(true);
                newButton5020_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("��Ǯ��׬���˵�", 5021, 2);
              
                var newButton5020_2 = LayOutParent.GetChild(1).gameObject;
                newButton5020_2.SetActive(true);
                newButton5020_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("�ϴʾܾ�", 5022, 2);
                break;
            case 5024:
                var newButton5024_1 = LayOutParent.GetChild(0).gameObject; newButton5024_1.SetActive(true);
                newButton5024_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("��" , 5025, 2);

                var newButton5024_2 = LayOutParent.GetChild(1).gameObject; newButton5024_2.SetActive(true);
                newButton5024_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("����" , 5025, 2);
                break;
            case 5027:
                var newButton5027_1 = LayOutParent.GetChild(0).gameObject; newButton5027_1.SetActive(true);
                newButton5027_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("��", 5028, 2);

                var newButton5027_2 = LayOutParent.GetChild(1).gameObject; newButton5027_2.SetActive(true);
                newButton5027_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("����", 5029, 2);
                break;
            case 5033:
                var newButton5033_1 = LayOutParent.GetChild(0).gameObject; newButton5033_1.SetActive(true);
                newButton5033_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("��Ӧ����ɯ", 5034, 2);

                var newButton5033_2 = LayOutParent.GetChild(1).gameObject; newButton5033_2.SetActive(true);
                newButton5033_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("�ܾ�����ɯ", 5035, 2);
                break;
            case 5047:
                var newButton5047_1 = LayOutParent.GetChild(0).gameObject; newButton5047_1.SetActive(true);
                newButton5047_1.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("��Ӧ����ɯ", 5048, 2);
                var newButton5047_2 = LayOutParent.GetChild(1).gameObject; newButton5047_2.SetActive(true);
                newButton5047_2.GetComponent<SelectedButton>().InitSelectButtonHomeEvent("�ܾ�����ɯ", 5049, 2);
                break;
            default:
                break;
        }
    }

    public void InstantiateOutNPCButton(int NPCID)
    {
        switch (NPCID)
        {
            case 6004:
                var newButton6004_1 = LayOutParent.GetChild(0).gameObject;
                newButton6004_1.SetActive(true);
                newButton6004_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("��������", 6005, 3);

                var newButton6004_2 = LayOutParent.GetChild(1).gameObject;
                newButton6004_2.SetActive(true);
                newButton6004_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("���ǲ���", 6006, 3);

                break;

            case 9003:
                var newButton9003_1 = LayOutParent.GetChild(0).gameObject;
                newButton9003_1.SetActive(true);
                newButton9003_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("ʲôҲ����", 8198, 3);

                var newButton9003_2 = LayOutParent.GetChild(1).gameObject;
                newButton9003_2.SetActive(true);
                newButton9003_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("����һ����", 8197, 3);

                var newButton9003_3 = LayOutParent.GetChild(2).gameObject;
                newButton9003_3.SetActive(true);
                newButton9003_3.GetComponent<SelectedButton>().InitSelectButtonOutNPC("ȫ������", 8196, 3);
                break;
            case 8004:
                var newButton8004_1 = LayOutParent.GetChild(0).gameObject;
                newButton8004_1.SetActive(true);
                newButton8004_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("�ṩ����", 8005, 3);

                var newButton8004_2 = LayOutParent.GetChild(1).gameObject;
                newButton8004_2.SetActive(true);
                newButton8004_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("�����ѱ�", 8006, 3);
                break;
            case 8013:
                var newButton8013_1 = LayOutParent.GetChild(0).gameObject;
                newButton8013_1.SetActive(true);
                newButton8013_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("�ṩ����", 8015, 3);

                var newButton8013_2 = LayOutParent.GetChild(1).gameObject;
                newButton8013_2.SetActive(true);
                newButton8013_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("���ܲ���", 8016, 3);
                break;
            case 6021:
                var newButton6021_1 = LayOutParent.GetChild(0).gameObject;newButton6021_1.SetActive(true);
                newButton6021_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("��������", 6022, 3);

                var newButton6021_2 = LayOutParent.GetChild(1).gameObject;newButton6021_2.SetActive(true);
                newButton6021_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("��Ĭ����", 6023, 3);

                break;
            case 9013:
                var newButton9013_1 = LayOutParent.GetChild(0).gameObject; newButton9013_1.SetActive(true);
                newButton9013_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("������", 9014, 3);

                var newButton9013_2 = LayOutParent.GetChild(1).gameObject; newButton9013_2.SetActive(true);
                newButton9013_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("������", 9015, 3);
                break;
            case 8120:
                var newButton8120_1 = LayOutParent.GetChild(0).gameObject; newButton8120_1.SetActive(true);
                newButton8120_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("���Դ�", 8121, 3);

                var newButton8120_2 = LayOutParent.GetChild(1).gameObject; newButton8120_2.SetActive(true);
                newButton8120_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("������Σ��", 8122, 3);
                break;
            case 6026:
                var newButton6026_1 = LayOutParent.GetChild(0).gameObject; newButton6026_1.SetActive(true);
                newButton6026_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("���з��ң�����-10��", 6027, 3);

                var newButton6026_2 = LayOutParent.GetChild(1).gameObject; newButton6026_2.SetActive(true);
                newButton6026_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("׳���ܾ�������-10��", 6028, 3);
                break;
            case 8021:
                var newButton8021_1 = LayOutParent.GetChild(0).gameObject; newButton8021_1.SetActive(true);
                newButton8021_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("��������", 8022, 3);

                var newButton8021_2 = LayOutParent.GetChild(1).gameObject; newButton8021_2.SetActive(true);
                newButton8021_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("��������", 8023, 3);
                break;
            case 8007:
                var newButton8007_1 = LayOutParent.GetChild(0).gameObject; newButton8007_1.SetActive(true);
                newButton8007_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("����", 8008, 3);
                var newButton8007_2 = LayOutParent.GetChild(1).gameObject; newButton8007_2.SetActive(true);
                newButton8007_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("����", 8009, 3);
                break;
            case 6031:
                var newButton6031_1 = LayOutParent.GetChild(0).gameObject; newButton6031_1.SetActive(true);
                newButton6031_1.GetComponent<SelectedButton>().InitSelectButtonOutNPC("����", 6032, 3);
                var newButton6031_2 = LayOutParent.GetChild(1).gameObject; newButton6031_2.SetActive(true);
                newButton6031_2.GetComponent<SelectedButton>().InitSelectButtonOutNPC("������", 6033, 3);
                break;

            default:
                break;
        }
    }

    public void InstantiateRandomGiveThing(int GiveOptionNum,int itemShortInitID)
    {
        int i = 0;
        int[] ID = { 0,0,0,0,0,0};int[] Amount = { 0, 0, 0, 0, 0, 0 };
        foreach(var item in InventoryManager.Instance.bagItemList_SO.bagItemList)
        {
            if (item.BagItemID != 0)
            {
                if (i < GiveOptionNum)
                {
                    ID[i] = item.BagItemID;
                    Amount[i] = item.BagItemAmount;
                    i++;
                }
                
            }
        }

        for(int j = 0; j < GiveOptionNum; j++)
        {
            if(ID[j] != 0)
            {
                var giveThing = LayOutParent.GetChild(j).gameObject;
                giveThing.SetActive(true);
                giveThing.GetComponent<SelectedButton>().InitSelectButtonGiveItem
                    ( InventoryManager.Instance.GetItemDetails(ID[j]).ItemName+ "-1",ID[j],Amount[j],6,itemShortInitID);
            }
            else
            {
               //��Ʒ����
               var noItem = LayOutParent.GetChild(0).gameObject;
                noItem.SetActive(true);
                noItem.GetComponent<SelectedButton>().InitSelectButtonGiveItem("����������ɬ��������������", 0, 0, 6, itemShortInitID);
            }
        }
     
    }


    //ս��ǰѯ���Ƿ�ս��
    public void InstantiateIfBattleButton(EnemyDetails enemyDetails)
    {
        //ս��
        var newButton_1 = LayOutParent.GetChild(0).gameObject;
        newButton_1.SetActive(true);

        //��̸
        var newButton_2 = LayOutParent.GetChild(1).gameObject;
        newButton_2.SetActive(true);

        //����
        var newButton_3 = LayOutParent.GetChild(2).gameObject;
        newButton_3.SetActive(true);


        if (enemyDetails.EnemyChooseType == 1)
        {
            //���ɽ�̸
            //newButton_3.GetComponent<Button>().interactable = false;

            //0.�Ƿ����ս��
            //1.׼��ս��
            //2.�޷�����
            //3.����ʧ�ܣ�����ս��
            //4.���ܳɹ����۳�Ѫ��

            switch(enemyDetails.EnemyID)
            {
                case 10001:
                    newButton_1.GetComponent<SelectedButton>().InitSelectButtonIfBattle("��ʼս��", enemyDetails, 4,1,1);
                    newButton_2.GetComponent<SelectedButton>().InitSelectButtonIfBattle("���Խ���", enemyDetails, 4,2,2);
                    newButton_3.GetComponent<SelectedButton>().InitSelectButtonIfBattle("ֱ������", enemyDetails, 4,3,3);
                    break;
                case 10004:
                    newButton_1.GetComponent<SelectedButton>().InitSelectButtonIfBattle("��ʼս��", enemyDetails, 4, 1, 1);
                    newButton_2.GetComponent<SelectedButton>().InitSelectButtonIfBattle("���Խ���", enemyDetails, 4, 2, 2);
                    newButton_3.GetComponent<SelectedButton>().InitSelectButtonIfBattle("ֱ������", enemyDetails, 4, 3, 3);
                    break;

                default:
                    //���в��ɽ�����Ұ����
                    newButton_1.GetComponent<SelectedButton>().InitSelectButtonIfBattle("��ʼս��", enemyDetails, 4, 1, 1);
                    newButton_2.GetComponent<SelectedButton>().InitSelectButtonIfBattle("���Խ���", enemyDetails, 4, 2, 2);
                    newButton_3.GetComponent<SelectedButton>().InitSelectButtonIfBattle("ֱ������", enemyDetails, 4, 3, 3);
                    break;
            }
        }
        else if(enemyDetails.EnemyChooseType == 2)
        {
            //�������� ���ɽ�̸
            newButton_2.GetComponent<Button>().interactable = false;
            newButton_3.GetComponent<Button>().interactable = false;
        }
        else if(enemyDetails.EnemyChooseType == 3)
        {
            //����
        }
        else
        {

        }
    }

    public void InstantiateLuckPadding()
    {
        Debug.Log("do lucky padding");
        var newButton_1 = LayOutParent.GetChild(0).gameObject;
        newButton_1.SetActive(true);
        newButton_1.GetComponent<SelectedButton>().InitSelectButtonInLuckPadding("����",5,0);

        var newButton_2 = LayOutParent.GetChild(1).gameObject;
        newButton_2.SetActive(true);
        newButton_2.GetComponent<SelectedButton>().InitSelectButtonInLuckPadding("������",5,1);

        var newButton_3 = LayOutParent.GetChild(2).gameObject;
        newButton_3.SetActive(true);
        newButton_3.GetComponent<SelectedButton>().InitSelectButtonInLuckPadding("������",5,2);

        var newButton_4 = LayOutParent.GetChild(3).gameObject;
        newButton_4.SetActive(true);
        newButton_4.GetComponent<SelectedButton>().InitSelectButtonInLuckPadding("����ԭ��",5,3);

    }
}
