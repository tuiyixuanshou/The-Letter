using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMSwitchBounds : MonoBehaviour
{
    public Transform CMTransform;

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
        }
        
    }

    private void Start()
    {
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

        //重新绑定人物和边界
        SwitchBounds();
        SwitchPlayer();
    }
}
