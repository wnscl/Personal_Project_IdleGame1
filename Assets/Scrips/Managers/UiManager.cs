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

    public event Action<GameOptionText> lobbyUiEvent;
    public event Action InvenUiEvent;

    private bool isLobby;

    public void ChangeUiOfPlace()
    {
        GameOptionText choice = GameOptionText.ToLobby;
        if (GameCondition.isInLobby)
        {
            choice = GameOptionText.ToStage;
        }

        lobbyUiEvent?.Invoke(choice);

    }

}
