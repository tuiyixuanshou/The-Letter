using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_NextDay : MonoBehaviour
{
    public GameObject EventUnDonePanel;
    public void callButton_AddDay()
    {
        if (FindObjectsOfType<HomeEventUI>().Length == 0)
        {
            EventHandler.CallButton_AddDay();
        }
        else 
        {
            EventUnDonePanel.SetActive(true);
        }
        
    }



    //≤ª”√¡À
    public void callButton_ChangePlayerP()
    {
        if (FindObjectsOfType<HomeEventUI>().Length == 0)
        {
            EventHandler.CallButton_ChangePlayerPorperty();
        }
        else
        {
            EventUnDonePanel.SetActive(true);
        }
            
    }
    
}
