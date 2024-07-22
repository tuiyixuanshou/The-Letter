using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    [Header("数据库们")]
    public ItemList_SO itemlist_SO;

    public BagList_SO bagItemList_SO;

    public BagList_SO shopItemList_SO;

    public BagList_SO equipItemList_SO;

    public BagList_SO weaponItemList_SO;

    [Header("UI场景提示面板")]
    public GameObject GetItemPanel;


    private bool isGetPanelShowOff = true;

    private Player player =>FindObjectOfType<Player>();
    private InventoryUI inventoryUI => FindObjectOfType<InventoryUI>();

    protected override void Awake()
    {
        base.Awake();
        EmptyOriData();
    }

    private void EmptyOriData()
    {
        //初始物资
        for (int i = 0; i < bagItemList_SO.bagItemList.Count; i++)
        {
            if (i == 0)
            {
                bagItemList_SO.bagItemList[i] = new BagItemDetails
                {
                    BagItemID = 1103,
                    BagItemAmount = 2
                };
            }

            else if (i == 1)
            {
                bagItemList_SO.bagItemList[i] = new BagItemDetails
                {
                    BagItemID = 1105,
                    BagItemAmount = 2
                };
            }
            else if (i == 2)
            {
                bagItemList_SO.bagItemList[i] = new BagItemDetails
                {
                    BagItemID = 3305,
                    BagItemAmount = 1
                };
            }
            else if (i == 3)
            {
                bagItemList_SO.bagItemList[i] = new BagItemDetails
                {
                    BagItemID = 2104,
                    BagItemAmount = 1
                };
            }
            else if (i == 4)
            {
                bagItemList_SO.bagItemList[i] = new BagItemDetails
                {
                    BagItemID = 2201,
                    BagItemAmount = 1
                };
            }
            else
            {
                bagItemList_SO.bagItemList[i] = new BagItemDetails
                {
                    BagItemID = 0,
                    BagItemAmount = 0
                };
            }
        }

        //商店初始物资
        for (int i = 0; i < shopItemList_SO.bagItemList.Count; i++)
        {
            if (i == 0)
            {
                shopItemList_SO.bagItemList[i] = new BagItemDetails
                {
                    BagItemID = 1101,
                    BagItemAmount = 2
                };
            }
            else if (i == 1)
            {
                shopItemList_SO.bagItemList[i] = new BagItemDetails
                {
                    BagItemID = 1201,
                    BagItemAmount = 1
                };
            }
            else if (i == 2)
            {
                shopItemList_SO.bagItemList[i] = new BagItemDetails
                {
                    BagItemID = 2302,
                    BagItemAmount = 1
                };
            }
            else if (i == 3)
            {
                shopItemList_SO.bagItemList[i] = new BagItemDetails
                {
                    BagItemID = 2104,
                    BagItemAmount = 1
                };
            }
            else if (i == 4)
            {
                shopItemList_SO.bagItemList[i] = new BagItemDetails
                {
                    BagItemID = 2201,
                    BagItemAmount = 1
                };
            }
            else
            {
                shopItemList_SO.bagItemList[i] = new BagItemDetails
                {
                    BagItemID = 0,
                    BagItemAmount = 0
                };
            }
        }

        for (int i = 0; i < equipItemList_SO.bagItemList.Count; i++)
        {
            equipItemList_SO.bagItemList[i] = new BagItemDetails
            {
                BagItemID = 0,
                BagItemAmount = 0
            };
        }

        for (int i = 0; i < weaponItemList_SO.bagItemList.Count; i++)
        {
            weaponItemList_SO.bagItemList[i] = new BagItemDetails
            {
                BagItemID = 0,
                BagItemAmount = 0
            };
        }
    }

    private void OnEnable()
    {
        EventHandler.NewGameEmptyData += EmptyOriData;
    }

    private void OnDisable()
    {
        EventHandler.NewGameEmptyData -= EmptyOriData;
    }

    private void Start()
    {
        //获得提示面板
        GetItemPanel = GameObject.FindGameObjectWithTag("GetItemPanel");
    }


    //通过itemID获得itemDetail;
    public ItemDetails GetItemDetails(int ID)
    {
        return itemlist_SO.itemDetailsList.Find(i => i.ItemID == ID);
    }

    //目前背包缺少堆叠上限
    //背包添加内容
    public void AddBagItem(Item item, bool toDestory,int amount)
    {

        int index = GetItemIndexInBag(item.itemID);

        AddItemAtIndex(item.itemID, index, amount,true);
        if (toDestory)
        {
            Destroy(item.gameObject);
        }
       
        EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
    }

    public void AddBagIteminDial(int ID)
    {
        int index = GetItemIndexInBag(ID);
        AddItemAtIndex(ID, index, 1, false);
        EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
    }

    //从交互点获得东西
    public void AddBagItemsFromCollection(List<int> itemIDList,string Contains)
    {
        IEnumerator AddBagItemCollection()
        {
            //获得canvas group
            var getItemCanvasGroup = GetItemPanel.GetComponent<CanvasGroup>();
            player.InputDisable = true;

            //搜索中
            GetItemPanel.transform.GetChild(0).GetComponent<Image>().enabled = false;
            GetItemPanel.transform.GetChild(1).GetComponent<Text>().enabled = false;
            GetItemPanel.transform.GetChild(2).GetComponent<Text>().enabled = false;
            GetItemPanel.transform.GetChild(3).GetComponent<Text>().enabled = true;
            GetItemPanel.transform.GetChild(3).GetComponent<Text>().text = Contains;
           GetItemPanel.transform.GetChild(4).GetComponent<Text>().enabled = false;
            yield return TransitionFadeCanvas(getItemCanvasGroup, 1f, 0.1f);
            EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.SearchCollection));
            yield return new WaitForSeconds(AudioManager.Instance.GetSoundDetailsfromName(SoundName.SearchCollection).audioClip.length);
            yield return new WaitForSeconds(0.06f);
            EventHandler.CallInitSoundEffect(AudioManager.Instance.GetSoundDetailsfromName(SoundName.SearchCollection));
            yield return new WaitForSeconds(AudioManager.Instance.GetSoundDetailsfromName(SoundName.SearchCollection).audioClip.length);
            yield return TransitionFadeCanvas(getItemCanvasGroup, 0f, 0.4f);

            for (int i = 0; i < itemIDList.Count; i++)
            {
                yield return new WaitUntil(() => isGetPanelShowOff);
                int index = GetItemIndexInBag(itemIDList[i]);
                AddItemAtIndex(itemIDList[i], index, 1,true);
                EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
                yield return new WaitForSeconds(0.2f);
            }
            player.InputDisable = false;
        }


        StartCoroutine(AddBagItemCollection());
        

    }

    //判断背包是否有空位
    public bool CheckBagCapacity()
    {
        for(int i = 0; i < bagItemList_SO.bagItemList.Count; i++)
        {
            if(bagItemList_SO.bagItemList[i].BagItemID == 0)
            {
                return true;
            }
        }
        return false;
    }
    
    //找到背包中已有物品的位置
    public int GetItemIndexInBag(int ID)
    {
        for(int i = 0; i < bagItemList_SO.bagItemList.Count; i++)
        {
            if(bagItemList_SO.bagItemList[i].BagItemID == ID)
            {
                return i;
            }
        }
        return -1;
    }

    //在背包指定位置增加物品
    public void AddItemAtIndex(int ID, int index,int amount,bool isShowPanel)
    {


        if(index == -1)
        {
            //背包没有相应物品

            if (CheckBagCapacity())
            {
                //有空位
                var bagitem = new BagItemDetails { BagItemID = ID, BagItemAmount = amount };
                for (int i = 0; i < bagItemList_SO.bagItemList.Count; i++)
                {
                    if (bagItemList_SO.bagItemList[i].BagItemID == 0)
                    {
                        bagItemList_SO.bagItemList[i] = bagitem;
                        break;
                    }
                }
                if (isShowPanel)
                {
                    StartCoroutine(ShowGetItemPanel(ID, true));
                }
                
            }
            else
            {
                //没空位
                //提示：背包已满
                string mtext = "背包已满！";
                MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
                if (isShowPanel)
                {
                    StartCoroutine(ShowGetItemPanel(ID, false));
                }
            }
            
        }
        else
        {
            //背包有相应物体
            int currentAmount = bagItemList_SO.bagItemList[index].BagItemAmount + amount;
            var bagitem = new BagItemDetails { BagItemID = ID, BagItemAmount = currentAmount };
            bagItemList_SO.bagItemList[index] = bagitem;
            if (isShowPanel)
            {
                StartCoroutine(ShowGetItemPanel(ID, true));
            }
        }
        
    }


    public void SwapItem(int SwapType,int fromIndex,int targetIndex)
    {
        switch (SwapType)
        {
            case 1:
                //1表示背包内部各自交换
                BagItemDetails currentBagItem = bagItemList_SO.bagItemList[fromIndex];
                BagItemDetails targetItem = bagItemList_SO.bagItemList[targetIndex];

                if (targetItem.BagItemID != 0)
                {
                    bagItemList_SO.bagItemList[targetIndex] = currentBagItem;
                    bagItemList_SO.bagItemList[fromIndex] = targetItem;
                }
                else
                {
                    bagItemList_SO.bagItemList[targetIndex] = currentBagItem;
                    bagItemList_SO.bagItemList[fromIndex] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };

                }
                EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
                break;
            case 2:
                //2表示从背包到商店 
                BagItemDetails currentBagItem2 = bagItemList_SO.bagItemList[fromIndex];
                BagItemDetails targetItem2 = shopItemList_SO.bagItemList[targetIndex];
                Debug.Log(GetItemIndexInShopShelves(currentBagItem2.BagItemID));
                if (GetItemIndexInShopShelves(currentBagItem2.BagItemID) == -1)
                {
                    //货架里面没有移动进入的物品
                    if (targetItem2.BagItemID != 0)
                    {
                        shopItemList_SO.bagItemList[targetIndex] = currentBagItem2;
                        bagItemList_SO.bagItemList[fromIndex] = targetItem2;
                    }
                    else
                    {
                        shopItemList_SO.bagItemList[targetIndex] = currentBagItem2;
                        bagItemList_SO.bagItemList[fromIndex] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                    }

                }
                else
                {
                    //货架里有移动进入的物品
                    var currentshopthing = shopItemList_SO.bagItemList[GetItemIndexInShopShelves(currentBagItem2.BagItemID)];
                    shopItemList_SO.bagItemList[GetItemIndexInShopShelves(currentBagItem2.BagItemID)] = new BagItemDetails
                    {
                        BagItemID = currentshopthing.BagItemID,
                        BagItemAmount = currentshopthing.BagItemAmount + currentBagItem2.BagItemAmount
                    };
                    bagItemList_SO.bagItemList[fromIndex] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                }

                EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
                EventHandler.CallUpdateBagUI(InventoryType.ShopInventory, shopItemList_SO.bagItemList);
                break;

            case 3:
                //3表示从商店到背包
                BagItemDetails currentBagItem3 = shopItemList_SO.bagItemList[fromIndex];
                BagItemDetails targetItem3 = bagItemList_SO.bagItemList[targetIndex];
                if (GetItemIndexInBag(currentBagItem3.BagItemID) == -1)
                {
                    //背包里面没有移动进入的物品
                    if (targetItem3.BagItemID != 0)
                    {
                        bagItemList_SO.bagItemList[targetIndex] = currentBagItem3;
                        shopItemList_SO.bagItemList[fromIndex] = targetItem3;
                    }
                    else
                    {
                        bagItemList_SO.bagItemList[targetIndex] = currentBagItem3;
                        shopItemList_SO.bagItemList[fromIndex] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                    }
                }
                else
                {
                    //背包里有移动进入的物品
                    var currentshopthing = bagItemList_SO.bagItemList[(GetItemIndexInBag(currentBagItem3.BagItemID))];
                    bagItemList_SO.bagItemList[(GetItemIndexInBag(currentBagItem3.BagItemID))] = new BagItemDetails
                    {
                        BagItemID = currentshopthing.BagItemID,
                        BagItemAmount = currentshopthing.BagItemAmount + currentBagItem3.BagItemAmount
                    };
                    shopItemList_SO.bagItemList[fromIndex] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                }
                
                EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
                EventHandler.CallUpdateBagUI(InventoryType.ShopInventory, shopItemList_SO.bagItemList);
                break;
            case 4:
                //4表示从商店到商店
                BagItemDetails currentBagItem4 = shopItemList_SO.bagItemList[fromIndex];
                BagItemDetails targetItem4 = shopItemList_SO.bagItemList[targetIndex];
                if (targetItem4.BagItemID != 0)
                {
                    shopItemList_SO.bagItemList[targetIndex] = currentBagItem4;
                    shopItemList_SO.bagItemList[fromIndex] = targetItem4;
                }
                else
                {
                    shopItemList_SO.bagItemList[targetIndex] = currentBagItem4;
                    shopItemList_SO.bagItemList[fromIndex] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                }
                EventHandler.CallUpdateBagUI(InventoryType.ShopInventory, shopItemList_SO.bagItemList);
                break;

            case 5:
                //5表示从背包到装备
                BagItemDetails currentBagItem5 = bagItemList_SO.bagItemList[fromIndex];
                BagItemDetails targetItem5 = equipItemList_SO.bagItemList[targetIndex];

                if(targetItem5.BagItemID != 0)
                {
                    //maxuse不是-1且使用过的target物品直接销毁，换成另一个物品
                    //其他的还是不能换
                    if (GetItemDetails(currentBagItem5.BagItemID).canEquip)
                    {
                        if(inventoryUI.EquipMentSlots[targetIndex].MaxUse != -1 
                            && inventoryUI.EquipMentSlots[targetIndex].MaxUse != equipItemList_SO.bagItemList[targetIndex].BagItemAmount)
                        {
                            if (currentBagItem5.BagItemAmount > 1)
                            {
                                equipItemList_SO.bagItemList[targetIndex] = new BagItemDetails
                                {
                                    BagItemID = currentBagItem5.BagItemID,
                                    BagItemAmount = GetItemDetails(currentBagItem5.BagItemID).MaxEquipUse //装备中的itemamount就是剩余次数
                                };

                                bagItemList_SO.bagItemList[fromIndex] = new BagItemDetails
                                {
                                    BagItemID = currentBagItem5.BagItemID,
                                    BagItemAmount = currentBagItem5.BagItemAmount - 1
                                };
                            }
                            else
                            {
                                equipItemList_SO.bagItemList[targetIndex] = new BagItemDetails
                                {
                                    BagItemID = currentBagItem5.BagItemID,
                                    BagItemAmount = GetItemDetails(currentBagItem5.BagItemID).MaxEquipUse //装备中的itemamount就是剩余次数
                                };
                                bagItemList_SO.bagItemList[fromIndex] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                            }
                            EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
                            EventHandler.CallUpdateBagUI(InventoryType.EquipInventory, equipItemList_SO.bagItemList);
                        }
                    }
                }
                else
                {
                    if (GetItemDetails(currentBagItem5.BagItemID).canEquip)
                    {
                        //移动的物品可装备
                        if (currentBagItem5.BagItemAmount > 1)
                        {
                            equipItemList_SO.bagItemList[targetIndex] = new BagItemDetails
                            {
                                BagItemID = currentBagItem5.BagItemID,
                                BagItemAmount = GetItemDetails(currentBagItem5.BagItemID).MaxEquipUse //装备中的itemamount就是剩余次数
                            };

                            bagItemList_SO.bagItemList[fromIndex] = new BagItemDetails
                            {
                                BagItemID = currentBagItem5.BagItemID,
                                BagItemAmount = currentBagItem5.BagItemAmount - 1
                            };
                        }
                        else
                        {
                            equipItemList_SO.bagItemList[targetIndex] = new BagItemDetails
                            {
                                BagItemID = currentBagItem5.BagItemID,
                                BagItemAmount = GetItemDetails(currentBagItem5.BagItemID).MaxEquipUse //装备中的itemamount就是剩余次数
                            };
                            bagItemList_SO.bagItemList[fromIndex] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                        }
                        EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
                        EventHandler.CallUpdateBagUI(InventoryType.EquipInventory, equipItemList_SO.bagItemList);
                    }
                }
                //EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
                //EventHandler.CallUpdateBagUI(InventoryType.EquipInventory, equipItemList_SO.bagItemList);
                
                break;

            case 6:
                //6表示从装备到背包
                BagItemDetails currentBagItem6 = equipItemList_SO.bagItemList[fromIndex];
                BagItemDetails targetItem6 = bagItemList_SO.bagItemList[targetIndex];

                //只有maxuse为-1或者全新的物品才可以直接回去
                if (inventoryUI.EquipMentSlots[fromIndex].MaxUse == -1 || 
                    inventoryUI.EquipMentSlots[fromIndex].MaxUse == equipItemList_SO.bagItemList[fromIndex].BagItemAmount)
                {
                    //背包里面没有移动进入的物品
                    if (GetItemIndexInBag(currentBagItem6.BagItemID) == -1)
                    {
                        if (targetItem6.BagItemID != 0)
                        {
                            //提示：目标格子已满
                        }
                        else
                        {
                            bagItemList_SO.bagItemList[targetIndex] = new BagItemDetails
                            {
                                BagItemID = currentBagItem6.BagItemID,
                                BagItemAmount = 1
                            };
                            equipItemList_SO.bagItemList[fromIndex] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                            EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
                            EventHandler.CallUpdateBagUI(InventoryType.EquipInventory, equipItemList_SO.bagItemList);
                        }
                    }
                    //背包里有移动进入的物品
                    else
                    {
                        var currentequipthing = bagItemList_SO.bagItemList[(GetItemIndexInBag(currentBagItem6.BagItemID))];
                        bagItemList_SO.bagItemList[(GetItemIndexInBag(currentBagItem6.BagItemID))] = new BagItemDetails
                        {
                            BagItemID = currentequipthing.BagItemID,
                            BagItemAmount = currentequipthing.BagItemAmount + 1
                        };
                        equipItemList_SO.bagItemList[fromIndex] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                        EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
                        EventHandler.CallUpdateBagUI(InventoryType.EquipInventory, equipItemList_SO.bagItemList);
                    }
                }
                break;
            case 7:
                //7表示从背包到武器
                BagItemDetails currentBagItem7 = bagItemList_SO.bagItemList[fromIndex];
                BagItemDetails targetItem7 = weaponItemList_SO.bagItemList[targetIndex];


                if (GetItemDetails(currentBagItem7.BagItemID).canWeapon)
                {
                    //移动的物品可武器

                    //目标格为空
                    if (targetItem7.BagItemID == 0)
                    {
                        weaponItemList_SO.bagItemList[targetIndex] = currentBagItem7;
                        bagItemList_SO.bagItemList[fromIndex] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                    }
                    //目标格子不为空
                    else
                    {
                        if (targetItem7.BagItemID == currentBagItem7.BagItemID)
                        {
                            weaponItemList_SO.bagItemList[targetIndex] = new BagItemDetails
                            {
                                BagItemID = currentBagItem7.BagItemID,
                                BagItemAmount = currentBagItem7.BagItemAmount + targetItem7.BagItemAmount
                            };
                            bagItemList_SO.bagItemList[fromIndex] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                        }
                        else
                        {
                            bagItemList_SO.bagItemList[fromIndex] = targetItem7;
                            weaponItemList_SO.bagItemList[targetIndex] = currentBagItem7;
                        }
                    }
                    EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
                    EventHandler.CallUpdateBagUI(InventoryType.WeaponInventory, weaponItemList_SO.bagItemList);
                }
                else
                {
                    //移动的物品不可以武器
                    //提示：次物品不可作为武器使用！
                }
                break;

            case 8:
                //8表示从武器到背包
                BagItemDetails currentBagItem8 = weaponItemList_SO.bagItemList[fromIndex];
                BagItemDetails targetItem8 = bagItemList_SO.bagItemList[targetIndex];

                if (GetItemIndexInBag(currentBagItem8.BagItemID) == -1)
                {
                    //背包里面没有移动进入的物品
                    if (targetItem8.BagItemID != 0)
                    {
                        bagItemList_SO.bagItemList[targetIndex] = currentBagItem8;
                        weaponItemList_SO.bagItemList[fromIndex] = targetItem8;
                    }
                    else
                    {
                        bagItemList_SO.bagItemList[targetIndex] = currentBagItem8;
                        weaponItemList_SO.bagItemList[fromIndex] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                    }
                }
                else
                {
                    //背包里有移动进入的物品
                    var currentequipthing = bagItemList_SO.bagItemList[(GetItemIndexInBag(currentBagItem8.BagItemID))];
                    bagItemList_SO.bagItemList[(GetItemIndexInBag(currentBagItem8.BagItemID))] = new BagItemDetails
                    {
                        BagItemID = currentequipthing.BagItemID,
                        BagItemAmount = currentequipthing.BagItemAmount + currentBagItem8.BagItemAmount
                    };
                    weaponItemList_SO.bagItemList[fromIndex] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                }
                EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
                EventHandler.CallUpdateBagUI(InventoryType.WeaponInventory, weaponItemList_SO.bagItemList);
                break;
            default:
                break;
        }
        
    }

    //先减少so中的武器
    public void ReduceWeaponInSO(int index)
    {
        if(index != -1)
        {
            int a = weaponItemList_SO.bagItemList[index].BagItemAmount - 1;
            if (a >= 1)
            {
                weaponItemList_SO.bagItemList[index] = new BagItemDetails { BagItemID = weaponItemList_SO.bagItemList[index].BagItemID, BagItemAmount = a };
            }
            else
            {
                weaponItemList_SO.bagItemList[index] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
            }
        }
        
    }

    private int GetItemIndexInShopShelves(int ID)
    {
        for (int i = 0; i < shopItemList_SO.bagItemList.Count; i++)
        {
            if (shopItemList_SO.bagItemList[i].BagItemID == ID)
            {
                return i;
            }
        }
        return -1;
    }

    //找到商店物体的数量
    public int GetItemAmountInBag(int ID)
    {
        for (int i = 0; i < shopItemList_SO.bagItemList.Count; i++)
        {
            if (shopItemList_SO.bagItemList[i].BagItemID == ID)
            {
                return shopItemList_SO.bagItemList[i].BagItemAmount;
            }
        }
        return 0;
    }


    public void GetItemIndexInBagAndSetRestAmount(int ID,int RestAmount)
    {
   
        for (int i = 0; i < shopItemList_SO.bagItemList.Count; i++)
        {
            if (shopItemList_SO.bagItemList[i].BagItemID == ID)
            {

                if (RestAmount != 0)
                {
                    var bagitem = new BagItemDetails { BagItemID = ID, BagItemAmount = RestAmount };
                    shopItemList_SO.bagItemList[i] = bagitem;
                }
                else
                {
                    shopItemList_SO.bagItemList[i] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                }
                
            }
            else
            {
                Debug.Log("do not sell");
            }
        }

        EventHandler.CallUpdateBagUI(InventoryType.ShopInventory, shopItemList_SO.bagItemList);
        EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
        // return -1;
    }

    public void ReduceItemInBag(int ID, int RestAmount)
    {
        for (int i = 0; i < bagItemList_SO.bagItemList.Count; i++)
        {
            if (bagItemList_SO.bagItemList[i].BagItemID == ID)
            {
                if (RestAmount != 0)
                {
                    var bagitem = new BagItemDetails { BagItemID = ID, BagItemAmount = RestAmount };
                    bagItemList_SO.bagItemList[i] = bagitem;
                }
                else
                {
                    bagItemList_SO.bagItemList[i] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                }

            }
            else
            {
                Debug.Log("dont have this thing");
            }
        }
        EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
    }

    //展示获得物品的Panel
    IEnumerator ShowGetItemPanel(int itemID,bool isAdd)
    {
        //获得canvas group
        var getItemCanvasGroup = GetItemPanel.GetComponent<CanvasGroup>();
        isGetPanelShowOff = false;
        if (isAdd)
        {
            GetItemPanel.transform.GetChild(0).GetComponent<Image>().enabled = true;
            GetItemPanel.transform.GetChild(1).GetComponent<Text>().enabled = true;
            GetItemPanel.transform.GetChild(2).GetComponent<Text>().enabled = true;
            GetItemPanel.transform.GetChild(3).GetComponent<Text>().enabled = false;
            GetItemPanel.transform.GetChild(4).GetComponent<Text>().enabled = false;
            GetItemPanel.transform.GetChild(0).GetComponent<Image>().sprite = GetItemDetails(itemID).ItemIcon;
            GetItemPanel.transform.GetChild(1).GetComponent<Text>().text = GetItemDetails(itemID).ItemName;
            yield return TransitionFadeCanvas(getItemCanvasGroup, 1f, 0.1f);
            yield return new WaitForSeconds(0.3f);
            yield return TransitionFadeCanvas(getItemCanvasGroup, 0f, 0.1f);
            isGetPanelShowOff = true;
        }
        else
        {
            GetItemPanel.transform.GetChild(0).GetComponent<Image>().enabled = false;
            GetItemPanel.transform.GetChild(1).GetComponent<Text>().enabled = false;
            GetItemPanel.transform.GetChild(2).GetComponent<Text>().enabled = false;
            GetItemPanel.transform.GetChild(3).GetComponent<Text>().enabled = false;
            GetItemPanel.transform.GetChild(4).GetComponent<Text>().enabled = true;
            yield return TransitionFadeCanvas(getItemCanvasGroup, 1f, 0.1f);
            yield return new WaitForSeconds(0.3f);
            yield return TransitionFadeCanvas(getItemCanvasGroup, 0f, 0.1f);
            isGetPanelShowOff = true;
        }
       
    }

    //这里可以修改一下，改成动画穿过的形式
    //这里可以修改一下，改成动画穿过的形式
    IEnumerator TransitionFadeCanvas(CanvasGroup getItemCanvasGroup, float targetAlpha, float transitionFadeDuration)
    {
        float speed = Mathf.Abs(getItemCanvasGroup.alpha - targetAlpha) / transitionFadeDuration;

        while (!Mathf.Approximately(getItemCanvasGroup.alpha, targetAlpha))
        {
            getItemCanvasGroup.alpha = Mathf.MoveTowards(getItemCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            //yield return new WaitForSeconds(0.01f);
            yield return null;
        }
    }


    public void LiulanghanSteal()
    {
        for(int i = 0; i < shopItemList_SO.bagItemList.Count; i++)
        {
            var newItem = new BagItemDetails { BagItemID = shopItemList_SO.bagItemList[i].BagItemID, BagItemAmount = shopItemList_SO.bagItemList[i].BagItemAmount/2 };
            shopItemList_SO.bagItemList[i] = newItem;
        }
        EventHandler.CallUpdateBagUI(InventoryType.ShopInventory, shopItemList_SO.bagItemList);
    }

    public void BagSteal()
    {
        for (int i = 0; i < bagItemList_SO.bagItemList.Count; i++)
        {
            var newItem = new BagItemDetails { BagItemID = bagItemList_SO.bagItemList[i].BagItemID, BagItemAmount = bagItemList_SO.bagItemList[i].BagItemAmount / 2 };
            bagItemList_SO.bagItemList[i] = newItem;
        }
        EventHandler.CallUpdateBagUI(InventoryType.BagInventory, bagItemList_SO.bagItemList);
    }
}
