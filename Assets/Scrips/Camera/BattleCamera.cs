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
    //��ũ�� ���¿� ���� �޼��常 �����ϴ� ������ Ȯ���ؾ���
    {
        if (moveCor != null) return;

        int index = (int)stage; //ī�޶� ��Ŀ�� �����ϱ� ���� �ε�����

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
    //        float t = timer / moveDuration; // 0 �� 1

    //        transform.position = Vector3.Lerp(startPos, nextPosRot.pos, t);
    //        transform.rotation = Quaternion.Lerp(startRot, nextPosRot.rot, t);
    //        //������ -> ���� 

    //        timer += Time.deltaTime;
    //        yield return null;
    //    }
    //    transform.position = nextPosRot.pos; 
    //    transform.rotation = nextPosRot.rot;
    //    //������ �߰����� ���ϴ� �Լ� �� ���ı��� ���� ������ ���� ����.
    //    //�׷��� �������� ������ ���� ���� �־��ִ� ��

    //    moveCor = null;
    //    yield break;
    //}

}
