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

struct PosAndRot //�ΰ��� �ڷ����� ��ȯ�ϱ� ���� ����ü
{
    public Vector3 pos;
    public Quaternion rot;
}

public static class CalculateHelper
{
    //���� ����� ���⺤�͸� ����
    public static Vector3 GetDirection(Vector3 target, Vector3 requester, Axis ignoreAxis = Axis.None) //�ɼų� �Ķ���� ���� �⺻���� none�� ����
    {
        Vector3 direction = Vector3.zero;
        direction = target - requester;

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
    public static Quaternion GetRotation(Quaternion target, Vector3 direction, Axis ignoreAxis = Axis.None)
    {
        


        return Quaternion.identity;
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
