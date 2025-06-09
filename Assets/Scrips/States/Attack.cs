using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class Attack : BaseState
{
    public Attack(EntityInfo info) : base(info) { }

    //public Coroutine

    private int attackType; //0번 일반공격 1번 스킬  하지만 현재는 안씀 한바퀴 돌리고 스킬 추가할 때 넣으면 됨

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
            info.anim.SetFloat("AttackSpeed", info.atkSpeed); //공격속도 1을 기준으로 0.5 = 2초에 한번 공격
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
