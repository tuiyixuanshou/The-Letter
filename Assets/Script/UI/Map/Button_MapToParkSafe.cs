using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_MapToParkSafe : MonoBehaviour
{
    public int ActionPoint;
  public void callButton_MapToPark()
    {
        if (PlayerProperty.Instance.PlayerAction >= ActionPoint)
        {
            ActionPointControl.Instance.PointHUDControl(PlayerProperty.Instance.PlayerAction - ActionPoint);
            EventHandler.CallButton_MaptoPark();
        }
        
    }


}
