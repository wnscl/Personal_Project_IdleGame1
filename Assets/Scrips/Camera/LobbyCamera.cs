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
    public CameraState camState;

    private void Awake()
    {
        camState = CameraState.Lobby;
    }

    public void InjectLobbyEvent(CameraState order)
    {
        camState = order;
        CameraController.Instance.camEvent += ChangeCam;
    }
    [Button]
    private void ChangeCam()
    {
        if (moveCor != null) return;

        if (camState == CameraState.Battle) vcam.Priority = unactivePriority;
        else vcam.Priority = activePriority;

        moveCor = StartCoroutine(moveCamTest(GetLobbyCamPos()));
    }

    private PosAndRot GetLobbyCamPos() //enum값으로 빠뀌는 카메라의 위치를 구조체로 가져옴
    {
        PosAndRot nextPosRot;
        int index = 0;
        
        if (camState != CameraState.Battle) index = (int)camState;
        else index = 0;

        nextPosRot.pos = camAnchors[index].transform.position;
        nextPosRot.rot = camAnchors[index].transform.rotation;

        return nextPosRot;
    }
    private IEnumerator moveCamTest(PosAndRot nextPosRot)
    {
        float timer = 0;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;


        while (timer <= moveDuration)
        {
            float t = timer / moveDuration;

            transform.position = Vector3.Lerp(startPos, nextPosRot.pos, t);
            //Mathf.Lerp로 하면 안됨 왜? 타입에 맞게 해야해서 이건 트렌스폼포지션을 바꾸니 벡터3로 하는게 맞음
            transform.rotation = Quaternion.Lerp(startRot, nextPosRot.rot, t);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = nextPosRot.pos;
        transform.rotation = nextPosRot.rot;

        moveCor = null;
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