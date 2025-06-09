using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseState 
{
    protected EntityInfo info; //���µ��� ���������� ������ �� �ʵ�(��)
    protected BaseState(EntityInfo info)
    {
        this.info = info;
    }

    //��ũ���ͺ� ������Ʈ�� ���µ��� �����ؾ��� �����͸�
    //�������̽��� ���� ��������
    public abstract void Enter(); //�߻�޼���� �����ΰ� ������Ѵ�.
    public abstract void Update();
    public abstract void Exit();

}
