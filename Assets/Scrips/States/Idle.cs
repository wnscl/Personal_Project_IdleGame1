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
        //���� �Ǻ� �� ���� ���� �����
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
