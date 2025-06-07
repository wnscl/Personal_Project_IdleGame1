using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Cinemachine;
using NaughtyAttributes;
using Unity.Mathematics;
using Unity.VisualScripting;
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

    public void ChangeLobbyScreen(ScreenState nextState)
    //스크린 상태에 맞춘 메서드만 실행하는 구조로 확장해야함
    {
        if (moveCor != null) return;

        if (CheckToBattleScreen()) return; //화면 순위 지정과 특정 상황인지 판별

        int index = (int)nextState; //카메라 앵커를 지정하기 위한 인덱스값

        moveCor = StartCoroutine(MoveCam(index));
    }
    private bool CheckToBattleScreen()
    {
        if (GameCondition.Instance.Condition == ScreenState.Lobby && GameCondition.Instance.NextCondition == ScreenState.Battle)
        //메인 화면에서 전투 화면으로 가면 카메라 순위를 낮춤 + 카메라 코루틴 사용 안하고 초기화;
        {
            vcam.Priority = unactivePriority;
            transform.position = camAnchors[0].transform.position;
            transform.rotation = camAnchors[0].transform.rotation;
            return true;
        }
        else 
        {
            vcam.Priority = activePriority;
            return false;
        }
    }
    private IEnumerator MoveCam(int index)
    {
        float timer = 0;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        Vector3 endPos = camAnchors[index].transform.position;
        Quaternion endRot = camAnchors[index].transform.rotation;

        while (timer <= moveDuration)
        {
            float t = timer / moveDuration;

            transform.position = Vector3.Lerp(startPos, endPos, t);
            //Mathf.Lerp로 하면 안됨 왜? 타입에 맞게 해야해서 이건 트렌스폼포지션을 바꾸니 벡터3로 하는게 맞음
            transform.rotation = Quaternion.Lerp(startRot, endRot, t);
            //마찬가지 타입에 맞는 자료형.lerp
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
        transform.rotation = endRot;

        moveCor = null;
        yield break;
    }


    ////////////////////////////////////테스트용 코드들
    public TestLobbyCam testState;
    public PosAndRot posAndRot; 
    [Button]
    private void TestCamMove()
    {
        if (moveCor != null)
        {
            return;
        }
        moveCor = StartCoroutine(MoveCam((int)testState));
    }
    //private IEnumerator TestMoveLobbyCam()
    //{
    //    yield return OnMoveLobbyCam(GetLobbyCamPos(), moveDuration);
    //    transform.position = posAndRot.targetPos;
    //    transform.rotation = posAndRot.targetRot;
    //    moveCor = null;

    //    posAndRot.Init();

    //    yield break;
    //}
/*    //기존에 사용하던 카메라 이동 방식  구조체를 사용
    private IEnumerator MoveLobbyCam()
    {
        yield return OnMoveLobbyCam(GetLobbyCamPos(), moveDuration);
        transform.position = posAndRot.targetPos;
        transform.rotation = posAndRot.targetRot;
        moveCor = null;

        posAndRot.Init();

        yield break;
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
    }*/
}






