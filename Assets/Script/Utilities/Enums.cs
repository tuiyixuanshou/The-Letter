public enum SlotType
{
    Bag,box,Shop,Equipment,Weapon
}

public enum InventoryType
{
    BagInventory,BoxInventory,ShopInventory,
    EquipInventory,WeaponInventory
}

public enum TheCustomerType
{
    �������չ���,ɢ��
}

public enum BattleState 
{
    START, PLAYERTURN, ENEMYTURN, WON, LOST,UNSTART
}

public enum SoundName
{
    CalmMusic1,CalmSlowMusic2,CalmQuickMusic3,CalmQuickMusic4,ExitedSlowMusic1,TerrifyMusic,
    ParkAmib1,RBAmib1,Burning, none,RubButton,PanelOut,PaperOut,
    //3.24 ���л���
    TurnOnOff,TurnOnElect,SearchCollection,liftOpen,liftOperate,liftArrive,
    //home                                   //����UI        //����UI    //ս����Ч            //��������·
    OpenDoor,CloseDoor,MoneyChange,KnockDoor,SlotNor,SlotFig,ButtonClick,Ifight,Efight,FightBt,FootStepOut,FootStepIn,
    //3.26
    FightBGM,ShiXiangGet,InfoGet,
    //5.9         //5.12                                     //5.31 �ֲ���Χ
    KaichangMusic,OpenChurchLian,ChurchBGM,Radio,WarYAmnient,KongBu,ZhongWuLuoDi
}

public enum SceneName
{
    Home,Map,IntroduceScene,SaftyArea
}

public enum SceneMod
{
    Safe,War,Epidemic,Church
}