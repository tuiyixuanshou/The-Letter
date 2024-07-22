using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialPlayerPChange : Singleton<DialPlayerPChange>
{
   //对话中人物血量增减，Cut是增减的具体数值
   public void PlayerCutHealth(int Cut)
    {
        int currentHP = PlayerProperty.Instance.PlayerHealth;
        if(currentHP + Cut <= 0)
        {
            //游戏结束，快进到整体游戏失败
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

    //对话中人物San增减，Cut是增减的具体数值
    public void PlayerCutSan(int Cut)
    {
        int currentSan = PlayerProperty.Instance.PlayerSan;
        if (currentSan + Cut < 20)
        {
            //发疯结局
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

    //对话中人物增加东西，一次只能获得一个，ID是获得的物品的ID
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
