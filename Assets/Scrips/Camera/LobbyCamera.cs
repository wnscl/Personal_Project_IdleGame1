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
    //��ũ�� ���¿� ���� �޼��常 �����ϴ� ������ Ȯ���ؾ���
    {
        if (moveCor != null) return;

        if (CheckToBattleScreen()) return; //ȭ�� ���� ������ Ư�� ��Ȳ���� �Ǻ�

        int index = (int)nextState; //ī�޶� ��Ŀ�� �����ϱ� ���� �ε�����

        moveCor = StartCoroutine(MoveCam(index));
    }
    private bool CheckToBattleScreen()
    {
        if (GameCondition.Instance.Condition == ScreenState.Lobby && GameCondition.Instance.NextCondition == ScreenState.Battle)
        //���� ȭ�鿡�� ���� ȭ������ ���� ī�޶� ������ ���� + ī�޶� �ڷ�ƾ ��� ���ϰ� �ʱ�ȭ;
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
            //Mathf.Lerp�� �ϸ� �ȵ� ��? Ÿ�Կ� �°� �ؾ��ؼ� �̰� Ʈ�������������� �ٲٴ� ����3�� �ϴ°� ����
            transform.rotation = Quaternion.Lerp(startRot, endRot, t);
            //�������� Ÿ�Կ� �´� �ڷ���.lerp
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
        transform.rotation = endRot;

        moveCor = null;
        yield break;
    }


    ////////////////////////////////////�׽�Ʈ�� �ڵ��
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
/*    //������ ����ϴ� ī�޶� �̵� ���  ����ü�� ���
    private IEnumerator MoveLobbyCam()
    {
        yield return OnMoveLobbyCam(GetLobbyCamPos(), moveDuration);
        transform.position = posAndRot.targetPos;
        transform.rotation = posAndRot.targetRot;
        moveCor = null;

        posAndRot.Init();

        yield break;
    }
    private PosAndRot GetLobbyCamPos() //enum������ �ٲ�� ī�޶��� ��ġ�� ����ü�� ������
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
            //Mathf.Lerp�� �ϸ� �ȵ� ��? Ÿ�Կ� �°� �ؾ��ؼ� �̰� Ʈ�������������� �ٲٴ� ����3�� �ϴ°� ����
            transform.rotation = Quaternion.Lerp(posAndRot.requesterRot, posAndRot.targetRot, t);
            timer += Time.deltaTime;
            yield return null;
        }
        yield break;
    }*/
}






