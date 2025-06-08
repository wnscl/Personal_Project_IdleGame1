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

        //ó�� �κ���� ��ư ������ ����ȭ������
        //�ٽ� ������ �κ�ȭ������
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