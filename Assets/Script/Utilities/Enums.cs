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
    大批量收购商,散户
}

public enum BattleState 
{
    START, PLAYERTURN, ENEMYTURN, WON, LOST,UNSTART
}

public enum SoundName
{
    CalmMusic1,CalmSlowMusic2,CalmQuickMusic3,CalmQuickMusic4,ExitedSlowMusic1,TerrifyMusic,
    ParkAmib1,RBAmib1,Burning, none,RubButton,PanelOut,PaperOut,
    //3.24 科研基地
    TurnOnOff,TurnOnElect,SearchCollection,liftOpen,liftOperate,liftArrive,
    //home                                   //格子UI        //按键UI    //战斗音效            //室外内走路
    OpenDoor,CloseDoor,MoneyChange,KnockDoor,SlotNor,SlotFig,ButtonClick,Ifight,Efight,FightBt,FootStepOut,FootStepIn,
    //3.26
    FightBGM,ShiXiangGet,InfoGet,
    //5.9         //5.12                                     //5.31 恐怖氛围
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