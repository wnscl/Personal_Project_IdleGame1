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
        if (GameCondition.Instance.Condition == ScreenState.Inventory)
        {
            GameCondition.Instance.ChangeCondition(ScreenState.Lobby);
            return;
        }
        GameCondition.Instance.ChangeCondition(ScreenState.Inventory);

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