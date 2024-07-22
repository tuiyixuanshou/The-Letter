using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_ToStart : MonoBehaviour
{
    public void Button_Pressed()
    {
        EventHandler.CallPlaySoundEvent(SoundName.ButtonClick);
        EventHandler.CallButton_ToStart();
    }
}
