using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : Singleton<CameraControl>
{
    public CinemachineImpulseSource impulseSource;


    //��Ļ��
    public void CMShake()
    {
        impulseSource.GenerateImpulse();
    }

}
