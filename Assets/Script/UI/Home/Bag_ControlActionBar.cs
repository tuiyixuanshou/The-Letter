using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bag_ControlActionBar : MonoBehaviour
{
    private string sceneName;

    //背包动画事件
    public void OpenActionBar()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "SafetyArea"||sceneName == "ResearchBase" || sceneName =="Church" || sceneName == "WarYield")
        {
            EventHandler.CallTellInventoryUIToOpenAB();
        }
    }

    //背包动画事件
    public void CloseActionBar()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "SafetyArea" || sceneName == "ResearchBase" || sceneName == "Church" || sceneName == "WarYield")
        {
            EventHandler.CallTellInventoryUIToCloseAB();
        }
    }
}
