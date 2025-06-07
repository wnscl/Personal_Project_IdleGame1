using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using NaughtyAttributes.Test;
using UnityEngine;


public class BehaviorManager : MonoBehaviour
{
    private static BehaviorManager instance;
    public static BehaviorManager Instance
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
    }
    private IEnumerator OnMoveInTime(float duration, GameObject requester, GameObject target, Axis ignoreAxis = Axis.None)
    {
        float timer = 0;
        float t = 0;

        Vector3 startPos = requester.transform.position;
        Vector3 targetPos = target.transform.position;
        if (ignoreAxis != Axis.None)
        {
            switch (ignoreAxis)
            {
                case Axis.X:
                    targetPos.x = 0f;
                    break;
                case Axis.Y:
                    targetPos.y = 0f;
                    break;
                case Axis.Z:
                    targetPos.z = 0f;
                    break;
            }
        }
        //방향벡터를 넘겨주는 게 아니라 이렇게
        //대상의 포지션 자체를 써야 레프가 제대로 작동

        while (timer <= duration)
        {
            t = timer / duration;

            requester.transform.position = Vector3.Lerp(startPos, targetPos, t);

            timer += Time.deltaTime;
            yield return null;
        }
        requester.transform.position = targetPos;

        yield break;
    }
    private IEnumerator OnRotInTime(float duration, GameObject target, Quaternion targetRot)
    {
        float timer = 0;
        float t = 0;

        Quaternion startRot = target.transform.rotation;

        while (timer <= duration)
        {
            t = timer / duration;

            target.transform.rotation = Quaternion.Lerp(startRot, targetRot, t);

            timer += Time.deltaTime;
            yield return null;
        }
        target.transform.rotation = targetRot;

        yield break;
    }

    public GameObject player;
    public GameObject[] testObj;
    [SerializeField] private Stages nowTestStage;
    [SerializeField] private Stages nextTestStage;
    public float testMoveDuration;
    [Button]
    public void TestCorStart()
    {
        StartCoroutine(MoveToNextStage());
    }
    public void OnMoveToNextStage()
    {
        StartCoroutine (MoveToNextStage());
    }
    public IEnumerator MoveToNextStage()
    {
        Vector3 firstMovePos = CalculateHelper.GetDirection(testObj[(int)nowTestStage], player, Axis.Y);
        Vector3 secondMovePos = CalculateHelper.GetDirection(testObj[(int)nextTestStage], player, Axis.Y);

        //1. 몸돌리기
        yield return OnRotInTime(0.2f, player, CalculateHelper.GetRotation(player, firstMovePos));
        //2. 이동
        yield return OnMoveInTime(0.4f, player, testObj[(int)nowTestStage], Axis.Y);    
        //3. 몸돌리기
        yield return OnRotInTime(0.2f, player, CalculateHelper.GetRotation(player, secondMovePos));
        //4. 이동
        yield return OnMoveInTime(3f, player, testObj[(int)nextTestStage], Axis.Y);

        yield break;
    }


}
