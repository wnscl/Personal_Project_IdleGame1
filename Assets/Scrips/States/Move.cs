using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : BaseState
{
    public Move(EntityInfo info) : base(info) { }

    //public Coroutine
    public override void Enter()
    {
        info.anim.SetBool("isMove", true);
    }
    public override void Update()
    {
        switch (info.entityType) 
        {
            case EntityType.Player:
                BehaviorManager.Instance.OnMoveToNextStage(info.control);
                break;

            case EntityType.Monster:
                info.control.ChangeState((int)EntityState.Idle);
                break;

        }
    }
    public override void Exit()
    {
        info.anim.SetBool("isMove", false);
    }
}
