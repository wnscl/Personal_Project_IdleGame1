using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

public enum TestLobbyCam
{
    L,
    I,
    F,
    S,
    B
}

public class LobbyCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    public int unactivePriority = 1;
    public int activePriority = 3;

    public GameObject[] camAnchors;
    public float moveDuration;
    private Coroutine moveCor;

    public PosAndRot posAndRot;

    private void Start()
    {
        GameCondition.Instance.nowLobby += ChangeCam;
        GameCondition.Instance.nowInventory += ChangeCam;
        GameCondition.Instance.nowBattle += ChangeCam;
    }
    //[Button]
    private void ChangeCam()
    {
        if (moveCor != null) return;

        if (GameCondition.Instance.Condition == GameState.Lobby && GameCondition.Instance.NextCondition == GameState.Battle)
        //메인 화면에서 전투 화면으로 가면 카메라 순위를 낮춤 + 카메라 코루틴 사용 안하고 초기화;
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
        int index = (int)GameCondition.Instance.Condition;
        if (isTest)
        {
            index = (int)testState;
        }
        posAndRot.Set
            (transform.position, 
            camAnchors[index].transform.position, 
            transform.rotation, 
            camAnchors[index].transform.rotation);

        return posAndRot;
    }
    private IEnumerator MoveLobbyCam()
    {
        yield return OnMoveLobbyCam(GetLobbyCamPos(), moveDuration);
        transform.position = posAndRot.targetPos;
        transform.rotation = posAndRot.targetRot;
        moveCor = null;

        posAndRot.Init();

        yield break;
    }
    private IEnumerator OnMoveLobbyCam(PosAndRot posAndRot, float moveDuration)
    {
        float timer = 0;
        while (timer <= moveDuration)
        {
            float t = timer / moveDuration;

            transform.position = Vector3.Lerp(posAndRot.requesterPos, posAndRot.targetPos, t);
            //Mathf.Lerp로 하면 안됨 왜? 타입에 맞게 해야해서 이건 트렌스폼포지션을 바꾸니 벡터3로 하는게 맞음
            transform.rotation = Quaternion.Lerp(posAndRot.requesterRot, posAndRot.targetRot, t);
            timer += Time.deltaTime;
            yield return null;
        }
        yield break;
    }





    ////////////////////////////////////테스트용 코드들
    public TestLobbyCam testState;
    public bool isTest;
    [Button]
    private void TestCamMove()
    {
        if (moveCor != null)
        {
            return;
        }
        moveCor = StartCoroutine(TestMoveLobbyCam());
    }
    private IEnumerator TestMoveLobbyCam()
    {
        yield return OnMoveLobbyCam(GetLobbyCamPos(), moveDuration);
        transform.position = posAndRot.targetPos;
        transform.rotation = posAndRot.targetRot;
        moveCor = null;

        posAndRot.Init();

        yield break;
    }
}






