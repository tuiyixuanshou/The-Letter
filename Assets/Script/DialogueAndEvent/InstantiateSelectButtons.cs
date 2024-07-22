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
            default:
                break;
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
        var newButton_1 = LayOutParent.GetChild(0).gameObject;
        newButton_1.SetActive(true);
        newButton_1.GetComponent<SelectedButton>().InitSelectButtonInLuckPadding("��ǰ��",5,0);

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
