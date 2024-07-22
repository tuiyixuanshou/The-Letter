using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    [Header("���ݿ���")]
    public ItemList_SO itemlist_SO;

    public BagList_SO bagItemList_SO;

    public BagList_SO shopItemList_SO;

    public BagList_SO equipItemList_SO;

    public BagList_SO weaponItemList_SO;

    [Header("UI������ʾ���")]
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
        //��ʼ����
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

        //�̵��ʼ����
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
        //�����ʾ���
        GetItemPanel = GameObject.FindGameObjectWithTag("GetItemPanel");
    }


    //ͨ��itemID���itemDetail;
    public ItemDetails GetItemDetails(int ID)
    {
        return itemlist_SO.itemDetailsList.Find(i => i.ItemID == ID);
    }

    //Ŀǰ����ȱ�ٶѵ�����
    //�����������
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

    //�ӽ������ö���
    public void AddBagItemsFromCollection(List<int> itemIDList,string Contains)
    {
        IEnumerator AddBagItemCollection()
        {
            //���canvas group
            var getItemCanvasGroup = GetItemPanel.GetComponent<CanvasGroup>();
            player.InputDisable = true;

            //������
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

    //�жϱ����Ƿ��п�λ
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
    
    //�ҵ�������������Ʒ��λ��
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

    //�ڱ���ָ��λ��������Ʒ
    public void AddItemAtIndex(int ID, int index,int amount,bool isShowPanel)
    {


        if(index == -1)
        {
            //����û����Ӧ��Ʒ

            if (CheckBagCapacity())
            {
                //�п�λ
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
                //û��λ
                //��ʾ����������
                string mtext = "����������";
                MainCanvasUITip.Instance.UITipShowAndDisappear(mtext);
                if (isShowPanel)
                {
                    StartCoroutine(ShowGetItemPanel(ID, false));
                }
            }
            
        }
        else
        {
            //��������Ӧ����
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
                //1��ʾ�����ڲ����Խ���
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
                //2��ʾ�ӱ������̵� 
                BagItemDetails currentBagItem2 = bagItemList_SO.bagItemList[fromIndex];
                BagItemDetails targetItem2 = shopItemList_SO.bagItemList[targetIndex];
                Debug.Log(GetItemIndexInShopShelves(currentBagItem2.BagItemID));
                if (GetItemIndexInShopShelves(currentBagItem2.BagItemID) == -1)
                {
                    //��������û���ƶ��������Ʒ
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
                    //���������ƶ��������Ʒ
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
                //3��ʾ���̵굽����
                BagItemDetails currentBagItem3 = shopItemList_SO.bagItemList[fromIndex];
                BagItemDetails targetItem3 = bagItemList_SO.bagItemList[targetIndex];
                if (GetItemIndexInBag(currentBagItem3.BagItemID) == -1)
                {
                    //��������û���ƶ��������Ʒ
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
                    //���������ƶ��������Ʒ
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
                //4��ʾ���̵굽�̵�
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
                //5��ʾ�ӱ�����װ��
                BagItemDetails currentBagItem5 = bagItemList_SO.bagItemList[fromIndex];
                BagItemDetails targetItem5 = equipItemList_SO.bagItemList[targetIndex];

                if(targetItem5.BagItemID != 0)
                {
                    //maxuse����-1��ʹ�ù���target��Ʒֱ�����٣�������һ����Ʒ
                    //�����Ļ��ǲ��ܻ�
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
                                    BagItemAmount = GetItemDetails(currentBagItem5.BagItemID).MaxEquipUse //װ���е�itemamount����ʣ�����
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
                                    BagItemAmount = GetItemDetails(currentBagItem5.BagItemID).MaxEquipUse //װ���е�itemamount����ʣ�����
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
                        //�ƶ�����Ʒ��װ��
                        if (currentBagItem5.BagItemAmount > 1)
                        {
                            equipItemList_SO.bagItemList[targetIndex] = new BagItemDetails
                            {
                                BagItemID = currentBagItem5.BagItemID,
                                BagItemAmount = GetItemDetails(currentBagItem5.BagItemID).MaxEquipUse //װ���е�itemamount����ʣ�����
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
                                BagItemAmount = GetItemDetails(currentBagItem5.BagItemID).MaxEquipUse //װ���е�itemamount����ʣ�����
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
                //6��ʾ��װ��������
                BagItemDetails currentBagItem6 = equipItemList_SO.bagItemList[fromIndex];
                BagItemDetails targetItem6 = bagItemList_SO.bagItemList[targetIndex];

                //ֻ��maxuseΪ-1����ȫ�µ���Ʒ�ſ���ֱ�ӻ�ȥ
                if (inventoryUI.EquipMentSlots[fromIndex].MaxUse == -1 || 
                    inventoryUI.EquipMentSlots[fromIndex].MaxUse == equipItemList_SO.bagItemList[fromIndex].BagItemAmount)
                {
                    //��������û���ƶ��������Ʒ
                    if (GetItemIndexInBag(currentBagItem6.BagItemID) == -1)
                    {
                        if (targetItem6.BagItemID != 0)
                        {
                            //��ʾ��Ŀ���������
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
                    //���������ƶ��������Ʒ
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
                //7��ʾ�ӱ���������
                BagItemDetails currentBagItem7 = bagItemList_SO.bagItemList[fromIndex];
                BagItemDetails targetItem7 = weaponItemList_SO.bagItemList[targetIndex];


                if (GetItemDetails(currentBagItem7.BagItemID).canWeapon)
                {
                    //�ƶ�����Ʒ������

                    //Ŀ���Ϊ��
                    if (targetItem7.BagItemID == 0)
                    {
                        weaponItemList_SO.bagItemList[targetIndex] = currentBagItem7;
                        bagItemList_SO.bagItemList[fromIndex] = new BagItemDetails { BagItemID = 0, BagItemAmount = 0 };
                    }
                    //Ŀ����Ӳ�Ϊ��
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
                    //�ƶ�����Ʒ����������
                    //��ʾ������Ʒ������Ϊ����ʹ�ã�
                }
                break;

            case 8:
                //8��ʾ������������
                BagItemDetails currentBagItem8 = weaponItemList_SO.bagItemList[fromIndex];
                BagItemDetails targetItem8 = bagItemList_SO.bagItemList[targetIndex];

                if (GetItemIndexInBag(currentBagItem8.BagItemID) == -1)
                {
                    //��������û���ƶ��������Ʒ
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
                    //���������ƶ��������Ʒ
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

    //�ȼ���so�е�����
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

    //�ҵ��̵����������
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

    //չʾ�����Ʒ��Panel
    IEnumerator ShowGetItemPanel(int itemID,bool isAdd)
    {
        //���canvas group
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

    //��������޸�һ�£��ĳɶ�����������ʽ
    //��������޸�һ�£��ĳɶ�����������ʽ
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
