using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveToPark : MonoBehaviour
{
    public GameObject AskPanel;

    public void OnLeaveButtonPressed()
    {
        PlayerInventory.Instance.isStayInChurchOutDoor = true;
        AskPanel.SetActive(false);
        EventHandler.CallButton_MaptoPark();
    }
}
