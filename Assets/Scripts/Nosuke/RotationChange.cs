using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationChange : MonoBehaviour
{
    // ���f����Unity�Ɏ����Ă����ۂɉ�]�̒l�������������Ƃ��������̂�Unity��ŕύX���邽�߂̃X�N���v�g
    // ���Ɏg�����Ƃ͂Ȃ���������

    public Vector3 changeRotation;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localEulerAngles = changeRotation;
        // ���͂��ꂽ��������Ɋp�x��ύX
    }
}
