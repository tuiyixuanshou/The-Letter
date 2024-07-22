using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bag_ControlActionBar : MonoBehaviour
{
    private string sceneName;

    //���������¼�
    public void OpenActionBar()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "SafetyArea"||sceneName == "ResearchBase" || sceneName =="Church" || sceneName == "WarYield")
        {
            EventHandler.CallTellInventoryUIToOpenAB();
        }
    }

    //���������¼�
    public void CloseActionBar()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "SafetyArea" || sceneName == "ResearchBase" || sceneName == "Church" || sceneName == "WarYield")
        {
            EventHandler.CallTellInventoryUIToCloseAB();
        }
    }
}
