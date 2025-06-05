using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;


public class BattleCamera : MonoBehaviour
{
    public int nowStage;
    public GameObject[] camAnchors;
    public float moveDuration;

    private Coroutine moveCor;


    public BattleCameraState camState;

    private void Awake()
    {
        nowStage = 0;
        camState = BattleCameraState.Lock;
    }

    public void InjectBattleEvent(BattleCameraState order)
    {
        camState = order;
        CameraController.Instance.camEvent += ChangeCam;
    }

    [Button]
    private void ChangeCam()
    {
        if (moveCor != null)
        {
            return;
        }
        if (camState == BattleCameraState.Move)
        {
            moveCor = StartCoroutine(moveCam(GetBattleCamPos(nowStage)));
        }
    }
    private PosAndRot GetBattleCamPos(int stageCount)
    {
        PosAndRot nextPosRot;

        nextPosRot.pos = camAnchors[stageCount].transform.position;
        nextPosRot.rot = camAnchors[stageCount].transform.rotation;

        return nextPosRot;
    }

    private IEnumerator moveCam(PosAndRot nextPosRot)
    {
        float timer = 0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        while (timer <= moveDuration)
        {
            float t = timer / moveDuration; // 0 �� 1
            
            transform.position = Vector3.Lerp(startPos, nextPosRot.pos, t);
            transform.rotation = Quaternion.Lerp(startRot, nextPosRot.rot, t);
            //������ -> ���� 

            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = nextPosRot.pos; 
        transform.rotation = nextPosRot.rot;
        //������ �߰����� ���ϴ� �Լ� �� ���ı��� ���� ������ ���� ����.
        //�׷��� �������� ������ ���� ���� �־��ִ� ��

        moveCor = null;
        yield break;
    }

}
