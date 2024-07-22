using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InfoBookControl : MonoBehaviour
{
    public Image NewInfoSign;

    private void OnEnable()
    {
        if (InfoManager.Instance.isFindNewInfo)
        {
            NewInfoSign.enabled = true;
        }
    }

    public void ButtonPressed()
    {
        if (InfoManager.Instance.isFindNewInfo)
        {
            InfoManager.Instance.isFindNewInfo = false;
        }
    }
}
