using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;


public class GameCondition
{
    public static bool isInLobby = true;
}

public class InterctionController : MonoBehaviour
{
    public string order;

    public Button placeChangeButton;

    [Button]
    private void TestCamChange()
    {
        //CameraController.Instance.ChangeView(order);
    }


    public void OnChangePlace()
    {
        int camIndex = 0;
        if (GameCondition.isInLobby)
        {
            camIndex = 4;
            GameCondition.isInLobby = false;
        }

        UiManager.Instance.ChangeUiOfPlace();
        CameraController.Instance.ChangeCamState(camIndex);
        CameraController.Instance.ChangeViewOfPlace();
    }

}
