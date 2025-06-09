using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Idle : BaseState
{
    public Idle(EntityInfo info) : base(info) { }


    public override void Enter()
    {
        InitIdle();
        
        //info.control.UpdateState();
    }
    public override void Update() 
    {
        //상태 판별 후 다음 상태 만들기
        if (StageManager.Instance.isStageRun)
        {
            info.control.ChangeState((int)EntityState.Attack);
        }

    }
    public override void Exit() 
    { 
        
    }

    private void InitIdle()
    {
        info.anim.SetBool("isMove", false);
    }


}
