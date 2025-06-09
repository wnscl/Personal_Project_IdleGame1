using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using NaughtyAttributes.Test;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public struct AttackAppoint
{
    public float attackDuration;
    public GameObject requester; //객체가 있으면 안되고 값만 있는게 맞다. -> 객체를 넣을거면 클래스로 하는 편이 좋다.
    public GameObject target;

    public void Set(float attackSpeed, GameObject requester, GameObject target)
    {
        this.attackDuration = attackSpeed;
        this.requester = requester;
        this.target = target;
    }
    public void Init()
    {
        attackDuration = 0;
        requester = null;
        target = null;
    }

}

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
    public StatController statControl;

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

        Vector3 endPos = Vector3.Lerp(startPos, targetPos, (6f / 7f));


        while (timer <= duration)
        {
            t = timer / duration;

            requester.transform.position = Vector3.Lerp(startPos, endPos, t);

            timer += Time.deltaTime;
            yield return null;
        }
        requester.transform.position = endPos;

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
    private IEnumerator OnAttackInTime(AttackAppoint appoint)
    {
        if (appoint.requester == null || appoint.target == null)
        {
            yield break;
        }

        yield return new WaitForSeconds(appoint.attackDuration);

        Vector3 direction = CalculateHelper.GetDirection(appoint.target,appoint.requester,Axis.Y);

        Ray attackRay = new Ray(appoint.requester.transform.position + Vector3.up, direction);

        RaycastHit target;

        if (Physics.Raycast(attackRay, out target, 10f))
        {
            IStateControl attacker = appoint.requester.GetComponent<IStateControl>();
            IStateControl defender = target.collider.GetComponent<IStateControl>(); 

            statControl.GetDamage(attacker, defender);
            statControl.InvokeStatUpdate(defender);
        }

        yield break;
    }

    public GameObject player;
    [Button]
    public void TestCorStart()
    {
        //StartCoroutine(MoveToNextStage());
    }
    public void OnMoveToNextStage(IStateControl control)
    {
        StartCoroutine (MoveToNextStage(control));
    }
    public IEnumerator MoveToNextStage(IStateControl control)
    {
        Vector3 firstMovePos = CalculateHelper.GetDirection(StageManager.Instance.stages[(int)StageManager.Instance.NowStage], player, Axis.Y);
        Vector3 secondMovePos = CalculateHelper.GetDirection(StageManager.Instance.stages[(int)StageManager.Instance.NextStage], player, Axis.Y);

        //1. 몸돌리기
        yield return OnRotInTime(0.1f, player, CalculateHelper.GetRotation(player, firstMovePos));
        //2. 이동
        yield return OnMoveInTime(0.3f, player, StageManager.Instance.stages[(int)StageManager.Instance.NowStage], Axis.Y);    
        //3. 몸돌리기
        yield return OnRotInTime(0.1f, player, CalculateHelper.GetRotation(player, secondMovePos));
        //4. 이동
        yield return OnMoveInTime(2.5f, player, StageManager.Instance.stages[(int)StageManager.Instance.NextStage], Axis.Y);

        control.ChangeState((int)EntityState.Idle);

        yield break;
    }
    public void OnBasicAttack(AttackAppoint attackAppoint,IStateControl control)
    {
        EntityInfo entityinfo = control.GetEntityInfo();

        entityinfo.coroutine = StartCoroutine(BasicAttack(attackAppoint, control));
    }
    public IEnumerator BasicAttack(AttackAppoint attackAppoint, IStateControl control)
    {
        yield return OnAttackInTime(attackAppoint);
        
        EntityInfo entityInfo = control.GetEntityInfo();

        entityInfo.coroutine = null;
        
        if (entityInfo.currentState != EntityState.Dead)
        {
            control.ChangeState((int)EntityState.Idle);
        }


        yield break;
    }
    public void OnReSpawn(IStateControl control)
    {
        EntityInfo entityinfo = control.GetEntityInfo();

        entityinfo.coroutine = StartCoroutine(ReSpawn(control));
    }
    public IEnumerator ReSpawn(IStateControl control)
    {
        yield return new WaitForSeconds(4.99f);
        
        EntityInfo entityInfo = control.GetEntityInfo();

        entityInfo.coroutine = null;    

        entityInfo.currentState = EntityState.Idle;
        control.ChangeState((int)EntityState.Idle);   
    }

}
