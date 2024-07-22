using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testing : MonoBehaviour
{
    public Button ButtonPrefab;
    public Transform PrefabParent;
    public void instantiateTest()
    {
        Instantiate(ButtonPrefab, PrefabParent);
    }
}
