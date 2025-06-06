using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Axis
{
    None = 0,
    X, 
    Y, 
    Z
}

public struct PosAndRot //�ΰ��� �ڷ����� ��ȯ�ϱ� ���� ����ü ���� ���ó ī�޶�
{
    public Vector3 requesterPos;
    public Quaternion requesterRot;
    public Vector3 targetPos;
    public Quaternion targetRot;

    public void Set(Vector3 requesterPos, Vector3 targetPos, Quaternion requesterRot, Quaternion targetRot) //������ ���� �Լ�
    {
        this.requesterPos = requesterPos;
        this.requesterRot = requesterRot;
        this.targetPos = targetPos;
        this.targetRot = targetRot;
    }
    public void Init()
    {
        requesterPos = Vector3.zero;
        requesterRot = Quaternion.identity;
        targetPos = Vector3.zero;
        targetRot = Quaternion.identity;
    }
}

public static class CalculateHelper
{
    //���� ����� ���⺤�͸� ����
    public static Vector3 GetDirection(GameObject target, GameObject requester, Axis ignoreAxis = Axis.None) //�ɼų� �Ķ���� ���� �⺻���� none�� ����
    {
        Vector3 direction = Vector3.zero;
        direction = (target.transform.position - requester.transform.position);

        if (ignoreAxis == Axis.None)
        {
            return direction.normalized;
        }
        
        switch (ignoreAxis)
        {
            case Axis.X:
                direction.x = 0f;
                break;

            case Axis.Y:
                direction.y = 0f;
                break;

            case Axis.Z:
                direction.z = 0f;
                break;  
        }

        return direction.normalized;
    }
    //�־��� ������ ȸ������ ����
    public static Quaternion GetRotation(GameObject requester, Vector3 direction, Axis aliveAxis = Axis.None)
    {
        Quaternion rotation = requester.transform.rotation;
        if (aliveAxis == Axis.None)
        {
            rotation = Quaternion.LookRotation(direction);
        }


        return rotation;
    }

    public static float GetDistance(Vector3 target, Vector3 requester, Axis ignoreAxis = Axis.None)
    {
        if (ignoreAxis == Axis.None)
        {
            float distance = 0f;
            distance = Vector3.Distance(target, requester); 
            //Vector3.Distance�� �κ����� ���̸� ���ϰ� �װ� magnitude�� ��ȯ ��
            //(target - requester).magnitude�� �Ȱ���.
            return distance;
        }

        Vector3 direction = target - requester;

        switch (ignoreAxis)
        {
            case Axis.X:
                direction.x = 0f;
                break;

            case Axis.Y:
                direction.y = 0f;
                break;

            case Axis.Z:
                direction.z = 0f;
                break;
        }

        return direction.magnitude;
        //Unity�� * *"�� �����̶�� magnitude ��� sqrMagnitude�� ����϶�" * *�� �����Ѵ�.
        //��Ȯ�� ���� �ʿ� ���� �� ���ɻ� ������ Ȯ��
        //�� magnitude �� ��Ȯ�� ����� �ʿ��� �� �Ÿ��񱳸� ������ ���� ������ ��� (��ġ���� ����� �Ÿ����谡 �ʿ��� ��)
        //sqrMagnitude�� ������Ʈ���� ���ӵǴ� ���꿡�� �Ǵ� �Ÿ��񱳸� �� �� (��Ÿ��ȿ� ���Դ°�? ��)
    }
}
