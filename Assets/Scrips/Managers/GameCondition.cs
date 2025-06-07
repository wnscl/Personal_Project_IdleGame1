using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Lobby = 0,
    Inventory,
    Forge,
    Store,
    Battle
}
public class GameCondition : MonoBehaviour
{
    private static GameCondition instance;
    public static GameCondition Instance
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
        condition = GameState.Lobby;
    }

    private GameState condition;
    public GameState Condition { get { return condition; } }

    private GameState nextCondition;
    public GameState NextCondition { get { return nextCondition; } }


    public event Action nowLobby;
    public event Action nowInventory;
    public event Action nowForge;
    public event Action nowStore;
    public event Action nowBattle;

    public void ChangeGameCondition(GameState nextState)
    {
        if (condition == nextState)
        {
            return;
        }

        nextCondition = nextState;
        switch (condition)
        {
            case GameState.Lobby:
                nowLobby.Invoke();
                break;

            case GameState.Inventory:
                nowInventory.Invoke();
                break;

            case GameState.Forge:
                nowForge.Invoke();
                break;

            case GameState.Store:
                nowStore.Invoke();
                break;

            case GameState.Battle:
                nowBattle.Invoke();
                break;
        }
        condition = nextCondition;
    }


}