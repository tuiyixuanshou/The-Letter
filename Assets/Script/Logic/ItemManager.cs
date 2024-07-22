using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : Singleton<ItemManager>
{
    public Item ItemPrefab;

    public Transform ItemParent;

    public Dictionary<string, List<SceneItem>> sceneItemDict = new Dictionary<string, List<SceneItem>>();


    private void OnEnable()
    {
        EventHandler.BeforeSceneUnLoad += OnBeforeSceneUnLoad;
        EventHandler.AfterSceneLoad += OnAfterSceneLoad;
    }

    

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnLoad += OnBeforeSceneUnLoad;
        EventHandler.AfterSceneLoad += OnAfterSceneLoad;
    }

    private void OnBeforeSceneUnLoad()
    {
        GetAllSceneItem();
    }

    private void OnAfterSceneLoad()
    {
        if((SceneManager.GetActiveScene().name != "Home") && (SceneManager.GetActiveScene().name != "Map") && (SceneManager.GetActiveScene().name != "EndScene"))
        {
            ItemParent = GameObject.FindGameObjectWithTag("ItemParent").transform;

            //刷新函数重建
            UpdateItemOnSaftyPark();

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
        List<SceneItem> currentSceneItems = new List<SceneItem>();

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
                foreach(var item in currentSceneItems)
                {
                    Item newItem = Instantiate(ItemPrefab, item.position.ToVector3(), Quaternion.identity, ItemParent);
                    newItem.Init(item.itemID);
                }
            }
        }
    }


    //安全区刷新
    private void UpdateItemOnSaftyPark()
    {
        if(DayManager.Instance.DayCount%3 == 1)
        {
            List<SceneItem> newcurrentSceneItems = new();

            //食物刷新
            int i = Random.Range(7, 10);
            for(int j = 0; j < i; j++)
            {
                int q = Random.Range(1, 4);
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
                            itemID = 1102,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };

                        newcurrentSceneItems.Add(newItem1);
                        break;
                    case 3:
                        SceneItem newItem2 = new SceneItem
                        {
                            itemID = 1103,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };

                        newcurrentSceneItems.Add(newItem2);
                        break;
                    case 4:
                        SceneItem newItem3 = new SceneItem
                        {
                            itemID = 1104,
                            position = new SerializableVector3(RandomPositionInSafetyPark())
                        };
                        newcurrentSceneItems.Add(newItem3);
                        break;
                    default:

                        break;
                }
            }

            //临时刷新面粉
            SceneItem flourItem = new SceneItem
            {
                itemID = 1202,
                position = new SerializableVector3(ReturnSpecialPosition(-4, 0, 0))
            };
            newcurrentSceneItems.Add(flourItem);

            SceneItem flourItem1 = new SceneItem
            {
                itemID = 1202,
                position = new SerializableVector3(ReturnSpecialPosition(3, -2.6f, 0))
            };
            newcurrentSceneItems.Add(flourItem1);

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
        //GameObject gameObject = new GameObject();
        //gameObject.transform.position = new Vector3
    }

    private Vector3 ReturnSpecialPosition(float x,float y,float z)
    {
        return new Vector3(x, y, z);
    }

    
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

}
