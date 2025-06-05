using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public string order;


    [Button]
    private void TestCamChange()
    {
        CameraController.Instance.ChangeView(order);
    }
}
