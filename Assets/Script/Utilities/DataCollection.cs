using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

//可捡拾物品数据
[System.Serializable]
public class ItemDetails
{
    public int ItemID;
    public string ItemName;
    public string ItemDescribe;
    public Sprite ItemSprite;
    public Sprite ItemIcon;
    public float itemPrice;

    //[Range(0, 1)]
    //public float sellPercentage;

    public bool canEquip;
    public bool canWeapon;

    public int ItemEquipAttack;
    public int ItemEquipDefense;

    public int ItemWeaponAttack;
    public int ItemWeaponDefense;


    public bool canHold;
}

//背包数据
[System.Serializable]
public struct BagItemDetails
{
    public int BagItemID;
    public int BagItemAmount;
}

[System.Serializable]
public struct ShopItemDetails
{
    public int ShopItemID;
    public int ShopItemAmount;
}


//可以序列化存储的物体坐标
[System.Serializable]
public class SerializableVector3
{
    public float x, y, z;

    public SerializableVector3(Vector3 pos)
    {
        this.x = pos.x;
        this.y = pos.y;
        this.z = pos.z;
    }
    
    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}

//场景中需要保存的物体数据
[System.Serializable]
public class SceneItem
{
    public int itemID;
    public SerializableVector3 position;
}

//对话的数据
[System.Serializable]
public class DialogueData
{
    public Sprite PeopleImage;
    public bool isLeft;
    public string PeopleName;

    [TextArea]
    public string DialogueText;

    public bool isNeedPause;
    //这句话是否播完
    [HideInInspector]public bool isOver;

    public UnityEvent AfterSentenceFinish;

    //public UnityEvent AfterDialFinish;

    public bool isFight;
}

[System.Serializable]
public class DialogueListElement
{
    public int CustomerID;

    public bool isThisDialDone;
    //public int DialID;

    public List<DialogueData> DialListElem;
}

[System.Serializable]
//家中事件存储列表
public class HomeEvent
{
    public int EventID;
    public string EventName;
    [TextArea]
    public string EventDiscribe;

    public bool isEventOver;

    public Sprite UISymbol;

    public List<DialogueData> dialogueDatas;

    public Vector2 rectPos;
}

//NPC数据库
[System.Serializable]
public class NPCDetails
{
    public string NPCName;

    public int NPCID;

    public Sprite NPCFace;

    public List<DialogueData> dialogueDatas;

    public List<DialogueData> dialogueDatas1;

    public SerializableVector3 NPCposition;

}

//每日事件面板事件戈单项内容
[System.Serializable]
public class EventItemOnPanel
{
    public string EventNPCName;

    public string EventContain;
}

//客人（TradeUI）
[System.Serializable]
public class CustomerDetails
{
    public int CustomerID;

    public string CustomerName;

    public TheCustomerType CustomerType;

    public Sprite CustomerFace;

    [TextArea]
    public string CustomerText;

    public Dictionary<int, int> NeedItemDictionary;

    public List<DialogueListElement> dialogueElemList;

    public int DialogueIndex;

    public bool isSellDone;

}

[System.Serializable]
public class EnemyDetails
{
    public int EnemyID;

    public string EnemyName;

    public Sprite EnemyFace;

    public Sprite EnemyFaceInBattle;

    public int AttackMax;

    public int AttackMin;

    public float defenseMax;

    public float defenseMin;

    public int MaxHP;

    //1战斗逃跑无交谈 2战斗无逃跑无交谈 3战斗逃跑交谈
    public int EnemyChooseType;

    public List<DialogueListElement> dialogueElemList;

    public int DialIndex;

    public List<BagItemDetails> GetBagItems;

    //public UnityEvent AfterDialFinish;

}

[System.Serializable]
public class SoundDetails
{
    public SoundName soundName;
    public AudioClip audioClip;
    [Range(0.1f, 1.5f)]
    public float soundPitchMin;
    [Range(0.1f, 1.5f)]
    public float soundPitchMax;
    [Range(0.1f, 1.0f)]
    public float soundVloume;

}

[System.Serializable]
public class SceneSoundItem
{
    public string sceneName;

    public SoundName music;

    public SoundName ambient;
}


[System.Serializable] 
public class EquipMentDetails
{
    public int EquipItemID;
    public int AttackNum;
    public int DefenseNum;
    public string EquipName;
}

[System.Serializable]
public class WeaponDetails
{
    public int WeaponItemID;
    public int amount;
    public int AttackNum;
    public int DefenseNum;
    public string WeaponName;
}

