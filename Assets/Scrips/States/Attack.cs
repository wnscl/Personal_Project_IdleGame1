using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class Attack : BaseState
{
    public Attack(EntityInfo info) : base(info) { }

    //public Coroutine

    private int attackType; //0�� �Ϲݰ��� 1�� ��ų  ������ ����� �Ⱦ� �ѹ��� ������ ��ų �߰��� �� ������ ��

    private AttackAppoint appoint;

    public override void Enter()
    {
        InitAttack(false);
    }
    public override void Update()
    {
        BehaviorManager.Instance.OnBasicAttack(appoint, info.control);
    }
    public override void Exit()
    {
        InitAttack(true);
    }
    private void InitAttack(bool isAttack)
    {
        attackType = 0;
        if (!isAttack)
        {
            info.anim.SetFloat("AttackSpeed", info.atkSpeed); //���ݼӵ� 1�� �������� 0.5 = 2�ʿ� �ѹ� ����
            info.anim.SetTrigger("Attack");

            AppointTarget();
        }
    }
    private void AppointTarget()
    {
        GameObject attacker, defender;

        if (info.entityType == EntityType.Player)
        {
            attacker = StageManager.Instance.player;
            defender = StageManager.Instance.enemy;
        }
        else
        {
            attacker = StageManager.Instance.enemy;
            defender = StageManager.Instance.player;
        }
        
        appoint.Set((1f / info.atkSpeed), attacker, defender);
    }

    
}
