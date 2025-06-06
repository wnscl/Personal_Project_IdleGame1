using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;


public enum CameraState
{
    Lobby = 0,
    Inventory,
    Forge,
    Store,
    Battle
}
public enum BattleCameraState
{
    Move,
    Lock
}

public class CameraController : MonoBehaviour
{
    private static CameraController instance;
    public static CameraController Instance
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
        currentState = CameraState.Lobby;
    }

    [SerializeField] private CinemachineBrain camBrain;

    //[SerializeField] private LobbyCamera lobbyCam;
    [SerializeField] private BattleCamera battleCam;
    public event Action<CameraState> lobbyCamEvent;
    public event Action<BattleCameraState> battleCamEvent;

    public CameraState currentState;

    /*public void ChangeView(string order)
    {
        camEvent = null; //ī�޶� ���� �̺�Ʈ �ʱ�ȭ(���� ���ֱ�)
        switch (order) //ī�޶� ���� �̺�Ʈ�� �޼��� ����
        {
            case "Lobby_Main":
                lobbyCam.InjectLobbyEvent(CameraState.Lobby);
                break;

            case "Lobby_Inven":
                lobbyCam.InjectLobbyEvent(CameraState.Inventory);
                break;
            case "Lobby_Battle":
                camBrain.m_DefaultBlend.m_Time = 0;
                //�ó׸ӽ� �극���� ī�޶� ��ȯ �ӵ��� �ٲٴ� ��� ����Ʈ���尪�� �ǵ帮�� ���̴�.
                lobbyCam.InjectLobbyEvent(CameraState.Battle);
                battleCam.InjectBattleEvent(BattleCameraState.Move);
                break;
        }
        camEvent?.Invoke();//ī�޶� ���� �̺�Ʈ ����
    }*/


    public void ChangeViewOfPlace()
    {
        lobbyCamEvent?.Invoke(currentState);
    }
    public void ChangeCamState(int index)
    {
        currentState = (CameraState)index;
        //enum�� int������ ã�� ��� �ε����� ã�� �Ͱ� ����.
    }
    public void ChangeViewOfBattle()
    {
        //������ �׳� �Ѱ��� �����Ӹ� ���߿� Ȯ���ϸ��
        battleCamEvent?.Invoke(BattleCameraState.Move);
    }



}
