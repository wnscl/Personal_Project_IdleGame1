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
        //처음 로비상태 버튼 누르면 전투화면으로
        //다시 누르면 로비화면으로
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
