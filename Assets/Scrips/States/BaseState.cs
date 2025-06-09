using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseState 
{
    protected EntityInfo info; //상태들이 공통적으로 가져야 할 필드(모델)
    protected BaseState(EntityInfo info)
    {
        this.info = info;
    }

    //스크립터블 오브젝트로 상태들이 공유해야할 데이터를
    //인터페이스를 통해 가져오게
    public abstract void Enter(); //추상메서드는 구현부가 없어야한다.
    public abstract void Update();
    public abstract void Exit();

}
