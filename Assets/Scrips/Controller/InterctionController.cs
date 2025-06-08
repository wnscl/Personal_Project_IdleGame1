using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;



public class InterctionController : MonoBehaviour
{
    public bool isTest;

    public void OnClickPlaceButton()
    {
        if (GameCondition.Instance.Condition == ScreenState.Battle)
        {
            GameCondition.Instance.ChangeCondition(ScreenState.Lobby);
        }
        else
        {
            GameCondition.Instance.ChangeCondition(ScreenState.Battle);
        }

        //처음 로비상태 버튼 누르면 전투화면으로
        //다시 누르면 로비화면으로
    }
    public void OnClickInvenButton()
    {
        ChangeGameScreen(ScreenState.Inventory);
    }
    public void OnClickForgeButton()
    {
        ChangeGameScreen(ScreenState.Forge);
    }
    public void OnClickStoreButton()
    {
        ChangeGameScreen(ScreenState.Store);
    }
    public void ChangeGameScreen(ScreenState state)
    {
        if (GameCondition.Instance.Condition == state)
        {
            GameCondition.Instance.ChangeCondition(ScreenState.Lobby);
            return;
        }
        GameCondition.Instance.ChangeCondition(state);
    }

}
/*if (GameCondition.Instance.Condition == ScreenState.Battle)
{
    GameCondition.Instance.ChangeGameCondition(ScreenState.Lobby);
}
else
{
    GameCondition.Instance.ChangeGameCondition(ScreenState.Battle);
}*/