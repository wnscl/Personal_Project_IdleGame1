using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : BaseState
{
    public Dead(EntityInfo info) : base(info) { }


    public override void Enter()
    {
        info.currentState = EntityState.Dead;
        info.anim.SetBool("isDead", true);
        info.anim.SetTrigger("Dead");
    }
    public override void Update()
    {
        if (info.entityType == EntityType.Player)
        {
            StageManager sm = StageManager.Instance;
            BehaviorManager.Instance.OnReSpawn(info.control);
            sm.player.transform.position = new Vector3(5, 0, 0);
            sm.player.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
    public override void Exit()
    {
        info.anim.SetBool("isDead", false);
    }

}