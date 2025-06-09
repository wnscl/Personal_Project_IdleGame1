using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditorInternal.VersionControl.ListControl;


public class BattleCamera : MonoBehaviour
{
    public int nowStage;
    public GameObject[] camAnchors;
    public float moveDuration;

    private Coroutine moveCor;

    public Stages stages;

    private void Awake()
    {
        nowStage = 0;
        stages = Stages.stage1;
    }

    [Button]
    public void Test()
    {
        ChangeBattleScreen(stages);
    }
    public void ChangeBattleScreen(Stages stage)
    //스크린 상태에 맞춘 메서드만 실행하는 구조로 확장해야함
    {
        if (moveCor != null) return;

        int index = (int)stage; //카메라 앵커를 지정하기 위한 인덱스값

        moveCor = StartCoroutine(MoveCam(index));
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

    //private PosAndRot GetBattleCamPos(int stageCount)
    //{
    //    PosAndRot nextPosRot;

    //    nextPosRot.pos = camAnchors[stageCount].transform.position;
    //    nextPosRot.rot = camAnchors[stageCount].transform.rotation;

    //    return nextPosRot;
    //}

    //private IEnumerator moveCam(PosAndRot nextPosRot)
    //{
    //    float timer = 0f;
    //    Vector3 startPos = transform.position;
    //    Quaternion startRot = transform.rotation;

    //    while (timer <= moveDuration)
    //    {
    //        float t = timer / moveDuration; // 0 → 1

    //        transform.position = Vector3.Lerp(startPos, nextPosRot.pos, t);
    //        transform.rotation = Quaternion.Lerp(startRot, nextPosRot.rot, t);
    //        //시작점 -> 끝점 

    //        timer += Time.deltaTime;
    //        yield return null;
    //    }
    //    transform.position = nextPosRot.pos; 
    //    transform.rotation = nextPosRot.rot;
    //    //레프는 중간점을 구하는 함수 즉 최후까지 끝에 도달할 수는 없다.
    //    //그래서 마지막에 끝점에 대한 값을 넣어주는 것

    //    moveCor = null;
    //    yield break;
    //}

}
