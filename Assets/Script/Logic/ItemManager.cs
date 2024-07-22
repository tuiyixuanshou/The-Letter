using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : Singleton<ItemManager>
{
    public Item ItemPrefab;

    public Transform ItemParent;

    public Dictionary<string, List<SceneItem>> sceneItemDict = new Dictionary<string, List<SceneItem>>();

    public Dictionary<string, List<SceneItemCollection>> sceneItemCollectionDict = new();

    [Header("资源个数")]
    public int itemNum;


    private void OnEnable()
    {
        EventHandler.BeforeSceneUnLoad += OnBeforeSceneUnLoad;
        EventHandler.AfterSceneLoad += OnAfterSceneLoad;
    }

    

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnLoad -= OnBeforeSceneUnLoad;
        EventHandler.AfterSceneLoad -= OnAfterSceneLoad;
    }

    private void OnBeforeSceneUnLoad()
    {
        //获得外景亮晶晶
        GetAllSceneItem();
        //获得交互点信息
        GetAllsceneCollection();
    }

    private void OnAfterSceneLoad()
    {
        if(SceneManager.GetActiveScene().name == "SafetyArea")
        {
            ItemParent = GameObject.FindGameObjectWithTag("ItemParent").transform;

            //刷新函数重建
            UpdateItemOnSaftyPark();

            RecreateAllItem();
        }
        else if(SceneManager.GetActiveScene().name == "ResearchBase")
        {
            ItemParent = GameObject.FindGameObjectWithTag("ItemParent").transform;

            //刷新函数重建
            UpdateItemOnRBbalcony();

            RecreateAllItem();

            //刷新交互点
            UpdateCollectionInRB();

            //itemcollection的更新在每个itemcollection的start中
        }
        else if (SceneManager.GetActiveScene().name == "Church")
        {
            ItemParent = GameObject.FindGameObjectWithTag("ItemParent").transform;
            //刷新交互点
            UpdateCollectionInChurch();
        }
        else if (SceneManager.GetActiveScene().name == "WarYield")
        {
            ItemParent = GameObject.FindGameObjectWithTag("ItemParent").transform;

            //刷新函数重建
            UpdateItemOnWarYield();

            RecreateAllItem();
        }




    }

    //获得场景中的所有物体
    private void GetAllSceneItem()
    {
        //新建临时列表
        List<SceneItem> currentSceneItems = new List<SceneItem>();

        //获得场景中的所有东西并加入列表中
        foreach(var item in FindObjectsOfType<Item>())
        {
            SceneItem sceneItem = new SceneItem
            {
                itemID = item.itemID,
                position = new SerializableVector3(item.transform.position)
            };

            currentSceneItems.Add(sceneItem);
        }

        //判断场景物品字典中是否有该场景
        if (sceneItemDict.ContainsKey(SceneManager.GetActiveScene().name))
        {
            //有该场景，更新其列表
            sceneItemDict[SceneManager.GetActiveScene().name] = currentSceneItems;
        }
        else
        {
            //没有该场景，添加场景
            sceneItemDict.Add(SceneManager.GetActiveScene().name, currentSceneItems);
        }
    }

    private void RecreateAllItem()
    {
        List<SceneItem> currentSceneItems = new();

        if(sceneItemDict.TryGetValue(SceneManager.GetActiveScene().name,out currentSceneItems))
        {
            if(currentSceneItems != null)
            {
                //清场
                foreach(var item in FindObjectsOfType<Item>())
                {
                    Destroy(item.gameObject);
                }

                //重新加载
                itemNum = currentSceneItems.Count;
                foreach (var item in currentSceneItems)
                {
                    Item newItem = Instantiate(ItemPrefab, item.position.ToVector3(), Quaternion.identity, ItemParent);
                    newItem.Init(item.itemID);
                }
            }
        }
    }


    //城市广场刷新
    private void UpdateItemOnSaftyPark()
    {
        if(DayManager.Instance.DayCount%2 == 1)
        {
            List<SceneItem> newcurrentSceneItems = new();

            //第一组刷新
            int i = Random.Range(3, 6);
            for(int j = 0; j < i; j++)
            {
                int q = Random.Range(1, 5);
                switch (q)
                {
                    case 1:
                        SceneItem newItem = new SceneItem
                        {
                            itemID = 1101,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };

                        newcurrentSceneItems.Add(newItem);

                        break;
                    case 2:
                        SceneItem newItem1 = new SceneItem
                        {
                            itemID = 1202,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };

                        newcurrentSceneItems.Add(newItem1);
                        break;
                    case 3:
                        SceneItem newItem2 = new SceneItem
                        {
                            itemID = 1203,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };

                        newcurrentSceneItems.Add(newItem2);
                        break;
                    case 4:
                        SceneItem newItem3 = new SceneItem
                        {
                            itemID = 1301,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem3);
                        break;
                    default:

                        break;
                }
            }

            //第二组刷新
            int i1 = Random.Range(7, 11);
            for (int j = 0; j < i1; j++)
            {
                int q = Random.Range(1, 6);
                switch (q)
                {
                    case 1:
                        SceneItem newItem = new()
                        {
                            itemID = 1102,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem);
                        break;
                    case 2:
                        SceneItem newItem1 = new()
                        {
                            itemID = 1103,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem1);
                        break;
                    case 3:
                        SceneItem newItem2 = new()
                        {
                            itemID = 1104,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem2);
                        break;
                    case 4:
                        SceneItem newItem3 = new()
                        {
                            itemID = 1105,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem3);
                        break;
                    case 5:
                        SceneItem newItem4 = new()
                        {
                            itemID = 1201,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem4);
                        break;
                    default:
                        break;
                }
            }

            //第三组刷新
            int i2 = Random.Range(0, 2);
            for(int j = 0; j < i2; j++)
            {
                int q = Random.Range(1, 8);
                switch (q)
                {
                    case 1:
                        SceneItem newItem = new()
                        {
                            itemID = 2101,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem);
                        break;
                    case 2:
                        SceneItem newItem1 = new()
                        {
                            itemID = 2102,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem1);
                        break;
                    case 3:
                        SceneItem newItem2 = new()
                        {
                            itemID = 2103,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem2);
                        break;
                    case 4:
                        SceneItem newItem3 = new()
                        {
                            itemID = 2104,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem3);
                        break;
                    case 5:
                        SceneItem newItem4 = new()
                        {
                            itemID = 2201,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem4);
                        break;
                    case 6:
                        SceneItem newItem5 = new()
                        {
                            itemID = 2202,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem5);
                        break;
                    case 7:
                        SceneItem newItem6 = new()
                        {
                            itemID = 2203,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem6);
                        break;
                    default:
                        break;
                }
            }

            //第四组刷新
            int i3 = Random.Range(4, 7);
            for(int j = 0; j < i3; j++)
            {
                int q = Random.Range(1, 7);
                switch (q)
                {
                    case 1:
                        SceneItem newItem = new()
                        {
                            itemID = 4101,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem);
                        break;
                    case 2:
                        SceneItem newItem1 = new()
                        {
                            itemID = 4102,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem1);
                        break;
                    case 3:
                        SceneItem newItem2 = new()
                        {
                            itemID = 3302,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem2);
                        break;
                    case 4:
                        SceneItem newItem3 = new()
                        {
                            itemID = 3304,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem3);
                        break;
                    case 5:
                        SceneItem newItem4 = new()
                        {
                            itemID = 3305,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem4);
                        break;
                    case 6:
                        SceneItem newItem5 = new()
                        {
                            itemID = 3306,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem5);
                        break;
                    default:
                        break;
                }
            }
            //第五组刷新
            int i4 = Random.Range(3, 6);
            for (int j = 0; j < i4; j++)
            {
                int q = Random.Range(1, 5);
                switch (q)
                {
                    case 1:
                        SceneItem newItem = new()
                        {
                            itemID = 4306,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem);
                        break;
                    case 2:
                        SceneItem newItem1 = new()
                        {
                            itemID = 3308,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem1);
                        break;
                    case 3:
                        SceneItem newItem2 = new()
                        {
                            itemID = 4308,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem2);
                        break;
                    case 4:
                        SceneItem newItem3 = new()
                        {
                            itemID = 4309,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem3);
                        break;
                    default:
                        break;
                }
            }
            ////临时刷新面粉
            //SceneItem flourItem = new SceneItem
            //{
            //    itemID = 1202,
            //    position = new SerializableVector3(ReturnSpecialPosition(-4, 0, 0))
            //};
            //newcurrentSceneItems.Add(flourItem);

            //判断场景物品字典中是否有该场景
            if (sceneItemDict.ContainsKey(SceneManager.GetActiveScene().name))
            {
                //有该场景，更新其列表
                sceneItemDict[SceneManager.GetActiveScene().name] = newcurrentSceneItems;
            }
            else
            {
                //没有该场景，添加场景
                sceneItemDict.Add(SceneManager.GetActiveScene().name, newcurrentSceneItems);
            }
        }
    }
    

    private Vector3 RandomPositionInSafetyPark()
    {
        float RandomPositionX = Random.Range(-28, 30);
        float RandomPositionY = Random.Range(13, -16);

        return new Vector3(RandomPositionX, RandomPositionY, 0);
    }


    //RB天台刷新
    private void UpdateItemOnRBbalcony()
    {
        if (DayManager.Instance.DayCount % 3 == 1)
        {
            List<SceneItem> newcurrentSceneItems = new();

            //天台刷新
            int i = Random.Range(4, 6);
            for (int j = 0; j < i; j++)
            {
                int q = Random.Range(1, 5);
                switch (q)
                {
                    case 1:
                        SceneItem newItem = new SceneItem
                        {
                            itemID = 1101,
                            position = new SerializableVector3(RandomPositionInRBbalcony())
                        };
                        newcurrentSceneItems.Add(newItem);
                        break;

                    case 2:
                        SceneItem newItem1 = new SceneItem
                        {
                            itemID = 1202,
                            position = new SerializableVector3(RandomPositionInRBbalcony())
                        };
                        newcurrentSceneItems.Add(newItem1);
                        break;

                    case 3:
                        SceneItem newItem2 = new SceneItem
                        {
                            itemID = 1103,
                            position = new SerializableVector3(RandomPositionInRBbalcony())
                        };

                        newcurrentSceneItems.Add(newItem2);
                        break;
                    case 4:
                        SceneItem newItem3 = new SceneItem
                        {
                            itemID = 1301,
                            position = new SerializableVector3(RandomPositionInRBbalcony())
                        };
                        newcurrentSceneItems.Add(newItem3);
                        break;
                    default:
                        break;
                }
            }
            int i1 = Random.Range(2, 4);
            for(int j = 0; j < i; j++)
            {
                int q = Random.Range(1, 5);
                switch (q)
                {
                    case 1:
                        SceneItem newItem = new SceneItem
                        {
                            itemID = 3306,
                            position = new SerializableVector3(RandomPositionInRBbalcony())
                        };
                        newcurrentSceneItems.Add(newItem);
                        break;

                    case 2:
                        SceneItem newItem1 = new SceneItem
                        {
                            itemID = 3302,
                            position = new SerializableVector3(RandomPositionInRBbalcony())
                        };
                        newcurrentSceneItems.Add(newItem1);
                        break;

                    case 3:
                        SceneItem newItem2 = new SceneItem
                        {
                            itemID = 3304,
                            position = new SerializableVector3(RandomPositionInRBbalcony())
                        };

                        newcurrentSceneItems.Add(newItem2);
                        break;
                    case 4:
                        SceneItem newItem3 = new SceneItem
                        {
                            itemID = 3305,
                            position = new SerializableVector3(RandomPositionInRBbalcony())
                        };
                        newcurrentSceneItems.Add(newItem3);
                        break;
                    default:
                        break;
                }

            }

            //判断场景物品字典中是否有该场景
            if (sceneItemDict.ContainsKey(SceneManager.GetActiveScene().name))
            {
                //有该场景，更新其列表
                sceneItemDict[SceneManager.GetActiveScene().name] = newcurrentSceneItems;
            }
            else
            {
                //没有该场景，添加场景
                sceneItemDict.Add(SceneManager.GetActiveScene().name, newcurrentSceneItems);
            }
        }

    }


    private void UpdateCollectionInRB()
    {
        List<SceneItemCollection> cursceneItemCollections = new();

        //第一个collection
        SceneItemCollection collection1 = new()
        {
            collectionitemIDList = new(),
            siblingNum = 0
        };
        //第二个collection
        SceneItemCollection collection2 = new()
        {
            collectionitemIDList = new(),
            siblingNum = 1
        };
        //第三个collection
        SceneItemCollection collection3 = new()
        {
            collectionitemIDList = new(),
            siblingNum = 2
        };

        if (DayManager.Instance.DayCount % 2 == 1)
        {
            int i = Random.Range(2, 4);
            for (int j = 0; j < i; j++)
            {
                int q = Random.Range(1, 6);
                switch (q)
                {
                    case 1:
                        collection1.collectionitemIDList.Add(2101);
                        break;
                    case 2:
                        collection1.collectionitemIDList.Add(2102);
                        break;
                    case 3:
                        collection1.collectionitemIDList.Add(2103);
                        break;
                    case 4:
                        collection1.collectionitemIDList.Add(2301);
                        break;
                    case 5:
                        collection1.collectionitemIDList.Add(2302);
                        break;
                    default:
                        break;
                }
            }
            int i1 = Random.Range(1, 3);
            for (int j = 0; j < i; j++)
            {
                int q = Random.Range(1, 3);
                switch (q)
                {
                    case 1:
                        collection1.collectionitemIDList.Add(2301);
                        break;
                    case 2:
                        collection1.collectionitemIDList.Add(2302);
                        break;
                    default:
                        break;
                }
            }
            int i1_1 = Random.Range(1, 3);
            for (int j = 0; j < i1_1; j++)
            {
                int q = Random.Range(1, 3);
                switch (q)
                {
                    case 1:
                        collection3.collectionitemIDList.Add(2303);
                        break;
                    case 2:
                        collection3.collectionitemIDList.Add(2304);
                        break;
                    default:
                        break;
                }
            }
            cursceneItemCollections.Add(collection1);

            int i2 = Random.Range(1, 3);
            for (int j = 0; j < i2; j++)
            {
                int q = Random.Range(1, 5);
                switch (q)
                {
                    case 1:
                        collection2.collectionitemIDList.Add(2104);
                        break;
                    case 2:
                        collection2.collectionitemIDList.Add(2201);
                        break;
                    case 3:
                        collection2.collectionitemIDList.Add(2202);
                        break;
                    case 4:
                        collection2.collectionitemIDList.Add(2303);
                        break;
                    default:
                        break;
                }
            }
            int i3 = Random.Range(2, 4);
            for (int j = 0; j < i3; j++)
            {
                int q = Random.Range(1, 3);
                switch (q)
                {
                    case 1:
                        collection2.collectionitemIDList.Add(2301);
                        break;
                    case 2:
                        collection2.collectionitemIDList.Add(2302);
                        break;
                    default:
                        break;
                }
            }
            cursceneItemCollections.Add(collection2);

            int i4 = Random.Range(2, 4);
            for (int j = 0; j < i4; j++)
            {
                int q = Random.Range(1, 5);
                switch (q)
                {
                    case 1:
                        collection3.collectionitemIDList.Add(4101);
                        break;
                    case 2:
                        collection3.collectionitemIDList.Add(3306);
                        break;
                    case 3:
                        collection3.collectionitemIDList.Add(2302);
                        break;
                    case 4:
                        collection3.collectionitemIDList.Add(3304);
                        break;
                    default:
                        break;
                }
            }
            int i5 = Random.Range(1, 3);
            for (int j = 0; j < i5; j++)
            {
                int q = Random.Range(1, 3);
                switch (q)
                {
                    case 1:
                        collection3.collectionitemIDList.Add(2303);
                        break;
                    case 2:
                        collection3.collectionitemIDList.Add(2304);
                        break;
                    default:
                        break;
                }
            }
            cursceneItemCollections.Add(collection3);


            //判断场景物品字典中是否有该场景
            if (sceneItemCollectionDict.ContainsKey(SceneManager.GetActiveScene().name))
            {
                //有该场景，更新其列表
                sceneItemCollectionDict[SceneManager.GetActiveScene().name] = cursceneItemCollections;
            }
            else
            {
                //没有该场景，添加场景
                sceneItemCollectionDict.Add(SceneManager.GetActiveScene().name, cursceneItemCollections);
            }
        }
    }

    private void UpdateCollectionInChurch()
    {
        List<SceneItemCollection> cursceneItemCollections = new();

        //第一个collection
        SceneItemCollection collection1 = new()
        {
            collectionitemIDList = new(),
            siblingNum = 0
        };
        SceneItemCollection collection2 = new()
        {
            collectionitemIDList = new(),
            siblingNum = 1
        };

        //天数限制暂无，待做
        int i = Random.Range(1, 4);
        for (int j = 0; j < i; j++)
        {
            int q = Random.Range(1, 3);
            switch (q)
            {
                case 1:
                    collection1.collectionitemIDList.Add(1101);
                    break;
                case 2:
                    collection1.collectionitemIDList.Add(1102);
                    break;
                default:
                    break;
            }
        }

        int i1 = Random.Range(1, 3);
        for (int j = 0; j < i1; j++)
        {
            int q = Random.Range(1, 5);
            switch (q)
            {
                case 1:
                    collection1.collectionitemIDList.Add(1103);
                    break;
                case 2:
                    collection1.collectionitemIDList.Add(1104);
                    break;
                case 3:
                    collection1.collectionitemIDList.Add(1105);
                    break;
                case 4:
                    collection1.collectionitemIDList.Add(1201);
                    break;
                default:
                    break;
            }
        }

        int i2 = Random.Range(0, 3);
        for (int j = 0; j < i2; j++)
        {
            int q = Random.Range(1, 4);
            switch (q)
            {
                case 1:
                    collection1.collectionitemIDList.Add(1202);
                    break;
                case 2:
                    collection1.collectionitemIDList.Add(1203);
                    break;
                case 3:
                    collection1.collectionitemIDList.Add(1301);
                    break;
                default:
                    break;
            }
        }
        cursceneItemCollections.Add(collection1);

        int i3 = Random.Range(1, 4);
        for (int j = 0; j < i3; j++)
        {
            int q = Random.Range(1, 4);
            switch (q)
            {
                case 1:
                    collection2.collectionitemIDList.Add(2101);
                    break;
                case 2:
                    collection2.collectionitemIDList.Add(2102);
                    break;
                case 3:
                    collection2.collectionitemIDList.Add(2103);
                    break;
                default:
                    break;
            }
        }

        int i4 = Random.Range(1, 3);
        for (int j = 0; j < i4; j++)
        {
            int q = Random.Range(1, 3);
            switch (q)
            {
                case 1:
                    collection2.collectionitemIDList.Add(2104);
                    break;
                case 2:
                    collection2.collectionitemIDList.Add(2201);
                    break;
                default:
                    break;
            }
        }

        int i5 = Random.Range(3,7);
        for (int j = 0; j < i5; j++)
        {
            int q = Random.Range(1, 4);
            switch (q)
            {
                case 1:
                    collection2.collectionitemIDList.Add(2204);
                    break;
                case 2:
                    collection2.collectionitemIDList.Add(2301);
                    break;
                case 3:
                    collection2.collectionitemIDList.Add(2302);
                    break;
                default:
                    break;
            }
        }
        cursceneItemCollections.Add(collection2);

        //判断场景物品字典中是否有该场景
        if (sceneItemCollectionDict.ContainsKey(SceneManager.GetActiveScene().name))
        {
            //有该场景，更新其列表
            sceneItemCollectionDict[SceneManager.GetActiveScene().name] = cursceneItemCollections;
        }
        else
        {
            //没有该场景，添加场景
            sceneItemCollectionDict.Add(SceneManager.GetActiveScene().name, cursceneItemCollections);
        }

    }

    //各场景中的collection重新加载
    public List<int> ReLoadCollection(int index)
    {
        List<SceneItemCollection> CollectionList = new();
        if(sceneItemCollectionDict.TryGetValue(SceneManager.GetActiveScene().name,out CollectionList))
        {
            foreach(var item in CollectionList)
            {
                if(item.siblingNum == index)
                {
                    if(item!= null)
                    {
                        return item.collectionitemIDList;
                    }
                }
            }
            
        }
        return null;

    }

    private void GetAllsceneCollection()
    {
        //新建临时列表
        List<SceneItemCollection> CollectionList = new();

        //获得场景中的所有东西并加入列表中
        foreach(var collection in FindObjectsOfType<ItemCollection>())
        {
            SceneItemCollection sceneCollection = new()
            {
                collectionitemIDList = collection.ItemIDList,
                siblingNum = collection.transform.GetSiblingIndex()
            };
            CollectionList.Add(sceneCollection);
        }

        //判断场景物品字典中是否有该场景
        if (sceneItemCollectionDict.ContainsKey(SceneManager.GetActiveScene().name))
        {
            //有该场景，更新其列表
            sceneItemCollectionDict[SceneManager.GetActiveScene().name] = CollectionList;
        }
        else
        {
            //没有该场景，添加场景
            sceneItemCollectionDict.Add(SceneManager.GetActiveScene().name, CollectionList);
        }
    }

    private Vector3 RandomPositionInRBbalcony()
    {
        float RandomPositionX = Random.Range(-7, 16);
        float RandomPositionY = 6f;

        return new Vector3(RandomPositionX, RandomPositionY, 0);
    }

    private Vector3 ReturnSpecialPosition(float x,float y,float z)
    {
        return new Vector3(x, y, z);
    }

    
    //战斗后随机掉落物品
    public void CreatItemAfterBattle(Vector3 EnemyPos,List<BagItemDetails> winItemList)
    {
        float x = (Random.value < 0.5f ? 0f : 1f);
        if (x == 0f)
        {
            Item newItem = Instantiate(ItemPrefab, EnemyPos + Random.insideUnitSphere * 1, Quaternion.identity, ItemParent);
            newItem.Init(winItemList[0].BagItemID);
        }
        else
        {
            Item newItem1 = Instantiate(ItemPrefab, EnemyPos + Random.insideUnitSphere * 1, Quaternion.identity, ItemParent);
            newItem1.Init(winItemList[0].BagItemID);
            Item newItem2 = Instantiate(ItemPrefab, EnemyPos + Random.insideUnitSphere * 1, Quaternion.identity, ItemParent);
            newItem2.Init(winItemList[1].BagItemID);
        }

        
        
    }

    private void UpdateItemOnWarYield()
    {
        if (DayManager.Instance.DayCount % 3 == 1 || DayManager.Instance.DayCount == 2 || DayManager.Instance.DayCount == 3)
        {
            List<SceneItem> newcurrentSceneItems = new();

            //第一组刷新
            int i = Random.Range(5, 9);
            for (int j = 0; j < i; j++)
            {
                int q = Random.Range(1, 10);
                switch (q)
                {
                    case 1:
                        SceneItem newItem = new SceneItem
                        {
                            itemID = 3101,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem);
                        break;
                    case 2:
                        SceneItem newItem1 = new SceneItem
                        {
                            itemID = 3102,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem1);
                        break;
                    case 3:
                        SceneItem newItem2 = new SceneItem
                        {
                            itemID = 3301,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem2);
                        break;
                    case 4:
                        SceneItem newItem3 = new SceneItem
                        {
                            itemID = 3302,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem3);
                        break;
                    case 5:
                        SceneItem newItem4 = new SceneItem
                        {
                            itemID = 3303,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem4);
                        break;
                    case 6:
                        SceneItem newItem5 = new SceneItem
                        {
                            itemID = 3304,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem5);
                        break;
                    case 7:
                        SceneItem newItem6 = new SceneItem
                        {
                            itemID = 3305,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem6);
                        break;
                    case 8:
                        SceneItem newItem7 = new SceneItem
                        {
                            itemID = 3306,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem7);
                        break;
                    case 9:
                        SceneItem newItem8 = new SceneItem
                        {
                            itemID = 3307,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem8);
                        break;
                    default:
                        break;
                }
            }
            //第二组刷新
            int i1 = Random.Range(1, 4);
            for (int j = 0; j < i1; j++)
            {
                int q = Random.Range(1, 4);
                switch (q)
                {
                    case 1:
                        SceneItem newItem = new()
                        {
                            itemID = 3201,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem);
                        break;
                    case 2:
                        SceneItem newItem1 = new()
                        {
                            itemID = 3202,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem1);
                        break;
                    case 3:
                        SceneItem newItem2 = new()
                        {
                            itemID = 3204,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem2);
                        break;
                    default:
                        break;
                }
            }

            //第三组刷新
            int i2 = Random.Range(1, 3);
            for (int j = 0; j < i2; j++)
            {
                int q = Random.Range(1, 4);
                switch (q)
                {
                    case 1:
                        SceneItem newItem = new()
                        {
                            itemID = 3401,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem);
                        break;
                    case 2:
                        SceneItem newItem1 = new()
                        {
                            itemID = 3402,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem1);
                        break;
                    case 3:
                        SceneItem newItem2 = new()
                        {
                            itemID = 3405,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem2);
                        break;
                    default:
                        break;
                }
            }

            //第四组刷新
            int i3 = Random.Range(3, 6);
            for (int j = 0; j < i3; j++)
            {
                int q = Random.Range(1, 4);
                switch (q)
                {
                    case 1:
                        SceneItem newItem = new()
                        {
                            itemID = 4101,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem);
                        break;
                    case 2:
                        SceneItem newItem1 = new()
                        {
                            itemID = 4302,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem1);
                        break;
                    case 3:
                        SceneItem newItem2 = new()
                        {
                            itemID = 3205,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem2);
                        break;
                    default:
                        break;
                }
            }
            //第五组刷新
            int i4 = Random.Range(4, 7);
            for (int j = 0; j < i4; j++)
            {
                int q = Random.Range(1, 4);
                switch (q)
                {
                    case 1:
                        SceneItem newItem = new()
                        {
                            itemID = 4307,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem);
                        break;
                    case 2:
                        SceneItem newItem1 = new()
                        {
                            itemID = 4310,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem1);
                        break;
                    case 3:
                        SceneItem newItem2 = new()
                        {
                            itemID = 4311,
                            position = new SerializableVector3(RandomPositionInWarYield())
                        };
                        newcurrentSceneItems.Add(newItem2);
                        break;
                    default:
                        break;
                }
            }

            //判断场景物品字典中是否有该场景
            if (sceneItemDict.ContainsKey(SceneManager.GetActiveScene().name))
            {
                //有该场景，更新其列表
                sceneItemDict[SceneManager.GetActiveScene().name] = newcurrentSceneItems;
            }
            else
            {
                //没有该场景，添加场景
                sceneItemDict.Add(SceneManager.GetActiveScene().name, newcurrentSceneItems);
            }
        }
    }


    private Vector3 RandomPositionInWarYield()
    {
        float RandomPositionX = Random.Range(-80, 52);
        float RandomPositionY = Random.Range(-22, 30);

        return new Vector3(RandomPositionX, RandomPositionY, 0);
    }


}
