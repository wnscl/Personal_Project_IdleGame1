using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : BaseState
{
    public Move(StateInfo info) : base(info) { }


    public override void Enter()
    {
        info.anim.SetBool("isMove", true);
    }
    public override void Update()
    {
        switch (info.who) 
        {
            case WhoAmI.Player:
                BehaviorManager.Instance.OnMoveToNextStage();
                break;

            case WhoAmI.Monster:

                break;

            case WhoAmI.Boss:

                break;
        }
    }
    public override void Exit()
    {
        info.anim.SetBool("isMove", false);
    }
}
