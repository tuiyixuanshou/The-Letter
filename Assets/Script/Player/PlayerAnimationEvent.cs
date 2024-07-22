using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimationEvent : MonoBehaviour
{
    public string sceneName;
    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }
    public void AnimWalkOutSide()
    {
        if(sceneName == "SafetyArea")
        {
            EventHandler.CallPlaySoundEvent(SoundName.FootStepOut);
        }
        else if(sceneName == "ResearchBase"||sceneName == "Church")
        {
            EventHandler.CallPlaySoundEvent(SoundName.FootStepIn);
        }
        
    }
}
