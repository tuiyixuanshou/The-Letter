using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialPlayerPChange : Singleton<DialPlayerPChange>
{
   //�Ի�������Ѫ��������Cut�������ľ�����ֵ
   public void PlayerCutHealth(int Cut)
    {
        int currentHP = PlayerProperty.Instance.PlayerHealth;
        if(currentHP + Cut <= 0)
        {
            //��Ϸ�����������������Ϸʧ��
            EndManager.Instance.HealthNone = true;
            EventHandler.CallButton_ToENDGame();
        }
        else if(currentHP + Cut <= 100)
        {
            PlayerProperty.Instance.PlayerHealth = currentHP + Cut;
        }
        else
        {
            PlayerProperty.Instance.PlayerHealth = 100;
        }

    }

    //�Ի�������San������Cut�������ľ�����ֵ
    public void PlayerCutSan(int Cut)
    {
        int currentSan = PlayerProperty.Instance.PlayerSan;
        if (currentSan + Cut < 20)
        {
            //������
            EndManager.Instance.SanNone = true;
            EventHandler.CallButton_ToENDGame();
        }
        else if (currentSan + Cut <= 100)
        {
            PlayerProperty.Instance.PlayerSan = currentSan + Cut;
        }
        else
        {
            PlayerProperty.Instance.PlayerSan = 100;
        }
    }
    public void PlayerCutWealth(int Cut)
    {
        EventHandler.CallPlaySoundEvent(SoundName.MoneyChange);
        int currentW = PlayerProperty.Instance.PlayerWealth;
        PlayerProperty.Instance.PlayerWealth = currentW + Cut;
    }

    //�Ի����������Ӷ�����һ��ֻ�ܻ��һ����ID�ǻ�õ���Ʒ��ID
    public void AddThingInDial(int ID)
    {
        InventoryManager.Instance.AddBagIteminDial(ID);
    }

    public void StealHalfStuff()
    {
        InventoryManager.Instance.LiulanghanSteal();
    }

    public void StealHalfBag()
    {
        InventoryManager.Instance.BagSteal();
    }
}
