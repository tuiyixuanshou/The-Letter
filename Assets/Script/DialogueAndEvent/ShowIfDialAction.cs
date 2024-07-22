using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIfDialAction : Singleton<ShowIfDialAction>
{
    public GameObject DialoguePanel;

    public bool OnShowIfDialoguePanelActive()
    {
        return DialoguePanel.activeInHierarchy;
    }
}
