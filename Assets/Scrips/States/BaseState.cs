using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseState 
{
    protected StateInfo info; //���µ��� ���������� ������ �� �ʵ�(��)
    protected BaseState(StateInfo info)
    {
        this.info = info;
    }

    //��ũ���ͺ� ������Ʈ�� ���µ��� �����ؾ��� �����͸�
    //�������̽��� ���� ��������
    public abstract void Enter(); //�߻�޼���� �����ΰ� ������Ѵ�.
    public abstract void Update();
    public abstract void Exit();

}
