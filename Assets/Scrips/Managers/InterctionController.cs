using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;



public class InterctionController : MonoBehaviour
{

    public void OnClickPlaceButton()
    {
        //ó�� �κ���� ��ư ������ ����ȭ������
        //�ٽ� ������ �κ�ȭ������
        if (GameCondition.Instance.Condition == GameState.Battle)
        {
            GameCondition.Instance.ChangeGameCondition(GameState.Lobby);
        }
        else
        {
            GameCondition.Instance.ChangeGameCondition(GameState.Battle);
        }

    }
    public void OnClickInvenButton()
    {

    }

}
