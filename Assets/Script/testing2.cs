using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing2 : MonoBehaviour
{
    public GameObject Imagee;

    private void OnEnable()
    {
        EventHandler.TestingEventLay += OnTestingEventLay;
    }

    private void OnDisable()
    {
        EventHandler.TestingEventLay -= OnTestingEventLay;
    }

    private void OnTestingEventLay()
    {
        Instantiate(Imagee, this.transform);
    }
}
