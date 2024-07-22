using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CMSwitchBounds : MonoBehaviour
{
    public Transform CMTransform;
    public string sceneName;

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnLoad += OnBeforeSceneUnLoad;
        EventHandler.AfterSceneLoad += OnAfterSceneLoad;
    }


    private void OnDisable()
    {
        EventHandler.BeforeSceneUnLoad -= OnBeforeSceneUnLoad;
        EventHandler.AfterSceneLoad -= OnAfterSceneLoad;
    }

    //修改边界
    private void SwitchBounds()
    {
        CinemachineConfiner CMConfiner = GetComponent<CinemachineConfiner>();
        if (GameObject.FindGameObjectWithTag("BoundColl") != null)
        {
            PolygonCollider2D BoundColl = GameObject.FindGameObjectWithTag("BoundColl").GetComponent<PolygonCollider2D>();

            CMConfiner.m_BoundingShape2D = BoundColl;
        }

        CMConfiner.InvalidatePathCache();
    }

    //添加player
    private void SwitchPlayer()
    {
        CinemachineVirtualCamera CM = GetComponent<CinemachineVirtualCamera>();
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            
            CM.m_Follow = player;
            CM.m_LookAt = player;

            if(sceneName == "ResearchBase")
            {
                CM.m_Follow = player.GetChild(1);
                CM.m_LookAt = player;
            }
        }

        //改变摄像头距离
        if(sceneName == "SafetyArea" ||sceneName == "ResearchBase")
        {
            CM.m_Lens.OrthographicSize = 9.25f;
        }
        else if(sceneName == "Church")
        {
            CM.m_Lens.OrthographicSize = 8f;
            Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            player.position = new Vector3(0, -4.7f, 0);
        }
        else if(sceneName == "WarYield")
        {
            CM.m_Lens.OrthographicSize = 12.00f;
        }
        else
        {
            CM.m_Lens.OrthographicSize = 5.3f;
            CM.transform.position = new Vector3(0, 0, CM.transform.position.z);
        }
        
    }


    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        SwitchBounds();
        SwitchPlayer();
    }


    private void OnBeforeSceneUnLoad()
    {
        //CMTransform.position = new Vector3(0, 0, CMTransform.position.z);
        //Debug.Log(this.transform.position);
    }

    private void OnAfterSceneLoad()
    {
        //重置摄像机的位置
        CMTransform.position = new Vector3(0, 0, CMTransform.position.z);
        sceneName = SceneManager.GetActiveScene().name;

        //重置摄像机的


        //重新绑定人物和边界
        SwitchBounds();
        SwitchPlayer();
    }
}
