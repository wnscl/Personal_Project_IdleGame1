using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenState
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
        condition = ScreenState.Lobby;
    }

    private ScreenState condition;
    public ScreenState Condition { get { return condition; } }

    private ScreenState nextCondition;
    public ScreenState NextCondition { get { return nextCondition; } }

    /*public event Action nowLobby;
    public event Action nowInventory;
    public event Action nowForge;
    public event Action nowStore;
    public event Action nowBattle;*/

    public event Action<ScreenState> conditionEvent;
   
    public void ChangeCondition(ScreenState nextState)
    {
        if (condition == nextState)
        {
            return;
        }
        nextCondition = nextState;

        conditionEvent?.Invoke(nextCondition);

        condition = nextState;
    }


    /*public void ChangeGameCondition(ScreenState nextState)
    {
        if (condition == nextState)
        {
            return;
        }

        nextCondition = nextState;
        switch (condition)
        {
            case ScreenState.Lobby:
                nowLobby.Invoke();
                break;

            case ScreenState.Inventory:
                nowInventory.Invoke();
                break;

            case ScreenState.Forge:
                nowForge.Invoke();
                break;

            case ScreenState.Store:
                nowStore.Invoke();
                break;

            case ScreenState.Battle:
                nowBattle.Invoke();
                break;
        }
        condition = nextCondition;
    }*/


}