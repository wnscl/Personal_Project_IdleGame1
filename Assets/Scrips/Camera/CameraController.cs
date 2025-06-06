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
        camEvent = null; //카메라 동작 이벤트 초기화(전부 없애기)
        switch (order) //카메라 동작 이벤트에 메서드 주입
        {
            case "Lobby_Main":
                lobbyCam.InjectLobbyEvent(CameraState.Lobby);
                break;

            case "Lobby_Inven":
                lobbyCam.InjectLobbyEvent(CameraState.Inventory);
                break;
            case "Lobby_Battle":
                camBrain.m_DefaultBlend.m_Time = 0;
                //시네머신 브레인의 카메라 전환 속도를 바꾸는 방법 디폴트블렌드값을 건드리는 것이다.
                lobbyCam.InjectLobbyEvent(CameraState.Battle);
                battleCam.InjectBattleEvent(BattleCameraState.Move);
                break;
        }
        camEvent?.Invoke();//카메라 동작 이벤트 실행
    }*/


    public void ChangeViewOfPlace()
    {
        lobbyCamEvent?.Invoke(currentState);
    }
    public void ChangeCamState(int index)
    {
        currentState = (CameraState)index;
        //enum을 int값으로 찾는 방법 인덱스로 찾는 것과 같다.
    }
    public void ChangeViewOfBattle()
    {
        //지금은 그냥 한가지 움직임만 나중에 확장하면됨
        battleCamEvent?.Invoke(BattleCameraState.Move);
    }



}
