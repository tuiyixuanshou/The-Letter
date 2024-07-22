using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_ConsumePanel : MonoBehaviour
{
    public void Button_Continue()
    {
        if (PlayerProperty.Instance.PlayerHealth <= 0)
        {
            EventHandler.CallButton_ToENDGame();
        }
    }
}
