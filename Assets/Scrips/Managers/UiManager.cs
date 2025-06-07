using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private static UiManager instance;
    public static UiManager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public event Action<ScreenState> UiActiveEvent; //ui을 끄고 키거나 하는 이벤트

    public void SendMessage_UiActiveEvent()
    {
        UiActiveEvent?.Invoke(GameCondition.Instance.Condition);
    }

}
