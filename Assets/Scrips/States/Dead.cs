using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : BaseState
{
    public Dead(EntityInfo info) : base(info) { }


    public override void Enter()
    {
        info.anim.SetBool("isDead", true);
        info.anim.SetTrigger("Dead");
    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }

}