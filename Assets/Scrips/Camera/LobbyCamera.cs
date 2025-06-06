using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;


public class LobbyCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    public int unactivePriority = 1;
    public int activePriority = 3;

    public GameObject[] camAnchors;
    public float moveDuration;
    private Coroutine moveCor;

    public PosAndRot posAndRot;

    //private void Awake()
    //{
    //    CameraController.Instance.lobbyCamEvent += ChangeCam;
    //}
    private void Start()
    {
        CameraController.Instance.lobbyCamEvent += ChangeCam;
    }
    //[Button]
    private void ChangeCam(CameraState choice)
    {
        if (moveCor != null) return;

        if (choice == CameraState.Battle) //전투 탭으로 가면 카메라 순위를 낮춤;
        {
            vcam.Priority = unactivePriority;
            transform.position =  camAnchors[0].transform.position;
            transform.rotation = camAnchors[0].transform.rotation;
            return;
        }
        else vcam.Priority = activePriority;

        moveCor = StartCoroutine(MoveLobbyCam());
    }

    private PosAndRot GetLobbyCamPos() //enum값으로 바뀌는 카메라의 위치를 구조체로 가져옴
    {
        int index = (int)CameraController.Instance.currentState;

        posAndRot.Set
            (transform.position, 
            camAnchors[index].transform.position, 
            transform.rotation, 
            camAnchors[index].transform.rotation);

        return posAndRot;
    }
    private IEnumerator MoveLobbyCam()
    {
        yield return BehaviorManager.Instance.OnMoveLobbyCam(GetLobbyCamPos(), moveDuration);
        transform.position = posAndRot.targetPos;
        transform.rotation = posAndRot.targetRot;
        moveCor = null;

        posAndRot.Init();

        yield break;
    }
}








/*private IEnumerator moveCam(PosAndRot_L nextPosRot)
    {
        switch (camState)
        {
            case CameraState.Lobby:
                transform.position = nextPosRot.pos;
                transform.rotation = nextPosRot.rot;
                break;

            case CameraState.Inventory:
                transform.position = nextPosRot.pos;
                transform.rotation = nextPosRot.rot;
                break;

            case CameraState.Forge:

                break;

            case CameraState.Store:

                break;

            case CameraState.Battle:
                break;
        }

        yield break;  
    }*/